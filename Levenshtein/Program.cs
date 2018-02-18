using System;
using System.Collections.Generic;

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


            //var a = "ababab";
            //var b = "abaabab";
            var data = testData[1];

            var start = DateTime.Now;
            var matrix = new LevenshteinMatrix(data.Item1,data.Item2);
            Console.WriteLine($"\nLEvenshtein Time: {(DateTime.Now-start).Milliseconds}");

            Console.WriteLine(matrix);

            //Console.ReadLine();


        }
    }
}
