using Assets.Code;
using Witching.Rituals;

namespace Witching.Traits
{
    public class TruthSpeaker : Trait
    {
        public override string getName()
        {
            return "Truth Speaker";
        }

        public override string getDesc()
        {
            return "Allows the witch to move to a rulers town and maybe convince him to hire her as a Truth Speaker.";
        }

        public override void onAcquire(Person person)
        {
            var witch = person.unit as Witch;
            witch.AddWitchesRitual(new RaiseFunds.RitualUpdater(witch));
        }
    }
}