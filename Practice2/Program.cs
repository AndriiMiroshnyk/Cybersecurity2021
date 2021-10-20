using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Practice2
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] decData = File.ReadAllBytes("MyData.txt").ToArray();

            var generator = new RNGCryptoServiceProvider();
            byte[] key = new byte[decData.Length];
            generator.GetBytes(key);

            byte[] enData = new byte[decData.Length];
            for (int i = 0; i < decData.Length; i++)
            {
                enData[i] = (byte)(decData[i] ^ key[i]);
            }
            File.WriteAllBytes("MyData.dat", enData);
            byte[] DataToDecrypt = File.ReadAllBytes("MyData.dat").ToArray();
            byte[] decryptData = new byte[DataToDecrypt.Length];
            for (int i = 0; i < DataToDecrypt.Length; i++)
            {
                decryptData[i] = (byte)(DataToDecrypt[i] ^ key[i]);
            }
            Console.Write("Our decrypted information from file: " + Encoding.UTF8.GetString(decryptData));
            Console.WriteLine();
        }
    }
}