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
            return "Call all your acolytes to this location.";
        }

        public override string getCastFlavour()
        {
            return "The Coven is here.";
        }

        public override double getComplexity()
        {
            return 0;
        }

        public override int getCompletionMenace()
        {
            return 0;
        }

        public override int getCompletionProfile()
        {
            return 0;
        }

        public override bool isIndefinite()
        {
            return true;
        }

        public override void turnTick(UA witchUnit)
        {
            base.turnTick(witchUnit);
            var witch = witchUnit as Witch;
            foreach (Unit unit in map.units)
            {
                if ((unit is UAA acolyte) && acolyte.order == witch.society && !(unit.task is Task_GoToLocation))
                {
                    if (unit.task is GeneratePower)
                        witch.GetPower().Charges++;
                    else if (unit.location == location)
                        unit.task = new GeneratePower(witch.location);
                    else
                        unit.task = new Task_GoToLocation(witch.location);
                }
            }
        }
    }

    public class GeneratePower : Task
    {
        public Location target;

        public GeneratePower(Location loc)
        {
            target = loc;
        }

        public override string getShort()
        {
            return "Generating Power";
        }

        public override string getLong()
        {
            return getShort();
        }

        public override void turnTick(Unit acolyteUnit)
        {
            foreach (var unit in target.units)
            {
                if (unit is Witch witch && witch.task is Task_PerformChallenge task && task.challenge is Gathering)
                    continue;
                unit.task = null;
            }
        }

        public override bool isBusy()
        {
            return true;
        }
    }
}