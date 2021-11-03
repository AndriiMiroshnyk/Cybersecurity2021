using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Practice5_Registration
{
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }
    class Program
    {
        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        static void Main(string[] args)
        {
            byte[] Salt = PBKDF2.GenerateSalt();
            Console.Write("Please register your account");
            Console.WriteLine();
            Console.Write("Please enter your login: ");
            var userLogin = Console.ReadLine();
            var hashedUserLogin = Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(userLogin)));
            Console.Write("Please enter your password: ");
            var userPassword = Console.ReadLine();
            var hashedUserPassword = Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(Convert.ToString(userPassword)), Salt, 170000));
            Console.WriteLine("You are successfully registered!");

            Console.Write("Please log in to your account");
            Console.WriteLine();
            Console.Write("Please enter your login: ");
            var login = Console.ReadLine();
            var hashedLogin = Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(login)));
            Console.Write("Please enter your password: ");
            var password = Console.ReadLine();
            var hashedPassword = Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(Convert.ToString(password)), Salt, 170000));

            if (hashedLogin != hashedUserLogin || password != userPassword)
            {
                Console.WriteLine("Incorrect data! Please try again!");
            }
            else
            {
                Console.WriteLine("Successful login. Welcome to the system!");
            }

        }
    }
}