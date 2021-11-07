using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice6_Task2
{
    class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, Int32 NumberOfBytes)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(NumberOfBytes);
            }
        }
    }
    class aesChipher
    {
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            const string original = "C0mplexText";
            var aes = new aesChipher();
            var aeskey = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(original), PBKDF2.GenerateSalt(), 170000, HashAlgorithmName.SHA256, 32);
            var aesiv = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(original), PBKDF2.GenerateSalt(), 170000, HashAlgorithmName.SHA256, 16);
            var encrypted = aes.Encrypt(Encoding.UTF8.GetBytes(original), aeskey, aesiv);
            var decrypted = aes.Decrypt(encrypted, aeskey, aesiv);
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            Console.WriteLine("AES Encryption");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text = " + decryptedMessage);
            Console.ReadKey();
        }
    }
}
