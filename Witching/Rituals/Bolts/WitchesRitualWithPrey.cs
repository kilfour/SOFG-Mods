using Assets.Code;
using UnityEngine;
using Witching.Rituals.Bolts.Nuts;

namespace Witching.Rituals.Bolts
{
    public abstract class WitchesRitualWithPrey : WitchesRitual
    {
        public PreyComponent Prey;

        public WitchesRitualWithPrey(Location location, Person prey)
            : base(location)
        {
            Prey = new PreyComponent(prey);
        }

        public override Sprite getSprite()
        {
            return Prey.GetSprite();
        }
    }
}