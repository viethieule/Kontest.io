using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kontest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrganizationController : ControllerBase
    {
        private IUserOrganizationService _userOrganizationService;

        public UserOrganizationController(
            IUserOrganizationService userOrganizationService)
        {
            _userOrganizationService = userOrganizationService;
        }

        [HttpGet]
        [Route("getuserorganizationsbyuserid/{id}")]
        public IEnumerable<UserOrganizationViewModel> GetUserOrganizationsByUserId(string id)
        {
            return _userOrganizationService.GetOrganizationsByUserId(id);
        }
    }
}