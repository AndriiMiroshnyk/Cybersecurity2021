using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Practice3_Task4
{
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
            List<string> logins = new List<string>();
            List<string> passwords = new List<string>();

            while (true) { 
                Console.Write("Please enter 'Log in' to Log in and 'Sign up' to Sign up");
                Console.WriteLine();
                var temp = Console.ReadLine();
                if (temp == "Log in")
                {
                    Console.Write("Please enter your login: ");
                    var login = Console.ReadLine();
                    var sha256ForLogin = Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(login)));
                    if (logins.Contains(sha256ForLogin))
                    {
                        var index = logins.IndexOf(sha256ForLogin);
                        Console.Write("Please enter your password: ");
                        var password = Console.ReadLine();
                        var sha256ForPassword = Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(password)));
                        if (passwords[index] == sha256ForPassword)
                        {
                            Console.WriteLine("Successful login. Welcome to the system!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password. Please try again!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong login. Please try again!");
                    }
                }
                else if (temp == "Sign up")
                {
                    Console.Write("Please enter your login: ");
                    var login = Console.ReadLine();
                    var sha256ForLogin = Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(login)));
                    if (!logins.Contains(sha256ForLogin))
                    {
                        logins.Add(sha256ForLogin);
                        Console.Write("Please enter your password: ");
                        var password = Console.ReadLine();
                        var sha256ForPassword = Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(password)));
                        passwords.Add(sha256ForPassword);
                        Console.WriteLine("You are successfully registered!");
                    }
                    else
                    {
                        Console.WriteLine("Login is already in the database!");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter correct information!");
                }
            }
        }
    }
}
