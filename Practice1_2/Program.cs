using System;
using System.Security.Cryptography;

namespace Practice1_2
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
        static void Main(string[] args)
        {
            for (int i = 1; i <= 20; i++)
            {
                string randomNumber = Convert.ToBase64String(GenerateRandomNumber(32));
                Console.WriteLine(randomNumber);
            }
            Console.ReadLine();
        }
    }
}
