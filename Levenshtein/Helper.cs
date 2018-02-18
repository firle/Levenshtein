using System;
using System.Collections.Generic;
using System.Linq;

namespace Levenshtein
{
    public class Helper
    {
        public static int HammingDistance(IList<object> a, IList<object> b)
        {
#if DEBUG
            var time = DateTime.Now;
#endif

            int ham = Math.Abs(a.Count - b.Count);
            for (int i = 0; i < Math.Min(a.Count, b.Count); ++i)
                if (!a[i].Equals(b[i]))
                    ham++;


#if DEBUG
            Console.WriteLine($"Hamming Time: {(DateTime.Now - time).TotalMilliseconds}");
#endif

            return ham;
        }

        public static int HammingDistance(string a, string b) => HammingDistance(a.Cast<object>().ToList(), b.Cast<object>().ToList());

    }
}
