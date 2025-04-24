
using Assets.Code;
using UnityEngine;
using Common;
using System;
using System.Collections.Generic;

namespace ShapeShifter.Rituals
{
    public class RepurposeTheDead : Ritual
    {
        public RepurposeTheDead(Location location)
            : base(location) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("shape-shifter.skull-and-ball.png");
        }

        public override string getName()
        {
            return "Repurpose the Dead";
        }

        public override string getDesc()
        {
            return @"Claim a fallen soul for your own. Increases you recruitement points and number of allowed shapeshifters by 1.";
        }

        public override string getRestriction()
        {
            return "Only available in a location with a dead soul.";
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

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }
        public override double getComplexity()
        {
            return 50;
        }

        public override int getCompletionMenace()
        {
            return 25;
        }

        public override int getCompletionProfile()
        {
            return 10;
        }

        public override void complete(UA unit)
        {
            if (unit is ShapeShifter)
            {
                ShapeShifter.NumberAllowed++;
            }
            unit.location.RemoveProperty<Pr_FallenHuman>();
            map.overmind.availableEnthrallments++;
        }

        public override bool validFor(UA unit)
        {
            return unit.location.PropertyIs<Pr_FallenHuman>(a => a.charge > 0.0);
        }
    }
}