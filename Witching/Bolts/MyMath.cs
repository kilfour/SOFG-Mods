using System.Collections.Generic;
using System.Linq;


namespace Witching.Bolts
{
    public static class MyMath
    {
        public static double Sum(this IEnumerable<double> doubles)
        {
            return Enumerable.Sum(doubles);
        }
    }
}