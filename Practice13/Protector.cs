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
    class Protector
    {
        private static Logger nLogger = NLog.LogManager.GetCurrentClassLogger();
        private static Dictionary<string, User> _users = new Dictionary<string, User>();
        public static User Register(string userName, string password, string[] roles = null)
        {
            nLogger.Trace("Checking userName");
            if (_users.ContainsKey(userName))
            {
                Console.WriteLine("User already exists");
                nLogger.Warn("User already exists");
                return null;
            }
            else
            {
                User newUser = new User();
                nLogger.Trace("Created new instance of the class User()");
                byte[] salt = PBKDF2.GenerateSalt();
                nLogger.Trace("Generate Salt");
                byte[] hashedPassword = PBKDF2.HashPassword(Encoding.Default.GetBytes(password), salt, 170000, HashAlgorithmName.SHA512);
                nLogger.Trace("Hash password");
                newUser.Login = userName;
                newUser.Salt = Convert.ToBase64String(salt);
                newUser.PasswordHash = Convert.ToBase64String(hashedPassword);
                newUser.Roles = roles;
                _users.Add(userName, newUser);
                nLogger.Trace("NewUser added");
                Console.WriteLine("User is successfully registered!");
                return null;
            }
        }
        public static bool CheckPassword(string userName, string password)
        {
            nLogger.Trace("Checking Password");
            if (_users.ContainsKey(userName))
            {
                User enteredUser = _users[userName];
                byte[] userEnteredPassword = PBKDF2.HashPassword(Encoding.Default.GetBytes(password), Convert.FromBase64String(enteredUser.Salt), 170000, HashAlgorithmName.SHA512);
                if (Convert.ToBase64String(userEnteredPassword) == enteredUser.PasswordHash)
                {
                    nLogger.Info("Entered right password!");
                    return true;
                }
                else
                {
                    Console.WriteLine("You entered wrong password! Please try again!");
                    nLogger.Warn("Entered wrong password!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("You entered wrong login! Please try again!");
                nLogger.Warn("Entered wrong login!");
                return false;
            }
        }
        public static void LogIn(string userName, string password)
        {
            nLogger.Trace("Process of Log in");
            if (CheckPassword(userName, password))
            {
                var identity = new GenericIdentity(userName, "OIBAuth");
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                nLogger.Debug($"Assigning {principal} to CurrentPrincipal");
                System.Threading.Thread.CurrentPrincipal = principal;
                Console.WriteLine("You are successfully logged in!");
            }
        }
        public static void CheckFeatures()
        {
            nLogger.Trace("Checking User Principal");
            if (Thread.CurrentPrincipal == null)
            {
                nLogger.Fatal("Thread.CurrentPrincipal cannot be null");
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            if (Thread.CurrentPrincipal.IsInRole("Admins"))
            {
                Console.WriteLine("You have access to this secure feature.");
                nLogger.Info("Giving access to group 'Admins");
            }
            else
            {
                Console.WriteLine("Access to the secure feature is denied.");
                nLogger.Warn("Denying access to group 'Admins'");
            }
            if (Thread.CurrentPrincipal.IsInRole("Owners"))
            {
                Console.WriteLine("You have access to this feature for Owners.");
                nLogger.Info("Giving access to group 'Owners");
            }
            else
            {
                Console.WriteLine("Access to the Owners feature is denied.");
                nLogger.Warn("Denying access to group 'Owners'");
            }
            if (Thread.CurrentPrincipal.IsInRole("Managers"))
            {
                Console.WriteLine("You have access to this feature for Managers.");
                nLogger.Info("Giving access to group 'Managers");
            }
            else
            {
                Console.WriteLine("Access to the Managers feature is denied.");
                nLogger.Warn("Denying access to group 'Managers'");
            }
            if (Thread.CurrentPrincipal.IsInRole("Guests"))
            {
                Console.WriteLine("You have access to this feature for Guests.");
                nLogger.Info("Giving access to group 'Guests");
            }
            else
            {
                Console.WriteLine("Access to the Guests feature is denied.");
                nLogger.Warn("Denying access to group 'Guests'");
            }
        }
    }
}