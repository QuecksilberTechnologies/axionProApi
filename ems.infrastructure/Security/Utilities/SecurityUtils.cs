using System;
using System.Security.Cryptography;

namespace ems.infrastructure.Utilities
{
    public static class SecurityUtils
    {
        public static string GenerateSecureKey(int sizeInBytes)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[sizeInBytes];
                rng.GetBytes(key);
                return Convert.ToBase64String(key); // Returns a Base64-encoded string
            }
        }
    }
}
