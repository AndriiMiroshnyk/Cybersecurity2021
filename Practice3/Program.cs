using System;
using System.Security.Cryptography;
using System.Text;

namespace Practice3
{
    class Program
    {
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }
        public static byte[] ComputeHashSha1(byte[] toBeHashed)
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha384(byte[] toBeHashed)
        {
            using (var sha384 = SHA384.Create())
            {
                return sha384.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(toBeHashed);
            }
        }
        static void Main(string[] args)
        {
            const string strForHash1 = "Hello World!";
            const string strForHash2 = "Hello World!";
            const string strForHash3 = "Hello world!";

            var md5ForString1 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash1));
            var md5ForString2 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash2));
            var md5ForString3 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash3));

            Guid guid1 = new Guid(md5ForString1),
            guid2 = new Guid(md5ForString2),
            guid3 = new Guid(md5ForString3);

            Console.WriteLine("String: " + strForHash1);
            Console.WriteLine("Hash MD5: " + Convert.ToBase64String(md5ForString1));
            Console.WriteLine("Guid: " + guid1);

            Console.WriteLine("String: " + strForHash2);
            Console.WriteLine("Hash MD5: " + Convert.ToBase64String(md5ForString2));
            Console.WriteLine("Guid: " + guid2);

            Console.WriteLine("String: " + strForHash3);
            Console.WriteLine("Hash MD5: " + Convert.ToBase64String(md5ForString3));
            Console.WriteLine("Guid: " + guid3);

            Console.WriteLine();

            var sha1ForString1 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash1));
            var sha1ForString2 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash2));
            var sha1ForString3 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine("String: " + strForHash1);
            Console.WriteLine("Hash SHA1: " + Convert.ToBase64String(sha1ForString1));

            Console.WriteLine("String: " + strForHash2);
            Console.WriteLine("Hash SHA1: " + Convert.ToBase64String(sha1ForString2));

            Console.WriteLine("String: " + strForHash3);
            Console.WriteLine("Hash SHA1: " + Convert.ToBase64String(sha1ForString3));

            Console.WriteLine();

            var sha256ForString1 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash1));
            var sha256ForString2 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash2));
            var sha256ForString3 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine("String: " + strForHash1);
            Console.WriteLine("Hash SHA256: " + Convert.ToBase64String(sha256ForString1));

            Console.WriteLine("String: " + strForHash2);
            Console.WriteLine("Hash SHA256: " + Convert.ToBase64String(sha256ForString2));

            Console.WriteLine("String: " + strForHash3);
            Console.WriteLine("Hash SHA256: " + Convert.ToBase64String(sha256ForString3));

            Console.WriteLine();

            var sha384ForString1 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash1));
            var sha384ForString2 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash2));
            var sha384ForString3 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine("String: " + strForHash1);
            Console.WriteLine("Hash SHA384: " + Convert.ToBase64String(sha384ForString1));

            Console.WriteLine("String: " + strForHash2);
            Console.WriteLine("Hash SHA384: " + Convert.ToBase64String(sha384ForString2));

            Console.WriteLine("String: " + strForHash3);
            Console.WriteLine("Hash SHA384: " + Convert.ToBase64String(sha384ForString3));

            Console.WriteLine();

            var sha512ForString1 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash1));
            var sha512ForString2 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash2));
            var sha512ForString3 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine("String: " + strForHash1);
            Console.WriteLine("Hash SHA512: " + Convert.ToBase64String(sha512ForString1));

            Console.WriteLine("String: " + strForHash2);
            Console.WriteLine("Hash SHA512: " + Convert.ToBase64String(sha512ForString2));

            Console.WriteLine("String: " + strForHash3);
            Console.WriteLine("Hash SHA512: " + Convert.ToBase64String(sha512ForString3));
        }
    }
}
