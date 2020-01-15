using AutoMapper;
using Kontest.Model.Entities;
using Kontest.Service.ViewModels;

namespace Kontest.Service.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserOrganizationViewModel, UserOrganization>();
            CreateMap<UserViewModel, ApplicationUser>();
            CreateMap<OrganizationViewModel, Organization>();
            CreateMap<EventViewModel, Event>();
            CreateMap<OrganizationRequestViewModel, OrganizationRequest>();
            CreateMap<OrganizationCategoryViewModel, OrganizationCategory>();
        }
    }
}
