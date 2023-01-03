using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblProjectVersion
    {
        public string ProjectCode { get; set; } = null!;
        public int VersionId { get; set; }
        public string VersionCode { get; set; } = null!;
        public string? Description { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TblProject ProjectCodeNavigation { get; set; } = null!;
    }
}
