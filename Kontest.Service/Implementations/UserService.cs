using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kontest.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser FindUserByOtac(string otac)
        {
            return _userManager.Users.FirstOrDefault(u => u.OTAC == otac);
        }
    }
}
