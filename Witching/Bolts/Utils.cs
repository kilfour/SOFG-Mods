using Assets.Code;

namespace Witching.Bolts
{
    public static class Utils
    {
        public static double GetStandardPropertyLevel(Location location, Property.standardProperties form)
        {
            foreach (Property property in location.properties)
                if (property.getPropType() == form)
                    return property.charge;
            return 0.0;
        }

        public static void AddToProperty(string reason, Property.standardProperties property, double amount, Location location)
        {
            Property.addToProperty(reason, property, amount, location);
        }

        public static void RemoveFromProperty(string reason, Property.standardProperties property, double amount, Location location)
        {
            AddToProperty(reason, property, 0 - amount, location);
        }

        public static double GetUnrest(Location location)
        {
            return GetStandardPropertyLevel(location, Property.standardProperties.UNREST);
        }

        // public static void AddUnrest(string reason, double amount, Location location)
        // {
        //     AddToProperty(reason, Property.standardProperties.UNREST, amount, location);
        // }

        public static void RemoveUnrest(string reason, double amount, Location location)
        {
            RemoveFromProperty(reason, Property.standardProperties.UNREST, amount, location);
        }

        public static void AddMadness(string reason, double amount, Location location)
        {
            AddToProperty(reason, Property.standardProperties.MADNESS, amount, location);
        }

        // public static void RemoveMadness(string reason, double amount, Location location)
        // {
        //     RemoveFromProperty(reason, Property.standardProperties.MADNESS, amount, location);
        // }
    }
}