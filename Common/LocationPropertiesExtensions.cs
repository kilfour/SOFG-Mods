using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code;

namespace Common
{
    public static class LocationPropertiesExtensions
    {
        public static IEnumerable<T> GetAllPropertiesOf<T>(this Location location) where T : Property
        {
            return location.properties.OfType<T>();
        }

        private static Property GetStandardPropertyOrNull(Location location, Property.standardProperties form)
        {
            return location.properties.SingleOrDefault(a => a.getPropType() == form);
        }

        public static double GetUnrest(this Location location)
        {
            return
                GetStandardPropertyOrNull(location, Property.standardProperties.UNREST)
                    .MaybeSelect(a => a.charge)
                    .WithDefault(0);
        }

        public static bool HasProperty<T>(this Location location) where T : Property
        {
            return location.properties.HasAny<T>();
        }

        public static bool LacksProperty<T>(this Location location) where T : Property
        {
            return !HasProperty<T>(location);
        }

        public static T GetPropertyOrNull<T>(this Location location) where T : Property
        {
            return location.properties.OfType<T>().FirstOrDefault();
        }

        public static bool PropertyIs<T>(this Location location, Func<T, bool> predicate) where T : Property
        {
            return
                GetPropertyOrNull<T>(location)
                    .MaybeSelect(a => predicate(a))
                    .WithDefault(false);
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