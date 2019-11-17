using Kontest.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontest.Model.Entities
{
    [Table("ApplicationRoles")]
    public class ApplicationRole : IdentityRole<Guid>, IAuditable
    {
        public string Description { get; set; }

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
