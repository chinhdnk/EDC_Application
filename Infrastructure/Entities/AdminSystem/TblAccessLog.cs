using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblAccessLog
    {
        public int LogId { get; set; }
        public string? Username { get; set; }
        public byte? Action { get; set; }
        public byte? Status { get; set; }
        public DateTime? LogDate { get; set; }
        public string? Ip { get; set; }
    }
}
