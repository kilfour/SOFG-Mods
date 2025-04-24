using System.Collections.Generic;
using Assets.Code;
using Common;
using ShapeShifter.Traits;
using UnityEngine;

namespace ShapeShifter.Rituals
{
    public class Revert : Ritual
    {
        public Revert(Location location)
            : base(location) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("shape-shifter.revert.png");
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.OTHER;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("One Turn:", 1));
            return 1;
        }

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }

        public override string getName()
        {
            return "Revert to Shadow.";
        }

        public override string getDesc()
        {
            return "Shed the assumed identity, returning to the Shapeshifter's original form.";
        }

        public override string getCastFlavour()
        {
            return "The mask melts. The stolen face forgotten. What remains is silence and smoke.";
        }

        public override double getComplexity()
        {
            return 1;
        }

        public override int getCompletionMenace()
        {
            return 1;
        }

        public override int getCompletionProfile()
        {
            return 2;
        }

        public override void complete(UA unit)
        {
            unit.person.GetTrait<Mimic>().victim = null;
        }
    }
}