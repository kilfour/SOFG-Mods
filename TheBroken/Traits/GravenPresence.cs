using Assets.Code;
using Common;
using TheBroken.Modifiers;
using System.Linq;

namespace TheBroken.Traits
{
    [LegacyDoc(Order = "1.4.1", Caption = "Graven Presence/Flock", Content =
@"A passive ability that when in a village with a shard:
- Grows the Shard by 2 per turn.
- Gives the agent 1 gold.
- Spreads 1 shadow.  
Additionally for each Broken follower in the same location as The First: 
- Spreads an additional 0.25 shadow.
- When in a City creates unrest equal to the number of followers of The First.")]
    public class GravenPresence : Trait
    {
        public FirstAmongTheBroken theFirst;

        public GravenPresence(FirstAmongTheBroken theFirst)
        {
            this.theFirst = theFirst;
        }
        public override string getName()
        {
            if (theFirst.LeadingFlock)
                return "Graven Flock";
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

            // Unrest in Human city
            if (followers > 0 && location.HasCity())
            {
                Because.Of("Graven Flock").Add(followers).Unrest(location);
            }
            // -- 

            var shadow = 0.0;
            var shard = location.GetPropertyOrNull<Shard>();
            if (shard != null)
            {
                shard.AddCharge("Graven Presence", 2);
                shadow += 0.01;
                person.gold++;
            }
            shadow += followers * 0.0025;
            location.AddShadow(shadow);
        }

        public override int getCommandChange()
        {
            return 0;
        }

        public override int getIntrigueChange()
        {
            return 0;
        }

        public override int getLoreChange()
        {
            // if (assignedTo.unit.location.HasLibrary())
            //     return GetNumberOfFollowers();
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