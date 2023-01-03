using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblPasswordHistories = new HashSet<TblPasswordHistory>();
            Groups = new HashSet<TblGroup>();
            Perms = new HashSet<TblPermission>();
        }

        public string Username { get; set; } = null!;
        public string? MPhone { get; set; }
        public string? OPhone { get; set; }
        public string? ESignature { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Institution { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ProfileImage { get; set; }

        public virtual ICollection<TblPasswordHistory> TblPasswordHistories { get; set; }

        public virtual ICollection<TblGroup> Groups { get; set; }
        public virtual ICollection<TblPermission> Perms { get; set; }
    }
}
