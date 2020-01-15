using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kontest.Infrastructure.Interfaces;
using Kontest.Model.Entities;
using Kontest.Model.Enums;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kontest.Service.Implementations
{
    public class OrganizationRequestService : IOrganizationRequestService
    {
        private IRepository<OrganizationRequest, int> _organizationRequestRepository;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public OrganizationRequestService(
            IRepository<OrganizationRequest, int> organizationRequestRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _organizationRequestRepository = organizationRequestRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public OrganizationRequestViewModel CreateOrganizationRequest(OrganizationRequestViewModel organizationReqVm)
        {
            var request = _mapper.Map<OrganizationRequest>(organizationReqVm);

            _organizationRequestRepository.Add(request);
            _unitOfWork.Commit();

            return _mapper.Map<OrganizationRequestViewModel>(request);
        }

        public List<OrganizationRequestViewModel> GetOrganizationRequestByStatus(int status)
        {
            return _organizationRequestRepository
                .FindAll(req => req.OrgRequestStatus.HasValue && req.OrgRequestStatus.Value == (OrgRequestStatus)status,
                    req => req.OrganizationCategory, req => req.RequestingUser)
                .ProjectTo<OrganizationRequestViewModel>(_mapper.ConfigurationProvider,
                    req => req.OrganizationCategory, req => req.RequestingUser)
                .ToList();
        }
    }
}
