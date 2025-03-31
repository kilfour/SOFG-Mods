using System;
using Assets.Code;
using UnityEngine;

namespace TheBroken
{
    public class MyProperty : Property
    {
        public MyProperty(Location loc)
            : base(loc) { charge = 50; }

        public override string getName()
        {
            return "MyProp";
        }

        public override string getDesc()
        {
            return "No Description yet.";
        }

        public override Sprite getSprite(World world)
        {
            return world.iconStore.agony;
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

        public override standardProperties getPropType()
        {
            return standardProperties.OTHER;
        }

        public override bool deleteOnZero()
        {
            return false;
        }
    }
}