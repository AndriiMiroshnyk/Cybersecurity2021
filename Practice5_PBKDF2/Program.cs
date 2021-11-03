using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Practice5_PBKDF2
{
    class Program
    {
        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds, HashAlgorithmName.SHA256);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + "> Elapsed Time: " + sw.ElapsedMilliseconds + "ms");
        }
        static void Main(string[] args)
        {
            const string passwordToHash = "VeryC0mplexPassword";
            HashPassword(passwordToHash, 170000);
            HashPassword(passwordToHash, 220000);
            HashPassword(passwordToHash, 270000);
            HashPassword(passwordToHash, 320000);
            HashPassword(passwordToHash, 370000);
            HashPassword(passwordToHash, 420000);
            HashPassword(passwordToHash, 470000);
            HashPassword(passwordToHash, 520000);
            HashPassword(passwordToHash, 570000);
            HashPassword(passwordToHash, 620000);
            Console.ReadLine();
        }
    }
}
