using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Application.Data
{
    public class LoginResponse
    {
        public int UsertId { get; set; }
        public required string AccessToken { get; set; }
    }
}
