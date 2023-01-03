using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblUserProject
    {
        public string Username { get; set; } = null!;
        public string ProjectCode { get; set; } = null!;
        public string? Role { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual TblAccount UsernameNavigation { get; set; } = null!;
    }
}
