using Kontest.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontest.Model.Entities
{
    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser<Guid>, IAuditable
    {
        [MaxLength(256)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string StudentCode { get; set; }

        public DateTime? Birthday { get; set; }

        public string University { get; set; }

        public string ProfilePicture { get; set; }

        public virtual IEnumerable<UserOrganization> UserOrganizations { get; set; }

        public string OTAC { get; set; }
        public DateTime? OTACExpires { get; set; }

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
