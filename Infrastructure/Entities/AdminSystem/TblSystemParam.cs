using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblSystemParam
    {
        public int SysParamId { get; set; }
        public string Category { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Description { get; set; }
        public string? Lang { get; set; }
        public string? ExpandedValue { get; set; }
        public bool Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
