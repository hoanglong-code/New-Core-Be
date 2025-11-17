using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public class UserClaims
    {
        public ClaimsPrincipal? User { get; set; }
        public ClaimsIdentity? Identity { get; set; }
        public int userId { get; set; } = 0;
        public string userName { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string avatar { get; set; } = string.Empty;
        public string birthday { get; set; } = string.Empty;
        public string accessKey { get; set; } = string.Empty;
    }
}
