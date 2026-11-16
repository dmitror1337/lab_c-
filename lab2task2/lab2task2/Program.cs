using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string targetHash = "41664ae3-8eeb-be01-f730-cdb5e6f6ce4e";

        for (int number = 0; number <= 99999999; number++)
        {
            string password = number.ToString("D8");
            byte[] data = Encoding.UTF8.GetBytes(password);

            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(data);
                string result = new Guid(hash).ToString();

                if (result == targetHash)
                {
                    Console.WriteLine("Пароль знайдено: " + password);
                    return;
                }
            }
        }

        Console.WriteLine("Пароль не знайдено");
    }
}