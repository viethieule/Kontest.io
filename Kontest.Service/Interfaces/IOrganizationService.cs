using Kontest.Model.Entities;
using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Service.Interfaces
{
    public interface IOrganizationService
    {
        OrganizationViewModel GetOrganizationById(int id);
        List<UserOrganizationViewModel> Test(string id);
    }
}
