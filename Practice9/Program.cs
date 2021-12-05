using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice9
{
    class Program
    {
        private readonly static string CspContainerName = "RsaContainer";
        public static void GenerateKeys(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            }
        }
        public static byte[] SignData(byte[] dataToSign)
        {
            CspParameters cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm(nameof(SHA512));
                byte[] hashofData;
                using (var sha512 = SHA512.Create())
                {
                    hashofData = sha512.ComputeHash(dataToSign);
                }
                return rsaFormatter.CreateSignature(hashofData);
            }
        }
        public static bool VerifySignature(string publicKey, byte[] dataToSign, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKey));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(nameof(SHA512));
                byte[] hashofData;
                using (var sha512 = SHA512.Create())
                {
                    hashofData = sha512.ComputeHash(dataToSign);
                }
                return rsaDeformatter.VerifySignature(hashofData, signature);
            }
        }
        static void Main(string[] args)
        {
            string originalText = "Hello C#";
            byte[] textinBytes = Encoding.UTF8.GetBytes(originalText);
            GenerateKeys("Miroshnyk.xml");
            var signedData = SignData(textinBytes);
            var verifySignature = VerifySignature("Miroshnyk.xml", textinBytes, signedData);
            if (verifySignature)
            {
                Console.WriteLine("The Digital Signature is verified");
            }
            else
            {
                Console.WriteLine("The Digital Signature is not verified"); 
            }
        }
    }
}
