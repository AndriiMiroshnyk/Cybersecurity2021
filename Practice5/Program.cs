using System;
using System.Text;

namespace Practice5
{
    class Program
    {
        static void Main(string[] args)
        {
            const string password = "TestPASSword";
            byte[] salt = SaltedHash.GenerateSalt();
            Console.WriteLine("Password : " + password);
            Console.WriteLine("Salt = " + Convert.ToBase64String(salt));
            Console.WriteLine();
            var hashedPassword1 = SaltedHash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);
            Console.WriteLine("Hashed Password = " + Convert.ToBase64String(hashedPassword1));
            Console.ReadLine();
        }
    }
}
