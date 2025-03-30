using System.Linq;
using Assets.Code;

namespace Witching
{
    public static class ExtPerson
    {
        public static T GetTrait<T>(this Person person) where T : class
        {
            return person.traits.FirstOrDefault(a => a is T) as T;
        }

        public static bool HasTrait<T>(this Person person) where T : class
        {
            return person.traits.Any(a => a is T);
        }
    }
}