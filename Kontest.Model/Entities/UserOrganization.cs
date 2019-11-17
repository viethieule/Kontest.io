using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kontest.Infrastructure.SharedKernel;
using Kontest.Model.Interfaces;

namespace Kontest.Model.Entities
{
    [Table("UserOrganizations")]
    public class UserOrganization : Entity<int>, IAuditable
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        public int RoleTypeCode { get; set; }

        public DateTime AssignedDate { get; set; }

        [MaxLength(100)]
        public string AssignedBy { get; set; }

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
