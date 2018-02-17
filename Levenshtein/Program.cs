using System;

namespace Levenshtein
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");

            //var input1 = Console.ReadLine();
            //var input2 = Console.ReadLine();


            var start = DateTime.Now;
            var matrix = new LevenshteinMatrix("abababab","abaabab");
            Console.WriteLine($"\nTime: {(DateTime.Now-start).Milliseconds}");

            Console.WriteLine(matrix);

            //Console.ReadLine();


        }
    }
}
