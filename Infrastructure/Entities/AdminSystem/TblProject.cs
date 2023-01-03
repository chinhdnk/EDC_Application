using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblProject
    {
        public TblProject()
        {
            TblProjectTexts = new HashSet<TblProjectText>();
            TblProjectVersions = new HashSet<TblProjectVersion>();
        }

        public string ProjectCode { get; set; } = null!;
        public string ProjectGroup { get; set; } = null!;
        public DateTime? ApprovedSdate { get; set; }
        public DateTime? ApprovedEdate { get; set; }
        public DateTime? ActualEdate { get; set; }
        public string? ProjectType { get; set; }
        public string? RegNumber { get; set; }
        public DateTime? RegDate { get; set; }
        public string? Sponsor { get; set; }
        public string? Funder { get; set; }
        public string? ChiefPi { get; set; }
        public string? Collaborators { get; set; }
        public int SiteSize { get; set; }
        public string SupportedLang { get; set; } = null!;
        public string DefaultLang { get; set; } = null!;
        public int Status { get; set; }
        public string? Responsibility { get; set; }
        public string? CustomPage { get; set; }
        public string? SystemParameter { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TblProjectText> TblProjectTexts { get; set; }
        public virtual ICollection<TblProjectVersion> TblProjectVersions { get; set; }
    }
}
