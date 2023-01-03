using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblProjectText
    {
        public string ProjectCode { get; set; } = null!;
        public string Lang { get; set; } = null!;
        public string? ShortName { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TblProject ProjectCodeNavigation { get; set; } = null!;
    }
}
