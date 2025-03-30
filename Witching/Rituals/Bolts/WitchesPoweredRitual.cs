using Assets.Code;
using Witching.Rituals.Bolts.Nuts;
using Witching.Traits;

namespace Witching.Rituals.Bolts
{
    public class WitchesPoweredRitual : WitchesRitual
    {
        public PowerComponent Power;

        public WitchesPoweredRitual(Location location, WitchesPower powerSource, int requiredCharges)
            : base(location)
        {
            Power = new PowerComponent(powerSource, requiredCharges);
        }

        public override bool valid()
        {
            return Power.HasEnoughCharges();
        }

        protected void RitualComplete()
        {
            Power.ConsumeCharges();
        }

        protected void FullPowerRitualComplete()
        {
            Power.DrainAllCharges();
        }
    }
}