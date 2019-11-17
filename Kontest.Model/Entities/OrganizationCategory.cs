using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kontest.Infrastructure.SharedKernel;
using Kontest.Model.Interfaces;

namespace Kontest.Model.Entities
{
    [Table("OrganizationCategories")]
    public class OrganizationCategory : Entity<int>, IAuditable
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set;}

        public virtual IEnumerable<Organization> Organizations { get; set; }

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
