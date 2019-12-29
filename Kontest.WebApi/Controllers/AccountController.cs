using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kontest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IUserService userService,
            ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<string> Register(UserViewModel model)
        {
            var otac = await _userService.Register(model);
            return otac;
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            return user;
        }

        [HttpGet]
        [Route("getbyorgid/{id}")]
        public IEnumerable<UserViewModel> GetUsersByOrganizationId(int id)
        {
            return _userService.GetUsersByOrganizationId(id);
        }
    }
}