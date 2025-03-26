using Assets.Code;
using Witching.Rituals;

namespace Witching.Traits
{
    public class BloodWitch : Trait
    {
        public override string getName()
        {
            return "Blood Witch";
        }

        public override string getDesc()
        {
            return "The Blood Witch can inflict the Hunger on Heroes and Rulers and encourage the already infected to feed.";
        }

        public override void onAcquire(Person person)
        {
            var witch = person.unit as Witch;
            witch.WitchesTitle = "Blood Witch";
            witch.AddWitchesRitual(new TheWitchesHunger.RitualUpdater());
            witch.AddWitchesRitual(new TheWitchesStarvation.RitualUpdater());
        }
    }
}