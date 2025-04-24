using System;
using System.Linq;
using Assets.Code;
using UnityEngine;
using Common;

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
            if (UnitIsGathering(unit))
                return true;

            if (ThereIsAGatheringHere(unit.location))
                return true;

            return unit.location.GetUnrest() > 50;
        }

        private bool UnitIsGathering(Unit unit)
        {
            return unit.task is Task_PerformChallenge task && task.challenge is Gathering;
        }

        private bool ThereIsAGatheringHere(Location location)
        {
            return (from nearbyUnit in location.units
                    where UnitIsGathering(nearbyUnit)
                    select nearbyUnit).Any();
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
            var result = new UnrestCalculation(witch.location.GetUnrest(), power);
            Because.Of("Ritual: Coven Gathering")
                .Transform(result.UnrestToRemove)
                .UnrestIntoMadness(witch.location);
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

        private double GetPowerFromUnit(Witch witch, Assets.Code.Unit unit)
        {
            return GetPowerFromAcolyte(witch, unit) + GetPowerFromOtherWitch(witch, unit);
        }

        private double GetPowerFromOtherWitch(Witch witch, Assets.Code.Unit unit)
        {
            if (unit is Witch otherWitch && witch.location == otherWitch.location)
                if (unit.task != null && unit.task is GeneratePower)
                    return 2;
            return 0;
        }

        private double GetPowerFromAcolyte(Witch witch, Assets.Code.Unit unit)
        {
            if (UnitIsAnAcolyte(witch, unit))
                if (unit.task != null)
                    if (unit.task is GeneratePower) { return 1; }
                    else if (unit.task is Task_GoToLocation) { return 0; }
                    else unit.task = new Task_GoToLocation(witch.location);
                else if (unit.location == witch.location)
                    unit.task = new GeneratePower(witch.location);
                else
                    unit.task = new Task_GoToLocation(witch.location);
            return 0;
        }

        private static bool UnitIsAnAcolyte(Witch witch, Assets.Code.Unit unit)
        {
            return (unit is UAA acolyte) && acolyte.order == witch.society;
        }

        public override bool ignoreInterruptionWarning()
        {
            return true;
        }
    }
}