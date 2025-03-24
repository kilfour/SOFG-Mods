using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals;
using System.Linq;

namespace Witching
{
    public class Witch : UAE
    {
        public List<ICanUpdateRituals> updaters =
            new List<ICanUpdateRituals>();

        public string WitchesTitle = "Witch";

        public Witch(Location location, Society society)
            : base(location, society)
        {
            person.stat_might = 1;
            person.stat_lore = 4;
            person.stat_intrigue = 1;
            person.stat_command = 1;
            person.isMale = false;
            person.age = 42;
            person.hasSoul = true;
            person.gold = 20;

            AddWitchesRitual(new Empower.RitualUpdater(this));
            AddWitchesRitual(new TheWitchesStarvation.RitualUpdater());

            person.receiveTrait(new TruthSpeaker());
            person.receiveTrait(new WitchesPower(this));

            minions[0] = new M_Crow(map);
        }

        public void AddWitchesRitual(ICanUpdateRituals updater)
        {
            updaters.Add(updater);
        }

        public override string getName()
        {
            if (person.overrideName != null && person.overrideName.Length != 0)
            {
                return person.overrideName;
            }
            return WitchesTitle + " " + person.firstName;
        }

        public override bool isCommandable()
        {
            return true;
        }

        public override bool hasStartingTraits()
        {
            return true;
        }

        public override List<Trait> getStartingTraits()
        {
            List<Trait> list =
                new List<Trait>
                {
                    new HighPriestess(),
                    new WitchesChanneler(),
                    new Soothsayer()
                };
            return list;
        }

        public override Sprite getPortraitForeground()
        {
            return EventManager.getImg("witching.witch.png");
        }

        public void UpdateRituals(WitchesPower witchesPower, Location newLocation)
        {
            updaters.ForEach(a => a.UpdateRituals(this, witchesPower, newLocation));
        }

        public WitchesPower GetPower()
        {
            return person.traits.FirstOrDefault(a => a is WitchesPower) as WitchesPower;
        }
    }
}


