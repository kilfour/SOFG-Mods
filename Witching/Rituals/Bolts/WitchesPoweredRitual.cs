using Assets.Code;
using Witching.Traits;

namespace Witching.Rituals.Bolts
{

    public abstract class WitchesPoweredRitual : WitchesRitual
    {
        public WitchesPower witchesPower;

        protected abstract int RequiredCharges { get; }

        public WitchesPoweredRitual(Location location, WitchesPower witchesPower, Person prey)
            : base(location, prey)
        {
            this.witchesPower = witchesPower;
        }

        public override bool valid()
        {
            if (witchesPower.Charges < RequiredCharges) return false;
            return true;
        }

        protected virtual void RitualComplete()
        {
            witchesPower.Charges -= RequiredCharges;
        }
    }
}