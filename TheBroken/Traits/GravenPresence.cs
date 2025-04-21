using Assets.Code;
using Common;
using TheBroken.Modifiers;

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
            var shard = location.GetPropertyOrNull<Shard>();
            if (shard == null)
                return;
            shard.AddCharge("Graven Presence", 2);
            person.gold++;
            location.AddShadow(0.01);
        }
    }
}