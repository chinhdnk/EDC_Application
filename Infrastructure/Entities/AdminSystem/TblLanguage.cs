using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblLanguage
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Flag { get; set; }
        public bool? Active { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
