using System;
using System.Security.Cryptography;
using System.Text;

class Program
    
{

    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Введіть повідомлення: ");
        string message = Console.ReadLine();

        Console.Write("Введіть секретний ключ: ");
        string key = Console.ReadLine();

        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        byte[] hmacBytes;

        using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
        {
            hmacBytes = hmac.ComputeHash(messageBytes);
        }

        string savedHmac = Convert.ToBase64String(hmacBytes);

        Console.WriteLine("HMAC повідомлення: " + savedHmac);

        Console.WriteLine();
        Console.Write("Введіть повідомлення для перевірки: ");
        string receivedMessage = Console.ReadLine();

        byte[] receivedBytes = Encoding.UTF8.GetBytes(receivedMessage);
        byte[] receivedHmacBytes;

        using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
        {
            receivedHmacBytes = hmac.ComputeHash(receivedBytes);
        }

        string receivedHmac = Convert.ToBase64String(receivedHmacBytes);

        if (savedHmac == receivedHmac)
        {
            Console.WriteLine("Повідомлення справжнє.");
        }
        else
        {
            Console.WriteLine("Повідомлення змінене або неправильне.");
        }
    }
}