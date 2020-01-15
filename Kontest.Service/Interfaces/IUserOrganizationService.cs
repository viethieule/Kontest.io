using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kontest.Service.Interfaces
{
    public interface IUserOrganizationService
    {
        Task<UserOrganizationViewModel> AddUserOrganization(UserOrganizationViewModel userOrganizationVm);
        List<UserOrganizationViewModel> GetOrganizationsByUserId(string id);
        void DeleteUserOrganizationById(int id);
        List<UserOrganizationViewModel> GetUsersByOrganizationsId(int id);
    }
}
