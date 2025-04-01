using System;
using Assets.Code;
using UnityEngine;

namespace TheBroken.Modifiers
{
    public class TheKeeping : Property
    {
        public TheKeeping(Location loc)
            : base(loc) { charge = 20; }

        public override string getName()
        {
            return "The Keeping";
        }

        public override string getDesc()
        {
            return "What is sown in pain is kept in faith. Let the lord hunger. Let the Broken feast.";
        }

        public override Sprite getSprite(World world)
        {
            return EventManager.getImg("the-broken.wheat-and-candle.png");
        }

        public override standardProperties getPropType()
        {
            return standardProperties.OTHER;
        }

        public override void turnTick()
        {
            influences.Add(new ReasonMsg("The Keeping holds as time wears thin..", -1));
        }

        public override double foodGenMult()
        {
            var settlement = location.settlement as SettlementHuman;
            var population = settlement.population;
            var foodPerHabilitability = location.map.param.city_foodPerHabilitability;
            var popMin = (double)location.map.param.city_popMin;
            var habitability = (double)location.hex.getHabilitability();
            var minimumHabitability = location.map.param.mapGen_minHabitabilityForHumans;
            var amountOfFood = popMin;
            amountOfFood += Math.Ceiling(0.1 + (habitability - minimumHabitability) * foodPerHabilitability);
            foreach (Subsettlement sub in settlement.subs)
                amountOfFood *= sub.getFoodGenMult();
            return Math.Max(0.1, population / amountOfFood);
        }
    }
}