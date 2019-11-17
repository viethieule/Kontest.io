using Kontest.Infrastructure.SharedKernel;
using Kontest.Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontest.Model.Entities
{
    [Table("Events")]
    public class Event : Entity<int>, IAuditable
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public DateTime? DateOrganize { get; set; }
        public string Description { get; set; }
        
        [Required]
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

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
