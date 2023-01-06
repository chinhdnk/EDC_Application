using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MPhone { get; set; }
        public string OPhone { get; set; }
        public string ESignature { get; set; }
        public string ESignatureLink { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Institution { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileImageLink { get; set; }
    }
}
