using System;
using System.Linq;
using Assets.Code;
using UnityEngine;

namespace Witching.Rituals
{
    public class Gathering : Ritual
    {
        public Gathering(Location location)
            : base(location) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("witching.witches-gathering.png");
        }

        public override string getName()
        {
            return "Coven's Gathering";
        }

        public override string getDesc()
        {
            return "Gather the coven's acolytes. Once they arrive they will start generating power for the casting witch. Other witches can also join by casting the same ritual in the same location, contributing double the power. The witch that started the gathering will receive one profile and two menace for each power generated. This can be circumvented by gathering in locations with unrest. The unrest will consume the menace and transform it into maddness.";
        }

        public override double getProfile()
        {
            return 1;
        }

        public override double getMenace()
        {
            return 1;
        }

        public override bool isIndefinite()
        {
            return true;
        }

        public override void onImmediateBegin(UA witch)
        {
            foreach (var unit in witch.location.units)
            {
                if (OtherWitchHasStartedAGathering(witch as Witch, unit))
                {
                    witch.task = new GeneratePower(witch.location);
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
            var suspicion = GetSuspicion(witch.location, power);
            witch.addMenace(suspicion);
            witch.addProfile(suspicion * 2);
            witch.GetPower().Charges += (int)power;
        }

        private double GetPowerFromUnit(Witch witch, Unit unit)
        {
            return GetPowerFromAcolyte(witch, unit) + GetPowerFromOtherWitch(witch, unit);
        }

        private static double GetPowerFromOtherWitch(Witch witch, Unit unit)
        {
            if (unit is Witch otherWitch && witch.location == otherWitch.location)
                if (unit.task != null && unit.task is GeneratePower)
                    return 2;
            return 0;
        }

        private double GetPowerFromAcolyte(Witch witch, Unit unit)
        {
            if (UnitIsAnAcolyte(witch, unit))
                if (unit.task == null)
                    if (unit.location == location)
                        unit.task = new GeneratePower(witch.location);
                    else
                        unit.task = new Task_GoToLocation(witch.location);
                else if (unit.task is GeneratePower) { return 1; }
            return 0;
        }

        private double GetSuspicion(Location location, double covenPower)
        {
            var unrest = getStandardPropertyLevel(location, Property.standardProperties.UNREST);
            var suspicion = Math.Max(0, covenPower - unrest);
            var unrestRemoved = covenPower - suspicion;
            Property.addToProperty("Ritual: Coven Gathering", Property.standardProperties.UNREST, 0 - unrestRemoved, location);
            if (unrestRemoved > 0)
                Property.addToProperty("Ritual: Coven Gathering", Property.standardProperties.MADNESS, unrestRemoved * 2, location);
            return suspicion;
        }

        private static bool UnitIsAnAcolyte(Witch witch, Unit unit)
        {
            return (unit is UAA acolyte) && acolyte.order == witch.society;
        }

        internal double getStandardPropertyLevel(Location location, Property.standardProperties form)
        {
            foreach (Property property in location.properties)
                if (property.getPropType() == form)
                    return property.charge;
            return 0.0;
        }
        public override bool ignoreInterruptionWarning()
        {
            return true;
        }
    }
}