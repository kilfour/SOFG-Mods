using System;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

namespace TheBroken.Rituals
{
    public class TheFracture : Challenge
    {
        public FirstAmongTheBroken theFirst;
        public TheFracture(FirstAmongTheBroken theFirst, Location location)
            : base(location)
        {
            this.theFirst = theFirst;
        }

        public override string getName()
        {
            return "The Fracture";
        }

        public override string getDesc()
        {
            return "Switches the broken from settlers to followers.";
        }

        public override string getRestriction()
        {
            return "Can be performed anywhere, anytime.";
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
            return challengeStat.INTRIGUE;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Intrigue", unit.getStatIntrigue()));
            return Math.Max(1, unit.getStatIntrigue());
        }

        public override double getComplexity()
        {
            return 10;
        }

        public override int getCompletionMenace()
        {
            return 0;
        }

        public override int getCompletionProfile()
        {
            return 3;
        }
        public override bool validFor(UA unit)
        {
            return true;
        }

        public override void complete(UA unit)
        {
            theFirst.LeadingFlock = !theFirst.LeadingFlock;
        }
    }
}