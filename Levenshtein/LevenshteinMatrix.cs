using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Levenshtein
{
    public class LevenshteinMatrix
    {
        int m, n;

        //int[,] M;

        LevField[,] M;


        string a;
        string b;

        public LevenshteinMatrix(string input1, string input2, bool useHeristic = false)
        {
            m = input1.Length;
            n = input2.Length;

            int dLength = Math.Abs(m-n);
            int colOffset=0, rowOffset=0;
            if (m < n)
                colOffset = dLength;
            if (n < m)
                rowOffset = dLength;
                

            int minLength = Math.Min(n, m);

            a = input1;
            b = input2;

            M = new LevField[m+1, n+1];
            int colStart = 0;
            int colEnd = 0;
            var K = 0;

            int f, skipCols=1, rowMin = 0, currentStart=0;
            (int, int) minPos=(0,0);
            LevField dist;

                    //K = Helper.HammingDistance(a, b, minPos.Item1, minPos.Item2);
            if (useHeristic)
                for (int i = 0; i <= m; ++i)
                {
                    K = Helper.HammingDistance(a, b, minPos.Item1, minPos.Item2);
                Console.WriteLine(K);
                    rowMin = int.MaxValue;
                if(colStart !=0)
                    currentStart = Math.Min(minPos.Item2, colStart);
                    colStart = 0;
                    for (int j = currentStart; j <= n; ++j)
                    {
                        dist = new LevField(levDistanceDirection(i, j));
                        if (dist < rowMin)
                        {
                            rowMin = dist;
                            minPos = (i, j);
                        }

                        f = dist + Math.Abs(dLength - Math.Abs(i - j));

                        if (f >= K)
                        {
                            //skipCols = (f == K) ? 1 : 2;
                            if (m < n)
                            {
                                if (j < i)
                                    colStart = (colStart == 0) ? j + ((f == K) ? 1 : 2) : colStart;
                                else if ((colEnd < j) && (j - dLength > i))
                                {
                                    M[i, j] = dist;
                                    colEnd = j;
                                    break;
                                }

                            }
                            else
                            {
                                if (j + dLength < i)
                                    colStart = (colStart == 0) ? j + ((f == K) ? 1 : 2) : colStart;
                                else if ((colEnd < j) && (j > i))
                                {
                                    M[i, j] = dist;
                                    colEnd = j;
                                    break;
                                }
                            }
                        }

                        M[i, j] = dist;

                        //Console.WriteLine(this);
                        //Thread.Sleep(100);
                    }
                }
            else
                for (int i = 0; i <= m; ++i)
                {
                    for (int j = colStart; j <= n; ++j)
                    {
                        M[i, j] = new LevField(levDistanceDirection(i, j));
                    }
                }


            
        }

        public int levDistance(int i, int j)
        {
            if (Math.Min(i, j) == 0)
                return Math.Max(i, j);

            var ai = a[i-1];
            var bj = b[j-1];


            if (ai.Equals(bj))
                return M[i - 1, j - 1];

            var val = Math.Min(M[i - 1, j] + 1, M[i, j - 1] + 1);

            return Math.Min(val, M[i - 1, j - 1] + 1);
        }

        public (int, ELevDirection) levDistanceDirection(int i, int j)
        {
            //initialize Matrix
            if (Math.Min(i, j) == 0)
                return (i > j) ? (i, ELevDirection.Up) 
                               : (i < j) ? (j, ELevDirection.Left) 
                                         : (0, ELevDirection.None);

            var ai = a[i - 1];
            var bj = b[j - 1];

            int d = M[i - 1, j - 1]+1;
            int val = 0;

            ELevDirection drc = 0;

            if (ai == bj)
                d = M[i - 1, j - 1];
            
            int u = M[i - 1, j]+1;
            int l = M[i, j - 1]+1;

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

            if (val == d)
                drc |= ELevDirection.UpLeft;
            return (val, drc);
        }

        public int LevenshteinDistance { get { return M[m, n]; }}


        //public void PrintDirection()
        //{
        //    Console.WriteLine();

        //    Console.Write("       ");

        //    for (int i = 0; i < m; ++i)
        //        Console.Write($"{b[i]}   ");
        //    Console.WriteLine();

        //    for (int i = 0; i <= n; ++i)
        //    {
        //        if (i > 0)
        //        {
        //            string c = "\n   ";
        //            for (int j = 0; j <= m; ++j)
        //            {
        //                var field = M[i, j];
        //                if (field == null) continue;
        //                //((field.Direction & ELevDirection.Left) != 0) ? " - " : "   ";
        //                if (j != 0)
        //                {
        //                    c += ((field?.Direction & ELevDirection.UpLeft) != 0) ? @" \ " : "   ";
        //                    c += ((field?.Direction& ELevDirection.Up) != 0) ? "|" : " ";
        //                }
        //                else
        //                    c+=((field?.Direction & ELevDirection.Up) != 0) ? "|" : " ";
        //            }

        //            Console.Write(c);
        //            Console.Write($"\n{a[i - 1]}  ");
        //        }
        //        else
        //            Console.Write("\n   ");
        //        for (int j = 0; j <= m; ++j)
        //        {
        //            var field = M[i, j];
        //            if ((j != 0)&&(field != null))
        //            {
        //                var c = ((field?.Direction & ELevDirection.Left) != 0) ? " - " : "   ";

        //                Console.Write($"{c}{field}");
        //            }
        //            else
        //                Console.Write(field?.ToString());

        //        }
        //    }
        //}

        public override string ToString()
        {
            var str = "\n";

            var sb = new StringBuilder("\n");


            sb.Append("       ");

            for (int i = 0; i < n; ++i)
                sb.Append($"{b[i]}   ");
            sb.Append("\n");

                string c;
            for (int i = 0; i <= m; ++i)
            {
                if (i > 0)
                {
                    c = "\n";
                    for (int j = 0; j <= n; ++j)
                    {
                        var field = M[i, j];
                        if (field == null)
                        {
                            c += "    ";
                            continue;
                        }
                        //((field.Direction & ELevDirection.Left) != 0) ? " - " : "   ";
                        if (j != 0)
                        {
                            c += ((field?.Direction & ELevDirection.UpLeft) != 0) ? @" \ " : "   ";
                            c += ((field?.Direction & ELevDirection.Up) != 0) ? "|" : " ";
                        }
                        else
                            c += ((field?.Direction & ELevDirection.Up) != 0) ? "   |" : "     ";
                    }

                    sb.Append(c);
                    sb.Append($"\n{a[i - 1]}  ");
                }
                else
                    sb.Append("\n   ");
                
                for (int j = 0; j <= n; ++j)
                {
                    var field = M[i, j];

                    if ((j != 0))
                    {
                        if (field == null)
                            sb.Append("    ");
                        else
                        {
                            c = ((field?.Direction & ELevDirection.Left) != 0) ? " - " : "   ";

                            sb.Append($"{c}{field}");
                        }
                    }
                    else
                        sb.Append(field?.ToString()??" ");
                }
            }

            return sb.ToString();
        }

        public void Print()
        {
            Console.WriteLine();

            Console.Write("       ");

            for (int i = 0; i < n; ++i)
                Console.Write($"{b[i]}   ");
            Console.WriteLine();

            for (int i = 0; i <= m; ++i)
            {
                if (i > 0)
                    Console.Write($"\n\n{a[i - 1]}  ");
                else
                    Console.Write("\n   ");
                for (int j = 0; j <= n; ++j)
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
