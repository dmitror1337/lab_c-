using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        string password = "MyPassword123";
        byte[] salt = RandomNumberGenerator.GetBytes(32);
        Console.WriteLine("Сіль: " + Convert.ToBase64String(salt));
        Console.WriteLine();

        int variant = 6;
        var algorithms = new[]
        {
            ("SHA1",   HashAlgorithmName.SHA1),
            ("SHA256", HashAlgorithmName.SHA256),
            ("SHA384", HashAlgorithmName.SHA384),
            ("SHA512", HashAlgorithmName.SHA512),
        };

        foreach (var (name, hash) in algorithms)
        {
            Console.WriteLine(name + ":");

            for (int i = 0; i < 10; i++)
            {
                int iterations = variant * 10000 + i * 50000;

                Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hash, 32); 

                var sw = Stopwatch.StartNew();
                for (int r = 0; r < 10; r++)
                {
                    Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hash, 32);
                }
                sw.Stop();

                double avg = sw.Elapsed.TotalMilliseconds / 10;
                Console.WriteLine($"  {iterations,8} ітерацій: {avg.ToString("F2", CultureInfo.InvariantCulture),10} мс");
            }
            Console.WriteLine();
        }
    }
}