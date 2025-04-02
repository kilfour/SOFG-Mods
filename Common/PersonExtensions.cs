using System.Linq;
using Assets.Code;

namespace Common
{
    public static class PersonExtensions
    {
        public static T GetTrait<T>(this Person person) where T : class
        {
            return person.traits.OfType<T>().FirstOrDefault();
        }

        public static bool HasTrait<T>(this Person person) where T : class
        {
            return person.traits.Any(a => a is T);
        }
    }
}