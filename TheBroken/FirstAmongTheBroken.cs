using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Common;

namespace TheBroken
{
    public class FirstAmongTheBroken : UAE
    {
        // public List<ICanUpdateRituals> updaters =
        //     new List<ICanUpdateRituals>();


        // food calc : 
        // base = minpop (12) 
        // calc 0.1 + Habitability - minHabitabilityForHumans (0.15)  times city_foodPerHabilitability 75.0
        // base + calc

        //  0.5 - 0.14 * 75

        public FirstAmongTheBroken(Location location, Society society)
            : base(location, society)
        {
            person.stat_might = Constants.Might;
            person.stat_lore = Constants.Lore;
            person.stat_intrigue = Constants.Intrigue;
            person.stat_command = Constants.Command;
            person.isMale = false;
            person.age = 42;
            person.hasSoul = true;
            person.gold = Constants.Gold;
            location.AddProperty(new MyProperty(location));
        }

        public override string getName()
        {
            return "First Among The Broken";
        }

        public override bool isCommandable()
        {
            return true;
        }

        public override bool hasStartingTraits()
        {
            return false;
        }

        public override Sprite getPortraitForeground()
        {
            return EventManager.getImg("the-broken.first-among-the-broken.png");
        }

        // public void UpdateRituals(WitchesPower witchesPower, Location newLocation)
        // {
        //     updaters.ForEach(a => a.UpdateRituals(this, witchesPower, newLocation));
        // }
    }
}


