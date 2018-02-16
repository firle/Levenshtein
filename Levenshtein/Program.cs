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



            var matrix = new LevenshteinMatrix("lala","lalla");

            matrix.Print();

            //Console.ReadLine();


        }
    }
}
