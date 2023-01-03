using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblMailConfig
    {
        public int Id { get; set; }
        public string Server { get; set; } = null!;
        public int? Port { get; set; }
        public bool? Ssl { get; set; }
        public int? Timeout { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool? Active { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
