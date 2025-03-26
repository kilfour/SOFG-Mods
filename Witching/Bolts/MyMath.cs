using System;
using System.Collections.Generic;
using System.Linq;


namespace Witching.Rituals
{
    public static class MyMath
    {
        public static double Sum(this IEnumerable<double> doubles)
        {
            return doubles.Sum();
        }
    }
}