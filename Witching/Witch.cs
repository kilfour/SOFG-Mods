using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals;
using System;
using System.Linq;

namespace Witching
{
    public class Witch : UAE
    {
        public Empower.RitualUpdater empowerUpdater;

        public List<ICanUpdateRituals> updaters =
            new List<ICanUpdateRituals>();

        public Witch(Location location, Society society)
            : base(location, society)
        {
            person.stat_might = 1;
            person.stat_lore = 4;
            person.stat_intrigue = 1;
            person.stat_command = 2;
            person.isMale = false;
            person.age = 42;
            person.hasSoul = true;
            empowerUpdater = new Empower.RitualUpdater(this);
            AddWitchesRitual(new TheWitchesStarvation.RitualUpdater());
            person.receiveTrait(new WitchesPower(this));
        }

        public void AddWitchesRitual(ICanUpdateRituals updater)
        {
            updaters.Add(updater);
        }

        public override string getName()
        {
            return "Witch " + base.person.firstName;
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
                    new HungerWorship(),
                    new WitchesChanneler(),
                    new Prophet(),
                    new Fundraiser()
                };
            return list;
        }

        public override Sprite getPortraitForeground()
        {
            return EventManager.getImg("witching.witch.png");
        }

        public void UpdateRituals(WitchesPower witchesPower, Location newLocation)
        {
            empowerUpdater.UpdateRituals(this, witchesPower, newLocation);
            updaters.ForEach(a => a.UpdateRituals(this, witchesPower, newLocation));
        }

        public WitchesPower GetPower()
        {
            return person.traits.FirstOrDefault(a => a is WitchesPower) as WitchesPower;
        }
    }
}


