using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

namespace TheBroken.Rituals
{
    public class Uprising : Ritual
    {
        public FirstAmongTheBroken theFirst;

        public Uprising(FirstAmongTheBroken theFirst, Location location)
            : base(location)
        {
            this.theFirst = theFirst;
        }

        public override string getName()
        {
            return "Uprising";
        }

        public override string getDesc()
        {
            return "Toggles The Broken's behaviour from settling new lands to following the first and back again.";
        }

        public override string getRestriction()
        {
            return "Can be used anywhere, anytime.";
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
            return challengeStat.OTHER;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("One Turn", 1));
            return 1;
        }

        public override double getComplexity()
        {
            return 1;
        }

        public override int getCompletionMenace()
        {
            return 0;
        }

        public override int getCompletionProfile()
        {
            return 0;
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