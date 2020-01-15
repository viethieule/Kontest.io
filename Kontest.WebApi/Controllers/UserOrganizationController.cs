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

        [HttpGet]
        [Route("getUserOrganizationsByOrganizationId/{id}")]
        public IEnumerable<UserOrganizationViewModel> GetUserOrganizationsByOrganizationId(int id)
        {
            return _userOrganizationService.GetUsersByOrganizationsId(id);
        }

        [HttpDelete]
        [Route("deleteUserOrganizationByid/{id}")]
        public void DeleteUserOrganizationById(int id)
        {
            _userOrganizationService.DeleteUserOrganizationById(id);
        }

        [HttpPost]
        [Route("addUserOrganization")]
        public async Task<UserOrganizationViewModel> AddUserOrganization(UserOrganizationViewModel userOrganizationVm)
        {
            var user = await _userOrganizationService.AddUserOrganization(userOrganizationVm);
            return user;
        }
    }
}