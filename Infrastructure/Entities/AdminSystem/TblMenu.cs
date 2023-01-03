using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblMenu
    {
        public TblMenu()
        {
            TblPermissions = new HashSet<TblPermission>();
        }

        public int MenuId { get; set; }
        public string Title { get; set; } = null!;
        public string? Link { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }
        public bool Status { get; set; }
        public string? Icon { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TblPermission> TblPermissions { get; set; }
    }
}
