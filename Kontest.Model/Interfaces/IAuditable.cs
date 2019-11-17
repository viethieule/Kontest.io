using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Model.Interfaces
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
        int? StatusCode { get; set; }
    }
}
