using System;
using System.Collections;
using System.Collections.Generic;

namespace Levenshtein
{
    public class LevenshteinMatrix
    {
        int n, m;

        int[,] M;

        string a,b;

        public LevenshteinMatrix(string input1, string input2)
        {
            n = input1.Length;
            m = input2.Length;

            a = input1;
            b = input2;

            M = new int[n+1, m+1];

            for (int i = 0; i <= n; ++i)
                for (int j = 0; j <= m; ++j)
                {
                    M[i, j] = levDistance(i, j);
                //Print();
                }
            
        }

        public int levDistance(int i, int j)
        {
            Console.Write($"\n({i},{j}): ");
            if (Math.Min(i, j) == 0)
                return Math.Max(i, j);

            var ai = a[i-1];
            var bj = b[j-1];

            //Console.Write($"({ai},{bj})");

            if (ai == bj)
                return M[i - 1, j - 1];

            var val = Math.Min(M[i - 1, j] + 1, M[i, j - 1] + 1);

            Console.Write($"{val}  ");

            return Math.Min(val, M[i - 1, j - 1] + 1);
        }

        public void Print()
        {
            Console.WriteLine();

            Console.Write("       ");

            for (int i = 0; i < m; ++i)
                Console.Write($"{b[i]}   ");
            Console.WriteLine();

            for (int i = 0; i <= n; ++i)
            {
                if (i > 0)
                    Console.Write($"\n\n{a[i - 1]}  ");
                else
                    Console.Write("\n   ");
                for (int j = 0; j <= m; ++j)
                    Console.Write($"{M[i, j]}   ");
            }
        }

        public void CalcAlignments()
        {
            
        }

        private IEnumerable<ELevDirection> GetPredecessors(int i, int j)
        {
            var list = new List<ELevDirection>();

            if (i + j == 0) return list;

            if (Math.Min(i, j) == 0)
            {
                if (i == 0) list.Add(ELevDirection.Left);
                else if (j == 0) list.Add(ELevDirection.Up);
                return list;
            }

            var f = M[i, j];


            if (f > M[i - 1, j]) list.Add(ELevDirection.Up);
            else if (f > M[i, j - 1]) list.Add(ELevDirection.Left);
            else if ((f > M[i - 1, j - 1]) | ((f == M[i - 1, j - 1]) & (a[i] == b[j])))
                    list.Add(ELevDirection.UpLeft);


            return list;
        }


    }
}
