using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kontest.Infrastructure.Interfaces;
using Kontest.Model.Entities;
using Kontest.Service.Interfaces;
using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kontest.Service.Implementations
{
    public class OrganizationCategoryService : IOrganizationCategoryService
    {
        private IRepository<OrganizationCategory, int> _organizationCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public OrganizationCategoryService(
            IRepository<OrganizationCategory, int> organizationCategoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _organizationCategoryRepository = organizationCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<OrganizationCategoryViewModel> GetAll()
        {
            return _organizationCategoryRepository.FindAll()
                .ProjectTo<OrganizationCategoryViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
