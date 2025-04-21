using System;
using System.Collections.Generic;
using Assets.Code;
using Common;
using TheBroken.Modifiers;
using UnityEngine;

namespace TheBroken.Rituals
{
    public class Uprising : Ritual
    {
        public Uprising(Location location)
            : base(location) { }

        public override string getName()
        {
            return "Uprising";
        }

        public override string getDesc()
        {
            return "Encourage The Broken to break down the cities walls. Only one Splinter per map.";
        }

        public override string getRestriction()
        {
            return "Can only be performed in a human city. Cast again to remove Splinter.";
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
            return 10;
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
            if (!unit.location.HasCity()) return false;
            return true;
        }

        public override void complete(UA unit)
        {
            if (unit.location.HasProperty<Splinter>())
            {
                unit.location.RemoveProperty<Splinter>();
                return;
            }
            foreach (var loc in unit.map.locations)
            {
                loc.RemoveProperty<Splinter>();
            }
            unit.location.AddProperty(new Splinter(unit.location));
        }
    }
}