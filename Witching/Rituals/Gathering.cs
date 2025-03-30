using System;
using System.Linq;
using Assets.Code;
using UnityEngine;
using Witching.Bolts;

namespace Witching.Rituals
{
    public class Gathering : Ritual
    {
        public Gathering(Location location)
            : base(location) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("witching.circle-of-candles.png");
        }

        public override string getName()
        {
            return "Coven's Gathering";
        }

        public override string getDesc()
        {
            return "Gather the coven's acolytes. Once they arrive they will start generating power for the casting witch. Other witches can also join by casting the same ritual in the same location, contributing double the power. Unrest will be consumed and transformed into maddness. The ritual ends when there is no more unrest.";
        }

        public override string getRestriction()
        {
            return "The ritual needs to be started in a location where unrest exceeds 50.";
        }

        public override double getProfile()
        {
            return 0.5;
        }

        public override double getMenace()
        {
            return 0.25;
        }

        public override bool isIndefinite()
        {
            return true;
        }

        public override bool validFor(UA unit)
        {
            if (unit.task is Task_PerformChallenge task && task.challenge is Gathering)
                return true;
            return location.GetUnrest() > 50;
        }

        public override void onImmediateBegin(UA witch)
        {
            foreach (var unit in location.units)
            {
                if (OtherWitchHasStartedAGathering(witch as Witch, unit))
                {
                    witch.task = new GeneratePower(location);
                    return;
                }
            }
            base.onImmediateBegin(witch);
        }

        private static bool OtherWitchHasStartedAGathering(Witch witch, Unit unit)
        {
            return
                unit is Witch otherWitch
                && witch != otherWitch
                && otherWitch.task is Task_PerformChallenge task
                && task.challenge is Gathering;
        }

        public override void turnTick(UA witchUnit)
        {
            base.turnTick(witchUnit);
            witchUnit.midchallengeTimer = 0;
            var witch = witchUnit as Witch;
            var power = MyMath.Sum(from unit in map.units select GetPowerFromUnit(witch, unit));
            var result = new UnrestCalculation(location.GetUnrest(), power);
            Because.Of("Ritual: Coven Gathering").Remove(result.UnrestToRemove).Unrest(location);
            Because.Of("Ritual: Coven Gathering").Add(result.UnrestToRemove).Madness(location);
            witch.addMenace(0.25 * result.PowerToAdd);
            witch.addProfile(0.5 * result.PowerToAdd);
            witch.GetPower().Charges += Convert.ToInt32(result.PowerToAdd);
            if (!result.Continue)
                witch.task = null;
        }

        public class UnrestCalculation
        {
            public double PowerToAdd { get; }
            public double UnrestToRemove { get; }
            public bool Continue { get; }
            public UnrestCalculation(double unrest, double power)
            {
                var suspicion = Math.Max(0, power + 1 - unrest);
                PowerToAdd = Math.Round(power - suspicion);
                UnrestToRemove = PowerToAdd + 1;
                Continue = Math.Round(unrest) > UnrestToRemove;
            }
        }

        private double GetPowerFromUnit(Witch witch, Unit unit)
        {
            return GetPowerFromAcolyte(witch, unit) + GetPowerFromOtherWitch(unit);
        }

        private double GetPowerFromOtherWitch(Unit unit)
        {
            if (unit is Witch otherWitch && location == otherWitch.location)
                if (unit.task != null && unit.task is GeneratePower)
                    return 2;
            return 0;
        }

        private double GetPowerFromAcolyte(Witch witch, Unit unit)
        {
            if (UnitIsAnAcolyte(witch, unit))
                if (unit.task != null)
                    if (unit.task is GeneratePower) { return 1; }
                    else if (unit.task is Task_GoToLocation) { return 0; }
                    else unit.task = new Task_GoToLocation(location);
                else if (unit.location == location)
                    unit.task = new GeneratePower(location);
                else
                    unit.task = new Task_GoToLocation(location);
            return 0;
        }

        private static bool UnitIsAnAcolyte(Witch witch, Unit unit)
        {
            return (unit is UAA acolyte) && acolyte.order == witch.society;
        }

        public override bool ignoreInterruptionWarning()
        {
            return true;
        }
    }
}