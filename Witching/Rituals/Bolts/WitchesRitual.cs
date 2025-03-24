using System;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Traits;

namespace Witching.Rituals.Bolts
{
    public abstract class WitchesRitual : Ritual
    {
        public WitchesPower witchesPower;

        public Person prey;

        protected abstract int RequiredCharges { get; }

        private const int onlyPerformedByDarkEmpire = -1;

        public WitchesRitual(Location location, WitchesPower witchesPower, Person prey)
            : base(location)
        {
            this.witchesPower = witchesPower;
            this.prey = prey;
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.LORE;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Lore", unit.getStatLore()));
            return Math.Max(1, unit.getStatLore());
        }

        public override Sprite getSprite()
        {
            return prey.getPortrait();
        }

        public override int isGoodTernary()
        {
            return onlyPerformedByDarkEmpire;
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