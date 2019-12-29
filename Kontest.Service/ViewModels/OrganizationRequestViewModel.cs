using System;

namespace Kontest.Service.ViewModels
{
    public class OrganizationRequestViewModel
    {
        public int Id;

        public string OrganizationName { get; set; }

        public int OrganizationCategoryId { get; set; }

        public virtual OrganizationCategoryViewModel OrganizationCategory { get; set; }

        public Guid RequestingUserId { get; set; }

        public virtual UserViewModel RequestingUser { get; set; }

        public int? CreatedOrganizationId { get; set; }

        public virtual OrganizationViewModel CreatedOrganization { get; set; }

        public int ActionTypeCode { get; set; }

        // Audit fields
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public int? StatusCode { get; set; }
    }
}