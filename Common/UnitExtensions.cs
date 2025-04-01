using System.Linq;
using Assets.Code;

namespace Common
{
    public static class UnitExtensions
    {
        public static bool IsAProphet(this UA unit)
        {
            return unit.map.socialGroups.Any(a => a is HolyOrder order && order.prophet == unit);
        }
    }
}