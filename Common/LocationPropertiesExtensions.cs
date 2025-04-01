using System;
using System.Linq;
using Assets.Code;

namespace Common
{
    public static class LocationPropertiesExtensions
    {
        private static Property GetStandardPropertyOrNull(Location location, Property.standardProperties form)
        {
            return location.properties.SingleOrDefault(a => a.getPropType() == form);
        }

        public static double GetUnrest(this Location location)
        {
            var property = GetStandardPropertyOrNull(location, Property.standardProperties.UNREST);
            if (property == null)
                return 0;
            return property.charge;
        }

        public static bool HasProperty<T>(this Location location) where T : Property
        {
            return location.properties.FirstOrDefault(a => a is T) != null;
        }

        public static bool LacksProperty<T>(this Location location) where T : Property
        {
            return !HasProperty<T>(location);
        }

        public static T GetPropertyOrNull<T>(this Location location) where T : Property
        {
            return location.properties.FirstOrDefault(a => a is T) as T;
        }

        public static bool PropertyIs<T>(this Location location, Func<T, bool> predicate) where T : Property
        {
            var property = GetPropertyOrNull<T>(location);
            if (property == null) return false;
            return predicate(property);
        }

        public static void AddProperty(this Location location, Property property)
        {
            location.properties.Add(property);
        }

        public static void RemoveProperty<T>(this Location location) where T : Property
        {
            var property = GetPropertyOrNull<T>(location);
            if (property == null) return;
            location.properties.Remove(property);
        }
    }
}