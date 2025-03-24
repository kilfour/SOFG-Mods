using Assets.Code;
using Witching.Rituals;

namespace Witching.Traits
{
    public class HighPriestess : Trait
    {
        public override string getName()
        {
            return "High Priestess";
        }

        public override string getDesc()
        {
            return "The High Priestess gets access to the Witches Hunger, a ritual which allows her to inflict the Hunger on Heroes and Rulers.";
        }

        public override void onAcquire(Person person)
        {
            var witch = person.unit as Witch;
            witch.WitchesTitle = "Priestess";
            witch.AddWitchesRitual(new TheWitchesHunger.RitualUpdater());
        }
    }
}