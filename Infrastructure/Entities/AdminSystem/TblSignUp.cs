using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.AdminSystem
{
    public partial class TblSignUp
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? MPhone { get; set; }
        public string? OPhone { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Institution { get; set; }
        public int Status { get; set; }
        public string? RegProject { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
