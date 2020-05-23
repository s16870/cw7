using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public static class PasswordHelper
    {
        public static bool CheckPswd(string requestPswd, string pswd, byte[] salt)
        {
            var receivedPasswordHash = GenerateSaltedHash(requestPswd, salt);
            return pswd.Equals(receivedPasswordHash);
        }
        public static string GenerateSaltedHash(string pswd, byte[] salt)
        {

            var hashValue = GenerateHashValue(pswd, salt);
            return Convert.ToBase64String(hashValue);
        }

        private static byte[] GenerateHashValue(string pswd, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(pswd, salt, 20000);
            return pbkdf2.GetBytes(32);
        }
    }
}
