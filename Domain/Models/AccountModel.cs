using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AccountModel
    {
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public DateTime? PasswordDate { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Status { get; set; }
        public bool? OnLogin { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? WrongTime { get; set; }
        public DateTime? ResetPwDate { get; set; }
        public string? ResetPwKey { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string Salt { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }

    
}
