using System;
using Assets.Code;
using UnityEngine;

namespace TheBroken
{
    public class MyAcolyte : UAEN
    {
        public Rt_GhastEnshadow rt_enshadow;

        public MyAcolyte(Location loc, Society sg, Person p)
            : base(loc, sg, p)
        {

            // Person p = new Person(map.soc_dark);
            // UAEN_Ghast uAEN_Ghast = new UAEN_Ghast(u.location, map.soc_dark, p);
            // uAEN_Ghast.location.units.Add(uAEN_Ghast);
            // map.units.Add(uAEN_Ghast);

            p.shadow = 1.0;
            p.alert_aware = true;
            p.alert_maxShadow = true;
            p.alert_halfShadow = true;
            rt_enshadow = new Rt_GhastEnshadow(loc, this);
            rituals.Add(rt_enshadow);
            p.stat_might = 6;
            p.stat_intrigue = 1;
            p.stat_lore = 4;
            p.stat_command = 1;
            p.species = map.species_undead;
        }

        public override void turnTickAI()
        {
            // int stepDist = map.getStepDist(location2, base.location);

        }

        public override bool definesName()
        {
            return true;
        }

        public override string getName()
        {
            return "Ghast";
        }

        public override bool isCommandable()
        {
            return false;
        }

        public override Sprite getPortraitBackground()
        {
            return map.world.iconStore.standardBack;
        }

        public override Sprite getPortraitForeground()
        {
            return map.world.textureStore.evil_ghast;
        }
    }
}