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
    public class OrganizationRequestController : ControllerBase
    {
        private IOrganizationRequestService _organizationRequestService;

        public OrganizationRequestController(
            IOrganizationRequestService organizationRequestService)
        {
            _organizationRequestService = organizationRequestService;
        }

        [HttpGet]
        [Route("getByStatus")]
        public List<OrganizationRequestViewModel> GetOrganizationRequestByStatus([FromQuery] int status)
        {
            return _organizationRequestService.GetOrganizationRequestByStatus(status);
        }

        [HttpPost]
        [Route("create")]
        public OrganizationRequestViewModel Create(OrganizationRequestViewModel orgRequestVm)
        {
            return _organizationRequestService.CreateOrganizationRequest(orgRequestVm);
        }
    }
}