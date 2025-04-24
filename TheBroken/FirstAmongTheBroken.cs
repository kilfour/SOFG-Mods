using Assets.Code;
using UnityEngine;
using Common;
using TheBroken.Traits;
using TheBroken.Modifiers;
using System.Linq;
using TheBroken.Rituals;

namespace TheBroken
{
    [LegacyDoc(Order = "1", Caption = "The Broken", Content =
@"This mod introduces a unique agent: **The First Among the Broken**.  

They can only be recruited on a location with Farms.  
Upon entering the frey, the set up a 'Shard' at their starting location which shows up as a modifier.  
The First mainly focusses on automatic infiltration of farming villages and creating unrest.
")]
    public class FirstAmongTheBroken : UAE
    {

        public bool LeadingFlock;

        public FirstAmongTheBroken(Location location, Society society)
            : base(location, society)
        {
            person.stat_might = Constants.Might;
            person.stat_lore = Constants.Lore;
            person.stat_intrigue = Constants.Intrigue;
            person.stat_command = Constants.Command;
            person.isMale = true;
            person.age = 42;
            person.hasSoul = true;
            person.gold = Constants.Gold;
            person.receiveTrait(new GravenPresence(this));
            location.AddProperty(new Shard(location) { charge = 150 });
            rituals.Add(new Uprising(this, location));
            LeadingFlock = false;
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

        public static FirstAmongTheBroken GetInstance(Map map)
        {
            return
                (from unit in map.units
                 where unit is FirstAmongTheBroken
                 select unit as FirstAmongTheBroken)
                    .FirstOrDefault();
        }
    }
}


