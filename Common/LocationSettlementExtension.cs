using System;
using System.Linq;
using Assets.Code;

namespace Common
{
    public static class LocationSettlementExtension
    {
        public static T GetSubSettlementOrNull<T>(this Location location) where T : Subsettlement
        {
            return location?.settlement?.subs?.OfType<T>().FirstOrDefault();
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

        public static bool HasCity(this Location location)
        {
            return HasSubSettlement<Sub_City>(location);
        }

        public static bool HasLibrary(this Location location)
        {
            return HasSubSettlement<Sub_Library>(location);
        }

        public static bool HasMarket(this Location location)
        {
            return HasSubSettlement<Sub_Market>(location);
        }

        public static bool HasCoven(this Location location)
        {
            return HasSubSettlement<Sub_WitchCoven>(location);
        }
        public static Person GetRuler(this Location location)
        {
            if (location == null) return null;
            var humanSettlement = location.settlement as SettlementHuman;
            if (humanSettlement == null) return null;
            return humanSettlement.ruler;
        }
        public static HolyOrder GetHolyOrderOrNull(this Location location)
        {
            var temple = GetSubSettlementOrNull<Sub_Temple>(location);
            if (temple == null) return null;
            return temple.order;
        }

        public static bool IsFullyInfiltrated(this Location location)
        {
            if (location == null) return true;
            if (location.settlement == null) return true;
            return location.settlement.subs.All(a => !a.canBeInfiltrated() || a.infiltrated);
        }

        public static bool IsNotFullyInfiltrated(this Location location)
        {
            return !IsFullyInfiltrated(location);
        }

        public static void AddShadow(this Location location, double shadow)
        {
            if (location.settlement == null) return;
            if (location.settlement.shadow < 1.0)
                location.settlement.shadow = Math.Min(1.0, location.settlement.shadow + shadow);
        }
    }
}