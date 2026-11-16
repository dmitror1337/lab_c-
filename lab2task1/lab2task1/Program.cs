using System;
using System.Security.Cryptography;
using System.Text;

namespace HashCompareApp
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            string text1 = "Hello World!";
            string text2 = "Hello World!";
            string text3 = "Hello World?";

            string[] texts = { text1, text2, text3 };

            for (int i = 0; i < texts.Length; i++)
            {
                Console.WriteLine("Дані " + (i + 1) + ": " + texts[i]);

                byte[] data = Encoding.UTF8.GetBytes(texts[i]);

                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(data);
                    Console.WriteLine("MD5: " + Convert.ToBase64String(hash));
                }

                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] hash = sha1.ComputeHash(data);
                    Console.WriteLine("SHA1: " + Convert.ToBase64String(hash));
                }

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(data);
                    Console.WriteLine("SHA256: " + Convert.ToBase64String(hash));
                }

                using (SHA384 sha384 = SHA384.Create())
                {
                    byte[] hash = sha384.ComputeHash(data);
                    Console.WriteLine("SHA384: " + Convert.ToBase64String(hash));
                }

                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] hash = sha512.ComputeHash(data);
                    Console.WriteLine("SHA512: " + Convert.ToBase64String(hash));
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}