using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commons
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Sử dụng BCrypt để băm mật khẩu
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Xác minh mật khẩu
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
