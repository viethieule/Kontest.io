using IdentityModel;
using Kontest.Infrastructure.Interfaces;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontest.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRepository<UserOrganization, Guid> _userOrgnizationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IRepository<UserOrganization, Guid> userOrgnizationRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userOrgnizationRepository = userOrgnizationRepository;
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser FindUserByOtac(string otac)
        {
            return _userManager.Users.FirstOrDefault(u => u.OTAC == otac);
        }

        public async Task<string> Register(UserViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result != null && result.Succeeded)
            {
                var otac = CryptoRandom.CreateUniqueId();
                var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: otac,
                    salt: new byte[128 / 8],
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                user.OTAC = hashed;
                user.OTACExpires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1));
                var setOtacResult = await _userManager.UpdateAsync(user);
                if (setOtacResult != null && setOtacResult.Succeeded)
                {
                    return hashed;
                }
            }

            return string.Empty;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public List<ApplicationUser> GetUsersByOrganizationId(string id)
        {
            var users = _userOrgnizationRepository
                .FindAll(x => x.OrganizationId.Equals(new Guid(id)), x => x.User)
                .Select(x => x.User)
                .ToList();

            return users;
        }
    }
}
