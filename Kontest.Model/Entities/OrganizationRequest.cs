using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kontest.Infrastructure.SharedKernel;
using Kontest.Model.Interfaces;

namespace Kontest.Model.Entities
{
    [Table("OrganizationRequests")]
    public class OrganizationRequest : Entity<int>, IAuditable
    {
        [Required]
        [MaxLength(256)]
        public string OrganizationName { get; set; }

        public int OrganizationCategoryId { get; set; }

        [ForeignKey("OrganizationCategoryId")]
        public virtual OrganizationCategory OrganizationCategory { get; set; }

        public int RequestingUserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser RequestingUser { get; set; }

        public int? CreatedOrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization CreatedOrganization { get; set; }

        public int ActionTypeCode { get; set; }

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
