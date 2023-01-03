using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AccessLogModel
    {
        public int LogId { get; set; }
        public string Username { get; set; }
        public byte? Action { get; set; }
        public byte? Status { get; set; }
        public DateTime? LogDate { get; set; }
        public string Ip { get; set; }
    }
}
