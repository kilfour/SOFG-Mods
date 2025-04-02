using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static bool HasAny<T>(this IEnumerable<object> source)
        {
            return source.OfType<T>().Any();
        }
    }
}