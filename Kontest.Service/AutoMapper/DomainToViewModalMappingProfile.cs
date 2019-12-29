﻿using AutoMapper;
using Kontest.Model.Entities;
using Kontest.Service.ViewModels;

namespace Kontest.Service.AutoMapper
{
    public class DomainToViewModalMappingProfile : Profile
    {
        public DomainToViewModalMappingProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserOrganization, UserOrganizationViewModel>()
                .ForMember(x => x.Organization, opt => opt.ExplicitExpansion())
                .ForMember(x => x.User, opt => opt.ExplicitExpansion());
            CreateMap<Organization, OrganizationViewModel>()
                .ForMember(x => x.UserOrganizations, opt => opt.ExplicitExpansion())
                .ForMember(x => x.Events, opt => opt.ExplicitExpansion())
                .ForMember(x => x.OrganizationRequest, opt => opt.ExplicitExpansion());
            CreateMap<Event, EventViewModel>();
            CreateMap<OrganizationRequest, OrganizationRequestViewModel>();
            CreateMap<OrganizationCategory, OrganizationCategoryViewModel>();
        }
    }
}
