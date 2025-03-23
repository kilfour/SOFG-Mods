using System.Linq;
using Assets.Code;

namespace Witching.Traits
{
    public class WitchesChanneler : Trait
    {
        public override string getName()
        {
            return "Witches Channeler";
        }

        public override string getDesc()
        {
            return "Doubles the Power obtained by completing the elligable rituals.";
        }

        public override void onAcquire(Person person)
        {
            var witchesPower = person.traits.FirstOrDefault(a => a is WitchesPower) as WitchesPower;
            witchesPower.ChargesReceivedMultiplier = 2;
        }
    }
}