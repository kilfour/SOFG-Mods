using System.Collections.Generic;
using System.Linq;


namespace Common
{
    public static class MyMath
    {
        public static double Sum(this IEnumerable<double> doubles)
        {
            return Enumerable.Sum(doubles);
        }
    }
}