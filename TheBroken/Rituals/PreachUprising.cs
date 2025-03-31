using System;
using System.Collections.Generic;
using Assets.Code;
using Common;
using UnityEngine;

namespace TheBroken.Rituals
{
    public class PreachUprising : Ritual
    {
        public PreachUprising(Location location)
            : base(location) { }

        public override string getName()
        {
            return "Preach Uprising.";
        }

        public override string getDesc()
        {
            return "Encourage this Shard to extend the influence of The Broken.";
        }

        public override string getRestriction()
        {
            return "Needs a Shard to be present with atleast 100 magnitude, drains 100 magnitude.";
        }

        public override string getCastFlavour()
        {
            return "From the quiet comes the spark. One rises, not to speak, but to spread.";
        }

        public override Sprite getSprite()
        {
            return EventManager.getImg("the-broken.uprising.png");
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
            return 10;
        }
        public override bool validFor(UA unit)
        {
            var shard = unit.location.GetPropertyOrNull<Shard>();
            if (shard == null) return false;
            if (shard.charge < 100) return false;
            return true;
        }

        public override void complete(UA unit)
        {
            var shard = unit.location.GetPropertyOrNull<Shard>();
            shard.charge -= 100;
            Person p = new Person(map.soc_dark);
            var broken = new Broken(unit.location, map.soc_dark, p);
            broken.location.units.Add(broken);
            map.units.Add(broken);
        }
    }
}