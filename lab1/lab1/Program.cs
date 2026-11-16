using System.Security.Cryptography;
using System.Text;

namespace CryptoRandomApp
{
    internal class Program
    {
        const int Count = 10;
        const int Max = 100;
        const int Attempts = 3;

        static int[] Generate()
        {
            int[] result = new int[Count];
            for (int i = 0; i < Count; i++)
                result[i] = RandomNumberGenerator.GetInt32(Max);
            return result;
        }

        static int CountEqual(int[] a, int[] b)
        {
            int equal = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] == b[i]) equal++;
            return equal;
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine(" Криптографічно стійкі випадкові числа (RandomNumberGenerator) \n");

            int[][] seqs = new int[Attempts][];
            for (int i = 0; i < Attempts; i++)
            {
                seqs[i] = Generate();
                Console.WriteLine($"Спроба {i + 1}: {string.Join(", ", seqs[i])}");
            }

            Console.WriteLine("\nПорівняння послідовностей за позиціями:");
            for (int i = 0; i < Attempts; i++)
                for (int j = i + 1; j < Attempts; j++)
                    Console.WriteLine($"Спроба {i + 1} і спроба {j + 1}: збігається {CountEqual(seqs[i], seqs[j])} з {Count}");



            Console.ReadKey();
        }
    }
}