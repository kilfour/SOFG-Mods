using Assets.Code;
using Witching.Rituals;

namespace Witching.Traits
{
    public class Fundraiser : Trait
    {
        public override string getName()
        {
            return "Fundraiser";
        }

        public override string getDesc()
        {
            return "Gain an extra fifty gold. Whenever the fundraiser enters a human settlement she can spend a Witches Power to try and persuade the ruler to fund her.";
        }

        public override void onAcquire(Person person)
        {
            person.gold += 50;
            var witch = person.unit as Witch;
            witch.AddWitchesRitual(new RaiseFunds.RitualUpdater(witch));
        }
    }
}