using System;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;
namespace Witching.Rituals
{
    public class WitchesStories : WitchesPoweredRitual
    {

        public WitchesStories(Location location, WitchesPower witchesPower)
            : base(location, witchesPower, 1) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("witching.book-of-madness.png");
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
            if (Power.NotEnoughCharges())
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
            return 5;
        }

        public override int getCompletionProfile()
        {
            return 10;
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
            Property.addToPropertySingleShot("The Witch's Stories", Property.standardProperties.MADNESS, Power.GetCharges() * 2, unit.location);
            Power.DrainAllCharges();
        }
    }
}