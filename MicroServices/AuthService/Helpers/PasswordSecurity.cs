using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthService.Helpers
{
    public class PasswordSecurity
    {
        /// <summary>
        /// Create Password hash
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <param name="passwordHash">Get Password Hashed</param>
        /// <param name="passwordSalt">Get Password Salt</param>
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (HMACSHA256 hmac = new HMACSHA256())
            {
                //Get auto generate salt key
                passwordSalt = hmac.Key;
                //compute new hash password
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verify string password with stored password
        /// </summary>
        /// <param name="password">string input password</param>
        /// <param name="storedHash">password hashed in DB</param>
        /// <param name="storedSalt">password salt in DB</param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 32) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 64) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));

            using (HMACSHA256 hmac = new HMACSHA256(storedSalt))
            {
                //Generate hash from string inputed password
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                //check every byte to verify if inputed password matches with stored password
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
