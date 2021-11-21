using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice7_Task2
{
    class AsymmetricEncryption
    {
        const string ContainerName = "MyContainer";
        public static void AssignNewKeys(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = ContainerName,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }
        public static byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] chipherbytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                chipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            return chipherbytes;
        }
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plain;
            var cspParams = new CspParameters
            {
                KeyContainerName = ContainerName
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plain = rsa.Decrypt(dataToDecrypt, false);
            }
            return plain;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter message to encrypt: ");
            string originalmessage = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter 'New' to create new public key or Enter 'Existing' to select existing user public key: ");
            string temp = Convert.ToString(Console.ReadLine());
            if (temp == "New")
            {
                Console.WriteLine("Enter the name of your new public key XML file [ex. MyKey.xml]: ");
                string newPublicKey = Convert.ToString(Console.ReadLine());
                AsymmetricEncryption.AssignNewKeys(newPublicKey);
                var encrypted = AsymmetricEncryption.EncryptData(newPublicKey, Encoding.UTF8.GetBytes(originalmessage));
                var decrypted = AsymmetricEncryption.DecryptData(encrypted);
                Console.WriteLine("Original Text: " + originalmessage);
                Console.WriteLine("----------------------");
                Console.WriteLine("Encrypted Data: " + Convert.ToBase64String(encrypted));
                Console.WriteLine("----------------------");
                Console.WriteLine("Decrypted Data: " + Encoding.UTF8.GetString(decrypted));
            }
            else if (temp == "Existing")
            {
                Console.WriteLine("Enter the name of the existing user public key XML file [ex. MyKey.xml]: ");
                string exPublicKey = Convert.ToString(Console.ReadLine());
                var encrypted = AsymmetricEncryption.EncryptData(exPublicKey, Encoding.UTF8.GetBytes(originalmessage));
                Console.WriteLine("Original Text: " + originalmessage);
                Console.WriteLine("----------------------");
                Console.WriteLine("Encrypted Data: " + Convert.ToBase64String(encrypted));
            }
        }
    }
}
