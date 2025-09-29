using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commons
{
    public class JwtSettings
    {
        private readonly IConfiguration _configuration;

        public JwtSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Key => _configuration["Jwt:Key"];
        public string Issuer => _configuration["Jwt:Issuer"];
        public string Audience => _configuration["Jwt:Audience"];
    }
}
