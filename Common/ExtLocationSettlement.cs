using System.Linq;
using Assets.Code;

namespace Common
{
    public static class ExtLocationSettlement
    {
        public static T GetSubSettlementOrNull<T>(this Location location) where T : Subsettlement
        {
            // ----------------------------------------------------
            //Being very careful ;-)
            if (location == null) return null;
            if (location.settlement == null) return null;
            if (location.settlement.subs == null) return null;
            // --
            return
                location.settlement.subs
                    .Select(a => a as T)
                    .Where(a => a != null)
                    .FirstOrDefault();
        }
        public static bool HasSubSettlement<T>(this Location location) where T : Subsettlement
        {
            return GetSubSettlementOrNull<T>(location) != null;
        }

        public static bool HasTemple(this Location location)
        {
            return HasSubSettlement<Sub_Temple>(location);
        }

        public static bool HasFarms(this Location location)
        {
            return HasSubSettlement<Sub_Farms>(location);
        }

        public static HolyOrder GetHolyOrderOrNull(this Location location)
        {
            var temple = GetSubSettlementOrNull<Sub_Temple>(location);
            if (temple == null) return null;
            return temple.order;
        }

        public static bool IsFullyInfiltrated(this Location location)
        {
            return location.settlement.subs.All(a => !a.canBeInfiltrated() || a.infiltrated);
        }

        public static bool IsNotFullyInfiltrated(this Location location)
        {
            return !IsFullyInfiltrated(location);
        }
    }
}