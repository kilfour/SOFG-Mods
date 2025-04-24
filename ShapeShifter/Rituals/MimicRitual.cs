using System.Collections.Generic;
using Assets.Code;
using Common;
using ShapeShifter.Traits;
using UnityEngine;

namespace ShapeShifter.Rituals
{
    [LegacyDoc(Order = "1.2.1", Caption = "Mimic Ritual", Content =
@"The Shapeshifter's signature ability.  
Targets an enemy hero and upon completion the shifter adds the hero's stats to it's own.  
As long as it maintains the shifted form all profile and menace gains from any challenge performed is applied to the originally targeted hero.")]
    public class MimicRitual : Ritual
    {
        public ShapeShifter shapeShifter;
        public Person victim;
        public MimicRitual(Location location, ShapeShifter shapeShifter, Person victim)
            : base(location)
        {
            this.shapeShifter = shapeShifter;
            this.victim = victim;
        }

        public override Sprite getSprite()
        {
            return victim.getPortrait();
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.OTHER;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Per Turn:", 1));
            return 1;
        }

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }

        public override string getName()
        {
            return "Mimic " + victim.getName() + ".";
        }

        public override string getDesc()
        {
            return "Assume the form and abilities of a living hero.";
        }

        public override string getCastFlavour()
        {
            return "The shadow slips beneath the skin, borrowing voice and memory. It walks, speaks, and sins in another's name.";
        }

        public override double getComplexity()
        {
            return 3;
        }

        public override int getCompletionMenace()
        {
            return 3;
        }

        public override int getCompletionProfile()
        {
            return 5;
        }

        public override void complete(UA _)
        {
            shapeShifter.person.GetTrait<Mimic>().victim = victim;
        }
    }
}