using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kontest.Infrastructure.Interfaces;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontest.Service.Implementations
{
    public class UserOrganizationService :IUserOrganizationService
    {
        private IRepository<UserOrganization, int> _userOrganizationRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserOrganizationService(
            IRepository<UserOrganization, int> userOrganizationRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userOrganizationRepository = userOrganizationRepository;
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
    }
}
