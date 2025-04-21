using Assets.Code;
using Common;
using TheBroken.Modifiers;
using System.Linq;
using System;

namespace TheBroken.Traits
{
    public class GravenPresence : Trait
    {
        public override string getName()
        {
            return "Graven Presence";
        }

        public override string getDesc()
        {
            return "Stone forgets, gold gathers, and shadow clings.";
        }

        public override void turnTick(Person person)
        {
            base.turnTick(person);

            var location = person.unit.location;
            var followers = GetNumberOfFollowers();

            // GET RULER'S MONEY THROUGH MARKET
            if (location.HasMarket())
            {
                var ruler = location.GetRuler();
                if (ruler != null)
                {
                    var syphoned = Math.Min(1 + followers, ruler.gold);
                    ruler.gold -= syphoned;
                    person.gold += syphoned;
                }
            }
            // -- 

            var shadow = 0.0;
            var shard = location.GetPropertyOrNull<Shard>();
            if (shard != null)
            {
                shard.AddCharge("Graven Presence", 3);
                shadow += 0.01;
                person.gold++;
            }
            location.AddShadow(shadow);
        }

        public override int getCommandChange()
        {
            return 0;
        }

        public override int getIntrigueChange()
        {
            return GetNumberOfFollowers();
        }

        public override int getLoreChange()
        {
            if (assignedTo.unit.location.HasLibrary())
                return GetNumberOfFollowers();
            return 0;
        }

        private int GetNumberOfFollowers()
        {
            return
                (from unit in assignedTo.unit.location.units
                 where unit is Broken
                 select unit).Count();
        }
    }
}