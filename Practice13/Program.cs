using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System;
using NLog.Config;
using NLog.Targets;
using NLog;

namespace Practice13
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("target1")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${callsite} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("target2")
            {
                FileName = "${basedir}/LogFile.log",
                Layout = "${longdate} ${level} ${callsite} ${message} ${exception}"
            };
            config.AddTarget(fileTarget);

            config.AddRuleForOneLevel(NLog.LogLevel.Warn, fileTarget);
            config.AddRuleForOneLevel(NLog.LogLevel.Error, fileTarget);
            config.AddRuleForOneLevel(NLog.LogLevel.Fatal, fileTarget);
            config.AddRuleForAllLevels(consoleTarget);

            LogManager.Configuration = config;

            Logger nLogger = LogManager.GetLogger("For Example");

            nLogger.Trace("Registration is started");
            Console.WriteLine("Registration of 4 users");
            string startLogin = "null";
            string startPass = "null";
            nLogger.Trace("Start cycle for users registration");
            for (int i = 0; i < 4; i++)
            {
                nLogger.Info("New user registration");
                Console.Write("Please enter your login: ");
                var login = Console.ReadLine();
                nLogger.Trace($"The variable 'login' changed from '{startLogin}' to '{login}'");
                Console.Write("Please enter your password: ");
                var pass = Console.ReadLine();
                nLogger.Trace($"The variable 'pass' changed from '{startPass}' to '{pass}'");
                try
                {
                    int startCounter = 0;
                    Console.Write("Please enter number of roles: ");
                    int counter = Convert.ToInt32(Console.ReadLine());
                    nLogger.Trace($"The variable 'counter' changed from '{startCounter}' to '{counter}'");
                    string[] roles = new string[counter];
                    nLogger.Trace("Initialized array of roles");
                    nLogger.Trace("Start cycle for entering roles");
                    for (int n = 0; n < counter; n++)
                    {
                        Console.Write("Please enter role: ");
                        roles[n] = Console.ReadLine();
                        nLogger.Trace("Entered role is added to the array");
                    }
                    nLogger.Trace("Calling Register method.");
                    Protector.Register(login, pass, roles);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    nLogger.Error(ex, "Entered wrong number of roles!");
                }
            }
            nLogger.Info("Registration is finished!");

            Console.WriteLine("Now you can log in");
            nLogger.Info("Start Logging In ");
            nLogger.Trace("Start cycle for logging in");
            while (true)
            {
                Console.Write("Please enter your login: ");
                var login = Console.ReadLine();
                nLogger.Trace("Entering login...");
                nLogger.Debug($"The variable 'login' changed from '{startLogin}' to '{login}'");
                Console.Write("Please enter your password: ");
                var pass = Console.ReadLine();
                nLogger.Trace("Entering password...");
                nLogger.Debug($"The variable 'pass' changed from '{startPass}' to '{pass}'");
                nLogger.Info("Checking password...");
                if (!Protector.CheckPassword(login, pass))
                {
                    nLogger.Fatal("Entered wrong password!");
                    continue;
                }
                nLogger.Trace("Calling LogIn method.");
                Protector.LogIn(login, pass);
                nLogger.Info("User is successfully logged in!");
                Console.WriteLine();
                nLogger.Trace("Calling CheckFeatures method.");
                Protector.CheckFeatures();
            }
        }
    }
}
