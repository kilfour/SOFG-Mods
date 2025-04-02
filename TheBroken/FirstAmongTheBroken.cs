using Assets.Code;
using UnityEngine;
using Common;
using TheBroken.Traits;

namespace TheBroken
{
    public class FirstAmongTheBroken : UAE
    {
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
            person.receiveTrait(new GravenPresence());
            location.AddProperty(new Shard(location) { charge = 50 });
        }

        public override string getName()
        {
            if (person.overrideName != null && person.overrideName.Length != 0)
            {
                return person.overrideName;
            }
            return "First Broken " + person.firstName;
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
    }
}


