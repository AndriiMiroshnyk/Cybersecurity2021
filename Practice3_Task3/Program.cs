using System;
using System.Security.Cryptography;
using System.Text;

namespace Practice3_Task3
{
    class Program
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] ComputeHmacsha256(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
        static void Main(string[] args)
        {
            const string strForHMAC = "Hello World!";
            const string strForObtainedHMAC = "Hello W0rld!";
            string randomNumber = Convert.ToBase64String(GenerateRandomNumber(32));
            Console.WriteLine("Random number = " + randomNumber);
            var HMACString = ComputeHmacsha256(Encoding.Unicode.GetBytes(strForHMAC), Encoding.Unicode.GetBytes(randomNumber));
            Console.WriteLine("HMAC: " + Convert.ToBase64String(HMACString));

            //Checking HMAC
            var obtainedHMACString = ComputeHmacsha256(Encoding.Unicode.GetBytes(strForObtainedHMAC), Encoding.Unicode.GetBytes(randomNumber));
            Console.WriteLine("Obtained HMAC: " + Convert.ToBase64String(obtainedHMACString));
            if (Convert.ToBase64String(HMACString) == Convert.ToBase64String(obtainedHMACString))
            {
                Console.WriteLine("The message is authentic!");
            }
            else
            {
                Console.WriteLine("The message is not authentic!");
            }
        }
    }
}
