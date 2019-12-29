using System;

namespace Kontest.Service.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? DateOrganize { get; set; }
        public string Description { get; set; }

        public int OrganizationId { get; set; }

        public virtual OrganizationViewModel Organization { get; set; }

        // Audit fields
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public int? StatusCode { get; set; }
    }
}