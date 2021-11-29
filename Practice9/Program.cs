using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice9
{
    class Program
    {
        private readonly static string CspContainerName = "RsaContainer";
        public static byte[] SignData(byte[] dataToSign)
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
        public static void DeleteKeyInCsp()
        {
            CspParameters cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = false
            };
            rsa.Clear();
        }
        public static void GenerateKeys()
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
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
            DeleteKeyInCsp();
            GenerateKeys();
            string originalText = "Hello C#!";
            byte[] textinBytes = Encoding.UTF8.GetBytes(originalText);
            var signedText = SignData(textinBytes);
            var verifySignature = VerifySignature("Miroshnyk.xml", textinBytes, signedText);
        }
    }
}
