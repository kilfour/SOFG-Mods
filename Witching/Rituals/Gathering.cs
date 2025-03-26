using System;
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

        public override void onImmediateBegin(UA witchUnit)
        {
            var witch = witchUnit as Witch;
            foreach (var unit in witch.location.units)
            {
                if (unit is Witch otherWitch
                    && witch != otherWitch
                    && otherWitch.task is Task_PerformChallenge task
                    && task.challenge is Gathering)
                {
                    witch.task = new GeneratePower(witch.location);
                    return;
                }
            }
            base.onImmediateBegin(witch);
        }

        public override void turnTick(UA witchUnit)
        {
            base.turnTick(witchUnit);
            witchUnit.midchallengeTimer = 0;
            var witch = witchUnit as Witch;
            var covenPower = 0.0;
            foreach (Unit unit in map.units)
            {
                if ((unit is UAA acolyte) && acolyte.order == witch.society)
                {
                    if (unit.task != null && unit.task is GeneratePower)
                    {
                        covenPower++;
                    }
                    else if (unit.location == location)
                        unit.task = new GeneratePower(witch.location);
                    else if (!(unit.task is Task_GoToLocation))
                        unit.task = new Task_GoToLocation(witch.location);
                }
                if (unit is Witch otherWitch && witch.location == otherWitch.location)
                {
                    if (unit.task != null && unit.task is GeneratePower)
                    {
                        covenPower += 2;
                    }
                }
            }

            var suspicion = covenPower;
            var unrestRemoved = 0.0;
            if (witch.location.settlement is SettlementHuman settlementHuman)
            {
                var unrest = getStandardPropertyLevel(witch.location, Property.standardProperties.UNREST);
                suspicion = Math.Max(0, covenPower - unrest);
                unrestRemoved = covenPower - suspicion;
                Property.addToProperty("Ritual: Coven Gathering", Property.standardProperties.UNREST, 0 - unrestRemoved, witch.location);
            }
            if (unrestRemoved > 0)
            {
                var maddnessToAdd = unrestRemoved * 2;
                Property.addToProperty("Ritual: Coven Gathering", Property.standardProperties.MADNESS, maddnessToAdd, witch.location);
            }
            witch.GetPower().Charges += (int)covenPower;
            witch.addMenace(suspicion);
            witch.addProfile(suspicion * 2);

        }
        internal double getStandardPropertyLevel(Location location, Property.standardProperties form)
        {
            foreach (Property property in location.properties)
            {
                if (property.getPropType() == form)
                {
                    return property.charge;
                }
            }
            return 0.0;
        }
        public override bool ignoreInterruptionWarning()
        {
            return true;
        }
    }
}