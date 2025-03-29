using System;
using System.Collections.Generic;
using Assets.Code;
using Witching.Bolts;

namespace Witching.Rituals.Bolts.Nuts
{
    public abstract class WitchesRitual : Ritual
    {
        public WitchesRitual(Location location)
            : base(location) { }

        public override challengeStat getChallengeType()
        {
            return challengeStat.LORE;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Lore", unit.getStatLore()));
            return Math.Max(1, unit.getStatLore());
        }

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }
    }
}