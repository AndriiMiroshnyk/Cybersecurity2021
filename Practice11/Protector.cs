using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Practice11
{
    class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();
        public static User Register(string userName, string password, string[] roles = null)
        {
            if (_users.ContainsKey(userName))
            {
                Console.WriteLine("User already exists");
                return null;
            }
            else
            {
                User newUser = new User();
                byte[] salt = PBKDF2.GenerateSalt();
                byte[] hashedPassword = PBKDF2.HashPassword(Encoding.Default.GetBytes(password), salt, 170000, HashAlgorithmName.SHA512);
                newUser.Login = userName;
                newUser.Salt = Convert.ToBase64String(salt);
                newUser.PasswordHash = Convert.ToBase64String(hashedPassword);
                newUser.Roles = roles;
                _users.Add(userName, newUser);
                Console.WriteLine("User is successfully registered!");
                return null;
            }
        }
        public static bool CheckPassword(string userName, string password)
        {
            if (_users.ContainsKey(userName))
            {
                User enteredUser = _users[userName];
                byte[] userEnteredPassword = PBKDF2.HashPassword(Encoding.Default.GetBytes(password), Convert.FromBase64String(enteredUser.Salt), 170000, HashAlgorithmName.SHA512);
                if (Convert.ToBase64String(userEnteredPassword) == enteredUser.PasswordHash)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("You entered wrong password! Please try again!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("You entered wrong login! Please try again!");
                return false;
            }
        }
        public static void LogIn(string userName, string password)
        {
            if (CheckPassword(userName, password))
            {
                var identity = new GenericIdentity(userName, "OIBAuth");
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                System.Threading.Thread.CurrentPrincipal = principal;
                Console.WriteLine("You are successfully logged in!");
            }
        }
        public static void CheckFeatures()
        {
            if (Thread.CurrentPrincipal == null)
            {
                Console.WriteLine("Thread.CurrentPrincipal cannot be null!");
            }
            if (Thread.CurrentPrincipal.IsInRole("Admins"))
            {
                Console.WriteLine("You have access to this secure feature.");
            }
            if (Thread.CurrentPrincipal.IsInRole("Owners"))
            {
                Console.WriteLine("You have access to this feature for Owners.");
            }
            if (Thread.CurrentPrincipal.IsInRole("Managers"))
            {
                Console.WriteLine("You have access to this feature for Managers.");
            }
            if (Thread.CurrentPrincipal.IsInRole("Guests"))
            {
                Console.WriteLine("You have access to this feature for Guests.");
            }
        }
    }
}
