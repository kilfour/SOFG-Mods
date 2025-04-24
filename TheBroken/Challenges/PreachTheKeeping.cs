using System;
using System.Collections.Generic;
using Assets.Code;

using Common;
using TheBroken.Modifiers;
using UnityEngine;

namespace TheBroken.Challenges
{
    [LegacyDoc(Order = "1.3.2", Caption = "Preach The Keeping", Content =
@"Builds the **The Keeping** modifier, which causes the village to withhold food from outsiders.  
Shard charge needs to be greater than 50.  
Comsumes 50 charge.")]
    public class PreachTheKeeping : Challenge
    {
        public PreachTheKeeping(Location location)
            : base(location) { }

        public override string getName()
        {
            return "The Keeping";
        }

        public override string getDesc()
        {
            return "Causes the village to withold food from it's neighbours for 20 turns. Casting it when a Keeping is allready going on adds 20 more turns.";
        }

        public override string getRestriction()
        {
            return "Needs a farming village with a Shard present with atleast 50 magnitude.";
        }

        public override string getCastFlavour()
        {
            return "Let the grain stay where it was sown. Let the mouths beyond go quiet.";
        }

        public override Sprite getSprite()
        {
            return EventManager.getImg("the-broken.wheat-basket.png");
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
            return 30;
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
            if (shard.charge < 50) return false;
            return true;
        }

        public override void complete(UA unit)
        {
            var keeping = unit.location.GetPropertyOrNull<TheKeeping>();
            if (keeping == null)
                unit.location.AddProperty(new TheKeeping(unit.location));
            else
                keeping.charge += 20;
        }
    }
}