using Kontest.Infrastructure.SharedKernel;
using Kontest.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontest.Model.Entities
{
    [Table("Organizations")]
    public class Organization : Entity<int>, IAuditable
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Alias { get; set; }

        public string Description { get; set; }

        public string ProfilePicture { get; set; }

        public int? OrganizationRequestId { get; set; }

        [ForeignKey("OrganizationRequestId")]
        public virtual OrganizationRequest OrganizationRequest { get; set; }

        public virtual IEnumerable<UserOrganization> UserOrganizations { get; set; }

        public virtual IEnumerable<Event> Events { get; set; }

        // Audit fields
        public DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public int? StatusCode { get; set; }
    }
}
