using System.Linq;
using Assets.Code;

namespace Witching
{
    public static class ExtUA
    {
        public static bool IsAProphet(this UA unit)
        {
            return unit.map.socialGroups.Any(a => a is HolyOrder order && order.prophet == unit);
        }
    }
}