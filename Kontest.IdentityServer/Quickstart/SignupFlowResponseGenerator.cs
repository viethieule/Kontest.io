using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kontest.IdentityServer.Quickstart
{
    public class SignupFlowResponseGenerator : AuthorizeInteractionResponseGenerator
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public SignupFlowResponseGenerator(ISystemClock clock,
            ILogger<AuthorizeInteractionResponseGenerator> logger,
            IConsentService consent,
            IProfileService profile,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService)
            : base(clock, logger, consent, profile)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        {
            var processOtacRequest = true;

            var isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            // if user is already authenticated then no need to process otac request.
            if (isAuthenticated)
            {
                processOtacRequest = false;
            }

            // here we only process only the request which have otac
            var acrValues = request.GetAcrValues().ToList();
            if (acrValues == null || acrValues.Count == 0)
            {
                processOtacRequest = false;
            }

            var otac = acrValues.FirstOrDefault(x => x.Contains("otac:"));
            if (string.IsNullOrEmpty(otac))
            {
                processOtacRequest = false;
            }
            else
            {
                otac = otac.Split(':')[1];
                if (string.IsNullOrEmpty(otac))
                {
                    processOtacRequest = false;
                }
            }

            if (processOtacRequest)
            {
                var user = _userService.FindUserByOtac(otac);
                if (user != null)
                {
                    // mark the otp as expired so that it cannot be used again.
                    user.OTAC = null;
                    user.OTACExpires = null;
                    await _userManager.UpdateAsync(user);

                    var claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, user.UserName),
                        new Claim(JwtClaimTypes.Email, user.Email)
                    };

                    var svr = new IdentityServerUser(user.Id.ToString())
                    {
                        AuthenticationTime = Clock.UtcNow.DateTime,
                        AdditionalClaims = claims
                    };

                    var claimPrincipal = svr.CreatePrincipal();
                    request.Subject = claimPrincipal;

                    await _signInManager.SignInAsync(user, true);

                    return new InteractionResponse
                    {
                        IsLogin = false, // as login is false it will not redirect to login page but will give the authorization code
                        IsConsent = false
                    };
                }
            }

            return await base.ProcessInteractionAsync(request, consent);
        }
    }
}
