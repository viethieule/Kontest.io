using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kontest.Infrastructure.Interfaces;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontest.Service.Implementations
{
    public class UserOrganizationService : IUserOrganizationService
    {
        private IRepository<UserOrganization, int> _userOrganizationRepository;
        private IRepository<Organization, int> _organizationRepository;
        private UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserOrganizationService(
            IRepository<UserOrganization, int> userOrganizationRepository,
            IRepository<Organization, int> organizationRepository,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userOrganizationRepository = userOrganizationRepository;
            _organizationRepository = organizationRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<UserOrganizationViewModel> GetOrganizationsByUserId(string id)
        {
            return _userOrganizationRepository
                .FindAll(uo => uo.UserId == new Guid(id), uo => uo.Organization)
                .ProjectTo<UserOrganizationViewModel>(_mapper.ConfigurationProvider, uo => uo.Organization)
                .ToList();
        }

        public List<UserOrganizationViewModel> GetUsersByOrganizationsId(int id)
        {
            return _userOrganizationRepository
                .FindAll(uo => uo.OrganizationId == id, uo => uo.User)
                .ProjectTo<UserOrganizationViewModel>(_mapper.ConfigurationProvider, uo => uo.User)
                .ToList();
        }

        public void DeleteUserOrganizationById(int id)
        {
            _userOrganizationRepository.Remove(id);
            _unitOfWork.Commit();
        }

        public async Task<UserOrganizationViewModel> AddUserOrganization(UserOrganizationViewModel userOrganizationVm)
        {
            var user = await _userManager.FindByIdAsync(userOrganizationVm.UserId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var org = _organizationRepository.FindById(userOrganizationVm.OrganizationId);
            if (org == null)
            {
                throw new KeyNotFoundException("Organization not found");
            }

            var userOrganization = _mapper.Map<UserOrganization>(userOrganizationVm);
            userOrganization.AssignedDate = DateTime.Now;

            _userOrganizationRepository.Add(userOrganization);
            _unitOfWork.Commit();

            return _mapper.Map<UserOrganizationViewModel>(userOrganization);
        }
    }
}
