using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Levenshtein
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            SampleData();
            //Benchmark();
            //Test();
            Console.ReadLine();
        }
        public static void SampleData()
        {
            Console.WriteLine("Hello World!\n");

            //var input1 = Console.ReadLine();
            //var input2 = Console.ReadLine();

            var testData = new List<(string, string)>{
                ("ababab","abaabab"),
                ("Peter","Petra"),
                ("abcdijklm","efgh"),
                ("aabb","bbcc"),
                ("aaaa","aaaabcaadef"),
                ("aaaabaaaaaaaab","baaabaaaaaaaaa"),
                ("baabaaaaba","abaaaaaaaaaaaaaaaaaa")
            };


            var data = testData[4];
            var a = data.Item1;
            var b = data.Item2;

            var dir = Path.Combine(Path.GetTempPath(), "Levenshtein", "data");
            Directory.CreateDirectory(dir);
            
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream(Path.Combine(dir, "console.txt"), FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
          
            var start = DateTime.Now;

            var matrix = new LevenshteinMatrix(a,b, true);
            Console.WriteLine($"\nLevenshtein Time: {(DateTime.Now - start).TotalMilliseconds}");
            Console.WriteLine();
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine();
            Console.WriteLine($"HammingDistance: {Helper.HammingDistance(a, b)}");

            Console.WriteLine();
            Console.WriteLine(matrix);
            Console.WriteLine($"LevenshteinDistance: {matrix.LevenshteinDistance}");
            Console.WriteLine();
            matrix.CalculateLevenshteinDistance(a, b);
            Console.WriteLine(matrix);
            Console.WriteLine($"LevenshteinDistance: {matrix.LevenshteinDistance}");
            Console.WriteLine();



            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");



        }
        public static void Test()
        {
            //Testing

            int iterations = 100000;
            LevenshteinMatrix matrix = new LevenshteinMatrix();
            var minLength = 10;
            var lengthDiv = 20;

            Console.WriteLine("Start TESTING");

            while(--iterations>0)
            {
                var a = RandomString(random.Next(lengthDiv) + minLength);
                var b = RandomString(random.Next(lengthDiv) + minLength);

                var lev1 = matrix.CalculateLevenshteinDistance(a,b);
                
                var lev2 = matrix.CalculateLevenshteinDistance(a, b, true);

                //Console.WriteLine($"({lev1},{lev2})");
                //Console.WriteLine(iterations);
               

                if (lev1 != lev2)
                {
                    Console.WriteLine("ERROR:");

                    Console.WriteLine(a);
                    Console.WriteLine(b);
                    Console.WriteLine();
                    Console.WriteLine($"HammingDistance: {Helper.HammingDistance(a,b)}");
                    Console.WriteLine($"LevenshteinDistance 1: {lev1}");
                    Console.WriteLine($"LevenshteinDistance 2: {lev2}");
                    Console.WriteLine();
                    break;
                }
            }
            Console.WriteLine("End TESTING");
        }
        public static void Benchmark()
        {
            //BENCHMARK
            bool printBenchData = false;
            LevenshteinMatrix matrix = new LevenshteinMatrix();
            DateTime start;

            DateTime end;
            var minLength = 2000;
            var lengthDiv = 100;
            var numPairs = 20;
            var iterations = 2;

            var benchData = new List<(string, string)>();

            for (int i = 0; i < numPairs; i++)
            {
                benchData.Add((RandomString(random.Next(lengthDiv) + minLength), RandomString(random.Next(lengthDiv) + minLength)));
            }
            var dir = Path.Combine(Path.GetTempPath(), "Levenshtein", "data");
            Directory.CreateDirectory(dir);
            var list = new List<string>();
            benchData.ForEach((t) => list.Add($"'{t.Item1}','{t.Item2}'"));
            File.WriteAllLines(Path.Combine(dir, $"benchData{DateTime.Now.Ticks}.txt"),list);


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("BENCHMARK Printing BenchData");
            Console.WriteLine();
            if (printBenchData)
                foreach (var item in benchData)
                {
                    var lev1 = matrix.CalculateLevenshteinDistance(item.Item1, item.Item2);
                    //Console.WriteLine(matrix);
                    //Console.WriteLine($"LevenshteinDistance: {lev1}");
                    //Console.WriteLine();

                    var lev2 = matrix.CalculateLevenshteinDistance(item.Item1, item.Item2, true);
                    Console.WriteLine(matrix);
                    Console.WriteLine($"Levenshtein Distance: {lev2}");
                    Console.WriteLine($"Hamming Distance: {matrix.HammingDistance}");
                    Console.WriteLine();

                    Debug.Assert(lev1 == lev2);
                }

            Console.WriteLine();
            Console.WriteLine("BENCHMARK without heuristic");

            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                foreach (var item in benchData)
                {
                    matrix.CalculateLevenshteinDistance(item.Item1, item.Item2);
                }
            }
            end = DateTime.Now;

            Console.WriteLine();
            Console.WriteLine($"Total Bench Time: {(end - start).TotalMilliseconds}");
            Console.WriteLine($"ms/lev Time: {(end - start).TotalMilliseconds / (iterations * numPairs)}");


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("BENCHMARK with heuristic");
            start = DateTime.Now;



            for (int i = 0; i < iterations; i++)
            {
                foreach (var item in benchData)
                {
                    matrix.CalculateLevenshteinDistance(item.Item1, item.Item2, true);
                }
            }
            end = DateTime.Now;

            Console.WriteLine();
            Console.WriteLine($"Total Bench Time: {(end - start).TotalMilliseconds}");
            Console.WriteLine($"ms/lev Time: {(end - start).TotalMilliseconds / (iterations * numPairs)}");


        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "aaaab";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
