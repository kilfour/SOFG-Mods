using Assets.Code;
using Common;
using TheBroken.Modifiers;
using UnityEngine;

namespace TheBroken
{
    public class Broken : UAEN
    {
        public Location targetLocation;

        public Broken(Location location, Society society, Person person)
            : base(location, society, person)
        {
            person.shadow = 1.0;
            person.alert_aware = true;
            person.alert_halfShadow = true;
            person.alert_maxShadow = true;
            person.stat_might = 1;
            person.stat_intrigue = 1;
            person.stat_lore = 1;
            person.stat_command = 1;
        }

        public override void turnTickAI()
        {
            if (location.HasProperty<Splinter>())
                return;

            if (targetLocation == null)
            {
                targetLocation = Shard.FindTargetLocation(location);
                if (targetLocation == null)
                {
                    map.addUnifiedMessage(this, person.unit.location, "The Stone Holds Firm", getName() + " could not find a valid location to found a new Shard and has returned to his village.", "Broken Disbands", force: true);
                    disband(map, "Could not found a new Shard.");
                }
            }

            if (location == targetLocation)
            {
                if (location.HasProperty<Shard>())
                {
                    targetLocation = null;
                    return;
                }
                if (!location.HasFarms())
                {
                    targetLocation = null;
                    return;
                }
                location.AddProperty(new Shard(location));
                map.addUnifiedMessage(this, person.unit.location, "The Stone Gives Way", getName() + " has founded a new Shard.", "Broken Founds Shard", force: true);
                disband(map, "Founded a new Shard.");
                return;
            }
            if (task == null && targetLocation != null)
                task = new Task_GoToLocation(targetLocation);
        }

        public override bool definesName()
        {
            return true;
        }

        public override string getName()
        {
            return "Broken " + person.firstName;
        }

        public override Sprite getPortraitForeground()
        {
            return EventManager.getImg("the-broken.cultist-1.png");
        }
    }
}