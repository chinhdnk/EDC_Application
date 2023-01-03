using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblPermission
    {
        public TblPermission()
        {
            Groups = new HashSet<TblGroup>();
            Usernames = new HashSet<TblUser>();
        }

        public string PermId { get; set; } = null!;
        public string? Title { get; set; }
        public bool Status { get; set; }
        public int Menu { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TblMenu MenuNavigation { get; set; } = null!;

        public virtual ICollection<TblGroup> Groups { get; set; }
        public virtual ICollection<TblUser> Usernames { get; set; }
    }
}
