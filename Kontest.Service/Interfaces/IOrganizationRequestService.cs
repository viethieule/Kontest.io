using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Service.Interfaces
{
    public interface IOrganizationRequestService
    {
        OrganizationRequestViewModel CreateOrganizationRequest(OrganizationRequestViewModel organizationReqVm);
        List<OrganizationRequestViewModel> GetOrganizationRequestByStatus(int status);
    }
}
