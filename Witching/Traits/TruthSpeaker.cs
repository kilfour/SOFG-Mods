using Assets.Code;
using Common;
using Witching.Rituals;

namespace Witching.Traits
{
    [LegacyDoc(Order = "1.2.3", Caption = "Truth Speaker", Content =
@"This trait allows the witch to change it's home location (but not it's society) to the current one, and adds a funding action to the list of local settlement action.  
It also improves the relation with the settlement's ruler.  
Can not be performed if a funding action for this witch already exists and if done a second time in another location, the first funding action will dissapear.")]
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
                msg += " Currently hired by " + Target.getName() + " in " + Target.getLocation().getName() + ".";
            return msg;
        }

        public override void onAcquire(Person person)
        {
            var witch = person.unit as Witch;
            witch.AddWitchesRitual(new RaiseFunds.RitualUpdater(witch));
        }
    }
}