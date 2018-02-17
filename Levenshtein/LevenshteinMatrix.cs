using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Levenshtein
{
    public class LevenshteinMatrix
    {
        int n, m;

        //int[,] M;

        LevField[,] M;

        string a,b;

        public LevenshteinMatrix(string input1, string input2)
        {
            n = input1.Length;
            m = input2.Length;

            a = input1;
            b = input2;

            M = new LevField[n+1, m+1];

            for (int i = 0; i <= n; ++i)
                for (int j = 0; j <= m; ++j)
                {
                M[i, j] = new LevField(levDistanceDirection(i, j));
                PrintDirection();
                    Thread.Sleep(100);
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

        public (int, ELevDirection) levDistanceDirection(int i, int j)
        {
            Console.Write($"\n({i},{j}): ");
            if (Math.Min(i, j) == 0)
                return (i > j) ? (i, ELevDirection.Up) 
                               : (i < j) ? (j, ELevDirection.Left) 
                                         : (0, ELevDirection.None);

            var ai = a[i - 1];
            var bj = b[j - 1];

            //Console.Write($"({ai},{bj})");

            int d = M[i - 1, j - 1]+1;
            int val = 0;

            ELevDirection drc = 0;

            if (ai == bj)
            {
                d = M[i - 1, j - 1];
                drc |= ELevDirection.UpLeft;
            }
            int u = M[i - 1, j] + 1;
            int l = M[i, j - 1] + 1;

            if (Math.Min(u, l) > d)
                return (d, ELevDirection.UpLeft);


            if (l >= u)
            {
                drc |= ELevDirection.Up;
                val = Math.Min(u, d);
            }
            if (l <= u)
            {
                drc |= ELevDirection.Left;
                val = Math.Min(l, d);
            }

            return (val, drc);
        }
        public void PrintDirection()
        {
            Console.WriteLine();

            Console.Write("       ");

            for (int i = 0; i < m; ++i)
                Console.Write($"{b[i]}   ");
            Console.WriteLine();

            for (int i = 0; i <= n; ++i)
            {
                if (i > 0)
                {
                    string c = "\n   ";
                    for (int j = 0; j <= m; ++j)
                    {
                        var field = M[i, j];
                        if (field == null) continue;
                        //((field.Direction & ELevDirection.Left) != 0) ? " - " : "   ";
                        if (j != 0)
                        {
                            c += ((field?.Direction & ELevDirection.UpLeft) != 0) ? @" \ " : "   ";
                            c += ((field?.Direction& ELevDirection.Up) != 0) ? "|" : " ";
                        }
                        else
                            c+=((field?.Direction & ELevDirection.Up) != 0) ? "|" : " ";
                    }

                    Console.Write(c);
                    Console.Write($"\n{a[i - 1]}  ");
                }
                else
                    Console.Write("\n   ");
                for (int j = 0; j <= m; ++j)
                {
                    var field = M[i, j];
                    if ((j != 0)&&(field != null))
                    {
                        var c = ((field?.Direction & ELevDirection.Left) != 0) ? " - " : "   ";

                        Console.Write($"{c}{field}");
                    }
                    else
                        Console.Write(field?.ToString());

                }
            }
        }

        public override string ToString()
        {
            var str = "\n";

            var sb = new StringBuilder("\n");


            sb.Append("       ");

            for (int i = 0; i < m; ++i)
                sb.Append($"{b[i]}   ");
            sb.Append("\n");

            for (int i = 0; i <= n; ++i)
            {
                if (i > 0)
                {
                    string c = "\n   ";
                    for (int j = 0; j <= m; ++j)
                    {
                        var field = M[i, j];
                        if (field == null) continue;
                        //((field.Direction & ELevDirection.Left) != 0) ? " - " : "   ";
                        if (j != 0)
                        {
                            c += ((field?.Direction & ELevDirection.UpLeft) != 0) ? @" \ " : "   ";
                            c += ((field?.Direction & ELevDirection.Up) != 0) ? "|" : " ";
                        }
                        else
                            c += ((field?.Direction & ELevDirection.Up) != 0) ? "|" : " ";
                    }

                    sb.Append(c);
                    sb.Append($"\n{a[i - 1]}  ");
                }
                else
                    sb.Append("\n   ");
                for (int j = 0; j <= m; ++j)
                {
                    var field = M[i, j];
                    if ((j != 0) && (field != null))
                    {
                        var c = ((field?.Direction & ELevDirection.Left) != 0) ? " - " : "   ";

                        sb.Append($"{c}{field}");
                    }
                    else
                        sb.Append(field?.ToString());
                }
            }

            return sb.ToString();
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
