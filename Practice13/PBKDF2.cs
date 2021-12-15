using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Practice13
{
    class PBKDF2
    {
        private static Logger nLogger = NLog.LogManager.GetCurrentClassLogger();
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                nLogger.Trace("Generating Salt");
                return randomNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA512))
            {
                nLogger.Trace("Hashing Password");
                return rfc2898.GetBytes(20);
            }
        }
    }
}