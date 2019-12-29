using Kontest.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Service.ViewModels
{
    public class UserOrganizationViewModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        
        public UserViewModel User { get; set; }

        public int OrganizationId { get; set; }
        
        public virtual OrganizationViewModel Organization { get; set; }

        public OrgnizationUserRoleType OrgnizationUserRoleType { get; set; }

        public DateTime AssignedDate { get; set; }

        public string AssignedBy { get; set; }

        // Audit fields
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public int? StatusCode { get; set; }
    }
}
