using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kontest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IOrganizationService _organizationService;

        public OrganizationController(
            IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public OrganizationViewModel GetOrganizationById(int id)
        {
            //return new OrganizationViewModel
            //{
            //    Id = id,
            //    Name = "Câu lạc bộ tin học"
            //};
            return _organizationService.GetOrganizationById(id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("test/{id}")]
        public List<UserOrganizationViewModel> Test(string id)
        {
            return _organizationService.Test(id);
        }
    }
}