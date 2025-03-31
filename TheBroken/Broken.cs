using Assets.Code;
using UnityEngine;

namespace TheBroken
{
    public class Broken : UAEN
    {
        public Broken(Location location, Society society, Person person)
            : base(location, society, person)
        {
            person.shadow = 1.0;
            person.stat_might = 1;
            person.stat_intrigue = 1;
            person.stat_lore = 1;
            person.stat_command = 1;
        }

        // public override void turnTickAI()
        // {
        //     int stepDist = map.getStepDist(location2, base.location);

        // }

        // public override bool definesName()
        // {
        //     return true;
        // }

        // public override string getName()
        // {
        //     return "Ghast";
        // }

        public override Sprite getPortraitForeground()
        {
            return EventManager.getImg("the-broken.cultist-1.png");
        }
    }
}