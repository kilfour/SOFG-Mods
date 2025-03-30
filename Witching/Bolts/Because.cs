using Assets.Code;

namespace Witching.Bolts
{
    public static class Because
    {
        public static Builder Of(string reason) => new Builder(reason);

        public class Builder
        {
            public static Builder Of(string reason) => new Builder(reason);

            private readonly string reason;
            public Builder(string reason)
            {
                this.reason = reason;
            }

            public AddBuilder Add(double amount) => new AddBuilder(reason, amount);

            public class AddBuilder : ExecutingBuilder
            {
                public AddBuilder(string reason, double amount)
                    : base(reason, amount) { }

                protected override void ChangeProperty(Property.standardProperties property, Location location)
                {
                    AddToProperty(reason, property, amount, location);
                }
            }

            public RemoveBuilder Remove(double amount) => new RemoveBuilder(reason, amount);

            public class RemoveBuilder : ExecutingBuilder
            {
                public RemoveBuilder(string reason, double amount)
                    : base(reason, amount) { }

                protected override void ChangeProperty(Property.standardProperties property, Location location)
                {
                    RemoveFromProperty(reason, property, amount, location);
                }
            }

            public TransformBuilder Transform(double amount) => new TransformBuilder(reason, amount);

            public class TransformBuilder : EXEBuilder
            {
                public TransformBuilder(string reason, double amount)
                    : base(reason, amount) { }

                public void UnrestIntoMadness(Location location)
                {
                    RemoveFromProperty(reason, Property.standardProperties.UNREST, amount, location);
                    AddToProperty(reason, Property.standardProperties.MADNESS, amount, location);
                }
            }

            public abstract class EXEBuilder
            {
                protected string reason;
                protected double amount;
                public EXEBuilder(string reason, double amount)
                {
                    this.reason = reason;
                    this.amount = amount;
                }
            }
            public abstract class ExecutingBuilder : EXEBuilder
            {
                protected ExecutingBuilder(string reason, double amount)
                : base(reason, amount) { }

                public void Madness(Location location)
                {
                    ChangeProperty(Property.standardProperties.MADNESS, location);
                }

                public void Unrest(Location location)
                {
                    ChangeProperty(Property.standardProperties.UNREST, location);
                }

                protected abstract void ChangeProperty(Property.standardProperties property, Location location);
            }
        }

        private static void AddToProperty(string reason, Property.standardProperties property, double amount, Location location)
        {
            Property.addToProperty(reason, property, amount, location);
        }

        private static void RemoveFromProperty(string reason, Property.standardProperties property, double amount, Location location)
        {
            AddToProperty(reason, property, 0 - amount, location);
        }
    }
}