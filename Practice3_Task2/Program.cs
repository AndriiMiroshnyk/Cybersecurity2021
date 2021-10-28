using System;
using System.Security.Cryptography;
using System.Text;

namespace Practice3_Task2
{
    class Program
    {
        public static string GetRandomPassword(int length)
        {
            const string chars = "0123456789";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }
            return sb.ToString();
        }
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }
        static void Main(string[] args)
        {
            Guid guid = new Guid("564c8da6-0440-88ec-d453-0bbad57c6036");
            const string MyHash = "po1MVkAE7IjUUwu61XxgNg==";
            while (true)
            {
                string password = GetRandomPassword(8);
                var md5hashedpassword = Convert.ToBase64String(ComputeHashMd5(Encoding.Unicode.GetBytes(password)));
                Console.WriteLine(md5hashedpassword);
                if (MyHash == md5hashedpassword)
                {
                    Console.WriteLine(md5hashedpassword);
                    Console.WriteLine();
                    Console.WriteLine(password);
                    break;
                }
            }
        }
    }
}