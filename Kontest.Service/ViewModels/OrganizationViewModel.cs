using Kontest.Model.Enums;
using System;
using System.Collections.Generic;

namespace Kontest.Service.ViewModels
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public string ProfilePicture { get; set; }

        public int? OrganizationRequestId { get; set; }

        public virtual OrganizationRequestViewModel OrganizationRequest { get; set; }

        public virtual IEnumerable<UserOrganizationViewModel> UserOrganizations { get; set; }

        public virtual IEnumerable<EventViewModel> Events { get; set; }

        // Audit fields
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}