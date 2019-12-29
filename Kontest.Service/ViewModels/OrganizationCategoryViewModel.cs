using System;
using System.Collections.Generic;

namespace Kontest.Service.ViewModels
{
    public class OrganizationCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<OrganizationViewModel> Organizations { get; set; }

        // Audit fields
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public int? StatusCode { get; set; }
    }
}