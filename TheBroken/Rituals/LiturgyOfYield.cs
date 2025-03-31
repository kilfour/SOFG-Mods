using System;
using System.Collections.Generic;
using Assets.Code;
using Common;
using UnityEngine;

namespace TheBroken.Rituals
{
    public class LiturgyOfYield : Ritual
    {
        public LiturgyOfYield(Location location)
            : base(location) { }

        public override string getName()
        {
            return "Liturgy of Yield";
        }

        public override string getDesc()
        {
            return "Collects 5O gold from your followers.";
        }

        public override string getRestriction()
        {
            return "Needs a Shard to be present with atleast 50 magnitude. Drains 50 magnitude.";
        }

        public override string getCastFlavour()
        {
            return "Not all offerings are of flesh. Some give what they have hoarded. Some give what they cannot afford.";
        }

        public override Sprite getSprite()
        {
            return EventManager.getImg("the-broken.tithe.png");
        }

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.INTRIGUE;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Intrigue", unit.getStatIntrigue()));
            return Math.Max(1, unit.getStatIntrigue());
        }

        public override double getComplexity()
        {
            return 20;
        }

        public override int getCompletionMenace()
        {
            return 2;
        }

        public override int getCompletionProfile()
        {
            return 5;
        }
        public override bool validFor(UA unit)
        {
            var shard = unit.location.GetPropertyOrNull<Shard>();
            if (shard == null) return false;
            if (shard.charge >= 50) return false;
            return true;
        }

        public override void complete(UA unit)
        {
            unit.location.GetPropertyOrNull<Shard>().charge -= 50;
            unit.person.gold += 50;
        }
    }
}