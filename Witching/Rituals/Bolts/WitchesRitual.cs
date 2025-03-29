using System;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Bolts;

namespace Witching.Rituals.Bolts
{
    public abstract class WitchesRitual : Ritual
    {
        public Person prey;

        public WitchesRitual(Location location, Person prey)
            : base(location)
        {
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
            return Constants.OnlyPerformedByDarkEmpire;
        }
    }
}