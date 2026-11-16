using System;
using System.Security.Cryptography;
using System.Text;


public class SaltedHash
{
   
    private const int SaltSize = 32;

    // криптостійка генерація солі
    public static byte[] GenerateSalt()
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    // Хеш = SHA256(сіль + пароль)
    public static byte[] ComputeHash(string password, byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        byte[] combined = new byte[salt.Length + passwordBytes.Length];
        Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
        Buffer.BlockCopy(passwordBytes, 0, combined, salt.Length, passwordBytes.Length);

        using (var sha = SHA256.Create())
        {
            return sha.ComputeHash(combined);
        }
    }
}

class Program
{
    static void Main()
        
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        string password = "MyPassword123";

        
        byte[] salt1 = SaltedHash.GenerateSalt();
        byte[] salt2 = SaltedHash.GenerateSalt();

        byte[] hash1 = SaltedHash.ComputeHash(password, salt1);
        byte[] hash2 = SaltedHash.ComputeHash(password, salt2);

        Console.WriteLine("Пароль: " + password);
        Console.WriteLine();
        Console.WriteLine("Сіль 1:  " + Convert.ToBase64String(salt1));
        Console.WriteLine("Хеш 1:   " + Convert.ToBase64String(hash1));
        Console.WriteLine();
        Console.WriteLine("Сіль 2:  " + Convert.ToBase64String(salt2));
        Console.WriteLine("Хеш 2:   " + Convert.ToBase64String(hash2));
        Console.WriteLine();
        Console.WriteLine("Хеши співпадають? " + (Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2)));
    }
}