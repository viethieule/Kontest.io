using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kontest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<string> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result != null && result.Succeeded)
            {
                var otac = CryptoRandom.CreateUniqueId();
                var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: otac,
                    salt: new byte[128 / 8],
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                user.OTAC = hashed;
                user.OTACExpires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1));
                var setOtacResult = await _userManager.UpdateAsync(user);
                if (setOtacResult != null && setOtacResult.Succeeded)
                {
                    return hashed;
                }
            }

            return string.Empty;
        }
    }
}