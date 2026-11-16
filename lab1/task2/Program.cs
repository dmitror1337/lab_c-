using System;

namespace SeedDemo
{
    internal class Program
    {
        private const int Count = 10;
        private const int MinValue = 1;
        private const int MaxValue = 100;

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Послідовності псевдовипадкових чисел";
            int seedA = 12345;
            int seedB = 12345;
            int seedC = 54321;

            int[] seqA = Generate(seedA);
            int[] seqB = Generate(seedB);
            int[] seqC = Generate(seedC);

            Console.WriteLine("Послідовності");
            PrintSequence($"Seed = {seedA} (A)", seqA);
            PrintSequence($"Seed = {seedB} (B)", seqB);
            PrintSequence($"Seed = {seedC} (C)", seqC);

            Console.ReadKey();
        }

        static int[] Generate(int seed)
        {
            Random rnd = new Random(seed);
            int[] result = new int[Count];
            for (int i = 0; i < Count; i++)
                result[i] = rnd.Next(MinValue, MaxValue + 1);
            return result;
        }

        static void PrintSequence(string title, int[] seq)
        {
            Console.Write($"{title}: ");
            for (int i = 0; i < seq.Length; i++)
            Console.WriteLine();
        }
    }
}