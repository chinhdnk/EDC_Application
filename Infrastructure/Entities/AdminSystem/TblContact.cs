using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblContact
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Title { get; set; }
        public string? Office { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public bool? Active { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
