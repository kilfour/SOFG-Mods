using Assets.Code;
using Witching.Rituals;

namespace Witching.Traits
{
    public class TruthSpeaker : Trait
    {
        public Person Target;

        public override string getName()
        {
            if (Target == null)
                return "Truth Speaker";
            return "Truth Speaker (" + Target.getName() + ")";
        }

        public override string getDesc()
        {
            var msg = "Allows the witch to move to a rulers town and maybe convince him to hire her as a Truth Speaker.";
            if (Target != null)
                msg += " Currently hired by " + Target.getName() + " in " + Target.getLocation() + ".";
            return msg;
        }

        public override void onAcquire(Person person)
        {
            var witch = person.unit as Witch;
            witch.AddWitchesRitual(new RaiseFunds.RitualUpdater(witch));
        }
    }
}