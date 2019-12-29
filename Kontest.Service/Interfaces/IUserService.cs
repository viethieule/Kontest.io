using Kontest.Model.Entities;
using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kontest.Service.Interfaces
{
    public interface IUserService
    {
        ApplicationUser FindUserByOtac(string otac);
        Task<string> Register(UserViewModel model);
        Task<ApplicationUser> GetUserById(string id);
        List<UserViewModel> GetUsersByOrganizationId(int id);
    }
}
