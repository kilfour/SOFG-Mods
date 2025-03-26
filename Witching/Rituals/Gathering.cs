using System.Linq;
using Assets.Code;
using Witching.Traits;
using Witching.Rituals.Bolts;
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

        public override void turnTick(UA witch)
        {
            base.turnTick(witch);
            foreach (Unit unit in map.units)
            {
                if ((unit is UAA acolyte) && acolyte.order == witch.society)
                {
                    unit.task = new Task_GoToLocation(witch.location);
                }

            }
        }
    }
}