using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kontest.Infrastructure.Interfaces;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kontest.Service.Implementations
{
    public class OrganizationService : IOrganizationService
    {
        private IRepository<Organization, int> _organizationRepository;
        private IRepository<UserOrganization, int> _userOrganizationRepository;
        private IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        public OrganizationService(
            IRepository<Organization, int> organizationRepository,
            IRepository<UserOrganization, int> userOrganizationRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _userOrganizationRepository = userOrganizationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public OrganizationViewModel GetOrganizationById(int id)
        {
            var org = _organizationRepository.FindById(id);
            return _mapper.Map<Organization, OrganizationViewModel>(org);
        }

        public List<UserOrganizationViewModel> Test(string id)
        {
            var result = _userOrganizationRepository
                .FindAll(uo => uo.UserId == new Guid(id), uo => uo.Organization)
                .ProjectTo<UserOrganizationViewModel>(_mapper.ConfigurationProvider, uo => uo.Organization)
                .ToList();
            //var result = _organizationRepository
            //    .FindAll(o => o.UserOrganizations.Any(uo => uo.UserId == new Guid(id)), o => o.UserOrganizations)
            //    .ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider, o => o.UserOrganizations)
            //    .ToList();
            return result;
        }
    }
}
