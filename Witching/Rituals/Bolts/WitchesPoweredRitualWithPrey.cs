using Assets.Code;
using UnityEngine;
using Witching.Rituals.Bolts.Nuts;
using Witching.Traits;

namespace Witching.Rituals.Bolts
{
    public class WitchesPoweredRitualWithPrey : WitchesRitual
    {
        public PowerComponent Power;

        public PreyComponent Prey;

        public WitchesPoweredRitualWithPrey(Location location, WitchesPower powerSource, int requiredCharges, Person prey)
            : base(location)
        {
            Power = new PowerComponent(powerSource, requiredCharges);
            Prey = new PreyComponent(prey);
        }

        public override bool valid()
        {
            return Power.HasEnoughCharges();
        }

        protected void RitualComplete()
        {
            Power.ConsumeCharges();
        }

        public override Sprite getSprite()
        {
            return Prey.GetSprite();
        }
    }
}