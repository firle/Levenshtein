using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Levenshtein
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");

            //var input1 = Console.ReadLine();
            //var input2 = Console.ReadLine();

            var testData = new List<(string, string)>{
                ("ababab","abaabab"),
                ("Peter","Petra"),
                ("abcd","efgh"),
                ("aabb","bbcc")
            };


            var data = testData[0];

            var start = DateTime.Now;

            var matrix = new LevenshteinMatrix(data.Item1, data.Item2, true);
            Console.WriteLine($"\nLevenshtein Time: {(DateTime.Now - start).TotalMilliseconds}");

            Console.WriteLine(matrix);


            //BENCHMARK
            bool printBenchData = false;


            DateTime end;
            var minLength = 5;
            var lengthDiv = 10;
            var numPairs = 20;
            var iterations = 10000;

            var benchData = new List<(string, string)>();

            for (int i = 0; i < numPairs; i++)
            {
                benchData.Add((RandomString(random.Next(lengthDiv) + minLength), RandomString(random.Next(lengthDiv) + minLength)));
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("BENCHMARK without heuristic");
            Console.WriteLine();

            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                foreach (var item in benchData)
                {
                    matrix = new LevenshteinMatrix(item.Item1, item.Item2);
                }
            }
            end = DateTime.Now;


            if (printBenchData)
            foreach (var item in benchData)
            {
                matrix = new LevenshteinMatrix(item.Item1, item.Item2);
                Console.WriteLine(matrix);
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine($"Total Bench Time: {(end - start).TotalMilliseconds}");
            Console.WriteLine($"ms/lev Time: {(end - start).TotalMilliseconds / (iterations * numPairs)}");


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("BENCHMARK with heuristic");
            Console.WriteLine();
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                foreach (var item in benchData)
                {
                    matrix = new LevenshteinMatrix(item.Item1, item.Item2, true);
                }
            }
            end = DateTime.Now;

            if(printBenchData)
            foreach (var item in benchData)
            {
                matrix = new LevenshteinMatrix(item.Item1, item.Item2);
                Console.WriteLine(matrix);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine($"Total Bench Time: {(end - start).TotalMilliseconds}");
            Console.WriteLine($"ms/lev Time: {(end - start).TotalMilliseconds / (iterations * numPairs)}");



        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "aaaabbc";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
