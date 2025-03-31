using System;
using Assets.Code;
using UnityEngine;

namespace TheBroken
{
    public class Shard : Property
    {
        public Shard(Location loc)
            : base(loc) { charge = 0; }

        public override string getName()
        {
            return "The Broken";
        }

        public override string getDesc()
        {
            return "Not all breaks are loud. Some appear like root through stone.";
        }

        public override Sprite getSprite(World world)
        {
            return EventManager.getImg("the-broken.shrine-of-first-breaking.png");
        }

        // public override double foodGenMult()
        // {
        //     var settlement = location.settlement as SettlementHuman;
        //     var population = settlement.population;
        //     var foodPerHabilitability = location.map.param.city_foodPerHabilitability;
        //     var popMin = (double)location.map.param.city_popMin;
        //     var habitability = (double)location.hex.getHabilitability();
        //     var minimumHabitability = location.map.param.mapGen_minHabitabilityForHumans;
        //     var amountOfFood = popMin;
        //     amountOfFood += Math.Ceiling(0.1 + (habitability - minimumHabitability) * foodPerHabilitability);
        //     foreach (Subsettlement sub in settlement.subs)
        //         amountOfFood *= sub.getFoodGenMult();
        //     return Math.Max(0.1, population / amountOfFood);
        // }

        public override standardProperties getPropType()
        {
            return standardProperties.OTHER;
        }
    }
}