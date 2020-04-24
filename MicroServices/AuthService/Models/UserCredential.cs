using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class UserAuthentication
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public Guid ValidationToken { get; set; }
        public byte? TypeID { get; set; }
        public byte AuthenticationStatus { get; set; }
        public Guid CustomerID { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? Active { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
