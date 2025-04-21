using System;
using System.Collections.Generic;
using Assets.Code;
using Common;
using TheBroken.Modifiers;
using UnityEngine;

namespace TheBroken.Challenges
{
    public class TheFracture : Challenge
    {
        public TheFracture(Location location)
            : base(location) { }

        public override string getName()
        {
            return "The Fracture";
        }

        public override string getDesc()
        {
            return "Increases the magnitude of the Shard by 75.";
        }

        public override string getRestriction()
        {
            return "Needs a Shard to be present. Can't raise magnitude above 300.";
        }

        public override string getCastFlavour()
        {
            return "A village doesn't fall in a day. First comes the fracture. Then the rot. Then the worship.";
        }

        public override Sprite getSprite()
        {
            return EventManager.getImg("the-broken.preach-the-fracture.png");
        }

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.COMMAND;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Command", unit.getStatCommand()));
            return Math.Max(1, unit.getStatCommand());
        }

        public override double getComplexity()
        {
            return 50;
        }

        public override int getCompletionMenace()
        {
            return 3;
        }

        public override int getCompletionProfile()
        {
            return 5;
        }
        public override bool validFor(UA unit)
        {
            var shard = unit.location.GetPropertyOrNull<Shard>();
            if (shard == null) return false;
            if (shard.charge >= 300) return false;
            return true;
        }

        public override void complete(UA unit)
        {
            var shard = unit.location.GetPropertyOrNull<Shard>();
            shard.charge += 75;
            if (shard.charge > 300)
                shard.charge = 300;
        }
    }
}