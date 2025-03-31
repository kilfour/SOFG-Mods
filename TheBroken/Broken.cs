using System.Linq;
using Assets.Code;
using Common;
using UnityEngine;

namespace TheBroken
{
    public class Broken : UAEN
    {
        public Location locationForNewShard;

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
            if (locationForNewShard == null)
            {
                locationForNewShard = FindLocationForNewShard();
                if (locationForNewShard == null)
                {
                    map.addUnifiedMessage(this, person, "The Broken", getName() + " could not find a valid location to found a new Shard and has returned to his village.", "Uprising", force: true);
                    disband(map, "Could not found a new Shard.");
                }
            }

            if (location == locationForNewShard)
            {
                location.AddProperty(new Shard(location));
                map.addUnifiedMessage(this, person, "The Broken", getName() + " has founded a new Shard.", "Uprising", force: true);
                disband(map, "Founded a new Shard.");
                return;
            }
            if (task == null && locationForNewShard != null)
                task = new Task_GoToLocation(locationForNewShard);
        }

        private Location FindLocationForNewShard()
        {
            var result =
                from mapLocation in map.locations
                where IsPotentialLocation(mapLocation)
                let distance = map.getStepDist(location, mapLocation)
                let isInfiltrated = mapLocation.IsFullyInfiltrated()
                orderby distance, isInfiltrated
                select new { location = mapLocation, distance, isInfiltrated };
            return result.FirstOrDefault()?.location;
        }

        private bool IsPotentialLocation(Location potentialLocation)
        {
            if (!potentialLocation.HasFarms())
                return false;
            if (potentialLocation.HasProperty<Shard>())
                return false;
            return true;
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