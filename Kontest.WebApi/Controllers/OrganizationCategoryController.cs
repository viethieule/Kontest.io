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
    public class OrganizationCategoryController : ControllerBase
    {
        private IOrganizationCategoryService _organizationCategoryService;

        public OrganizationCategoryController(
            IOrganizationCategoryService organizationCategoryService)
        {
            _organizationCategoryService = organizationCategoryService;
        }

        [HttpGet]
        [Route("getAll")]
        public IEnumerable<OrganizationCategoryViewModel> GetAll()
        {
            return _organizationCategoryService.GetAll();
        }
    }
}