using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Service.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string FullName { get; set; }

        public string StudentCode { get; set; }

        public DateTime? Birthday { get; set; }

        public string University { get; set; }

        public string ProfilePicture { get; set; }

        public virtual IEnumerable<UserOrganizationViewModel> UserOrganizations { get; set; }

        // Audit fields
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public int? StatusCode { get; set; }
    }
}
