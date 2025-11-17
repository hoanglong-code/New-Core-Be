using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commons
{
    public class PasswordHasher
    {
        public static string AesEncryption(string plainText, string secretKey)
        {
            byte[] key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(secretKey));
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.GenerateIV();
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using var msEncrypt = new MemoryStream();
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length); // prepend IV
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                    swEncrypt.Write(plainText);

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        public static string AesDecryption(string cipherText, string secretKey)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            byte[] key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(secretKey));

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(cipher);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }

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
