using System;
using System.Collections.Generic;
using System.Linq;

namespace Levenshtein
{
    public class Helper
    {

        //public static int HammingDistance(IList<object> a, IList<object> b, int startA=0, int startB=0)
        public static int HammingDistance(string a, string b, int startA = 0, int startB = 0)
        {
            int ham = Math.Abs((a.Length-startA) - (b.Length-startB));

            int i = startA;
            int j = startB;

            while(i<a.Length &j<b.Length)
            {
                if (!a[i++].Equals(b[j++]))
                    ham++;
            }

            return ham;
        }

        public static int HammingDistance(IList<object> a, IList<object> b)
        {
            int ham = Math.Abs(a.Count - b.Count);
            for (int i = 0; i < Math.Min(a.Count, b.Count); ++i)
                if (!a[i].Equals(b[i]))
                    ham++;
            
            return ham; 
        }

        public static int HammingDistance(string a, string b) => HammingDistance(a.Cast<object>().ToList(), b.Cast<object>().ToList());

    }
}
