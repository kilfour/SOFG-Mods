using System;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Bolts;
using Witching.Traits;

namespace Witching.Rituals
{
    public class WitchesStories : Ritual
    {
        public WitchesPower WitchesPower;

        public WitchesStories(Location location, WitchesPower witchesPower)
            : base(location) { WitchesPower = witchesPower; }

        public override Sprite getSprite()
        {
            return map.world.iconStore.madness;
        }

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }

        public override string getName()
        {
            return "The Witch's Stories";
        }

        public override string getDesc()
        {
            return @"When in a human settlement disperse all your power to spread nightmare's to the population, raising madness by twice your power.";
        }

        public override string getRestriction()
        {
            return "Must have atleast one Witches Power. Only available in a human settlement.";
        }

        public override bool validFor(UA unit)
        {
            if (WitchesPower.Charges <= 0)
                return false;
            if (!(unit.location.settlement is SettlementHuman))
                return false;
            return true;
        }

        public override double getComplexity()
        {
            return 30;
        }

        public override int getCompletionMenace()
        {
            return WitchesPower.Charges / 2;
        }

        public override int getCompletionProfile()
        {
            return WitchesPower.Charges;
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

        public override void complete(UA unit)
        {
            Property.addToPropertySingleShot("The Witch's Stories",
                Property.standardProperties.MADNESS, WitchesPower.Charges * 2, unit.location);
        }
    }
}