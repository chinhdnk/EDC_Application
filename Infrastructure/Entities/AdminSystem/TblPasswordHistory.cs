using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblPasswordHistory
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual TblUser UsernameNavigation { get; set; } = null!;
    }
}
