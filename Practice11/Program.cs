using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Practice11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Registration of 4 users");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("New user registration");
                Console.Write("Please enter your login: ");
                var login = Console.ReadLine();
                Console.Write("Please enter your password: ");
                var pass = Console.ReadLine();
                Console.Write("Please enter number of roles: ");
                int counter = Convert.ToInt32(Console.ReadLine());
                string[] roles = new string[counter];
                for (int n = 0; n < counter; n++)
                {
                    Console.Write("Please enter role: ");
                    roles[n] = Console.ReadLine();
                }
                Protector.Register(login, pass, roles);
                Console.WriteLine();
            }
            Console.WriteLine("Now you can log in");
            while (true)
            {
                Console.Write("Please enter your login: ");
                var login = Console.ReadLine();
                Console.Write("Please enter your password: ");
                var pass = Console.ReadLine();
                if (!Protector.CheckPassword(login, pass))
                {
                    Console.WriteLine("Enter correct password!");
                    continue;
                }
                Protector.LogIn(login, pass);
                Protector.CheckFeatures();
            }
        }
    }
}
