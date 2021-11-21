using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice8
{
    class Program
    {
        private readonly static string CspContainerName = "RsaContainer";
        public static void GenerateKeys(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            }
        }
        public static void EncryptData(string publicKeyPath, byte[] dataToEncrypt, string chipherTextPath)
        {
            byte[] chipherBytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                chipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            File.WriteAllBytes(chipherTextPath, chipherBytes);
        }
        public static byte[] DecryptData(string chipherTextPath)
        {
            byte[] chipherBytes = File.ReadAllBytes(chipherTextPath);
            byte[] plainTextBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainTextBytes = rsa.Decrypt(chipherBytes, true);
            }
            return plainTextBytes;
        }
        static void Main(string[] args)
        {
            GenerateKeys("Miroshnyk.xml"); //generated public key and written to the XML File
            Console.WriteLine("Enter 'E' to encrypt the message or Enter 'D' to decrypt the message: ");
            string temp = Convert.ToString(Console.ReadLine());
            if (temp == "E")
            {
                Console.WriteLine("Enter message to encrypt: ");
                string message = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter the name of the recipient public key XML file [ex. MyKey.xml]: ");
                string recPublicKey = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter the file name where to encrypt the message [ex. MyData.dat]: ");
                string datFile = Convert.ToString(Console.ReadLine());
                EncryptData(recPublicKey, Encoding.UTF8.GetBytes(message), datFile);
                Console.WriteLine("Done! Your message was encrypted.");
            }
            else if (temp == "D")
            {
                Console.WriteLine("Enter the name of the file to decrypt [ex. MyData.dat]: ");
                string fileToDecrypt = Convert.ToString(Console.ReadLine());
                Console.WriteLine("----------------------");
                Console.WriteLine("Decrypted Message: " + Encoding.UTF8.GetString(DecryptData(fileToDecrypt)));
                Console.WriteLine("----------------------");
                Console.WriteLine("Done! The message was decrypted.");
            }
            else
            {
                Console.WriteLine("Incorrect Data! Please try again!");
            }
        }
    }
}
