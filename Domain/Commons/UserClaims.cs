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
        public string access_key { get; set; } = string.Empty;
        public long userId { get; set; } = 0;
        public string userName { get; set; } = string.Empty;
        public long userMapId { get; set; } = 0;
        public string fullName { get; set; } = string.Empty;
        public int roleMax { get; set; } = 9999;
        public int roleLevel { get; set; } = 9999;
        public int type { get; set; } = 0;
        public string Token { get; set; }
    }
}
