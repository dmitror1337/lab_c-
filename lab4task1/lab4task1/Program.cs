using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Шифр Вернама");
Console.WriteLine("1 — зашифрувати файл");
Console.WriteLine("2 — розшифрувати файл");
Console.WriteLine("3 — підібрати ключ за фразою Mit21");
Console.Write("Обери режим: ");
string? mode = Console.ReadLine();

if (mode == "1")
{
    Console.Write("Шлях до початкового файлу: ");
    string path = Console.ReadLine()!;

    Console.Write("Ключ: ");
    byte[] key = Encoding.UTF8.GetBytes(Console.ReadLine()!);

    byte[] data = File.ReadAllBytes(path);
    byte[] result = Xor(data, key);

    string output = Path.ChangeExtension(path, ".dat");
    File.WriteAllBytes(output, result);
    Console.WriteLine($"Зашифрований файл: {output}");
}
else if (mode == "2")
{
    Console.Write("Шлях до зашифрованого файлу: ");
    string path = Console.ReadLine()!;

    Console.Write("Ключ: ");
    byte[] key = Encoding.UTF8.GetBytes(Console.ReadLine()!);

    byte[] data = File.ReadAllBytes(path);
    byte[] result = Xor(data, key);

    Console.WriteLine("Розшифрований текст:");
    Console.WriteLine(Encoding.UTF8.GetString(result));
}
else if (mode == "3")
{
    Console.Write("Шлях до зашифрованого файлу: ");
    string path = Console.ReadLine()!;

    byte[] cipher = File.ReadAllBytes(path);
    byte[] phrase = Encoding.ASCII.GetBytes("Mit21");

    byte[]? bestText = null;
    double bestScore = -1;

    for (int position = 0; position <= cipher.Length - phrase.Length; position++)
    {
        byte[] key = new byte[5];

        for (int i = 0; i < phrase.Length; i++)
            key[(position + i) % 5] = (byte)(cipher[position + i] ^ phrase[i]);

        byte[] text = Xor(cipher, key);
        string candidateText = Encoding.UTF8.GetString(text);
        double score = 0;

        foreach (char c in candidateText)
        {
            if (char.IsLetterOrDigit(c) ||
                char.IsWhiteSpace(c) ||
                char.IsPunctuation(c))
            {
                score++;
            }

            if (char.IsLetter(c))
                score++;
        }

        if (score > bestScore)
        {
            bestScore = score;
            bestText = text;
        }
    }

    Console.WriteLine("Розшифрований текст:");
    Console.WriteLine(Encoding.UTF8.GetString(bestText!));
}
else
{
    Console.WriteLine("Невідомий режим.");
}

static byte[] Xor(byte[] data, byte[] key)
{
    byte[] result = new byte[data.Length];

    for (int i = 0; i < data.Length; i++)
        result[i] = (byte)(data[i] ^ key[i % key.Length]);

    return result;
}