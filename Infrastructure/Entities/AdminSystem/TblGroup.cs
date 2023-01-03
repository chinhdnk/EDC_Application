using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblGroup
    {
        public TblGroup()
        {
            Perms = new HashSet<TblPermission>();
            Usernames = new HashSet<TblUser>();
        }

        public int GroupId { get; set; }
        public string Title { get; set; } = null!;
        public bool Status { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TblPermission> Perms { get; set; }
        public virtual ICollection<TblUser> Usernames { get; set; }
    }
}
