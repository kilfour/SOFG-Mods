using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals;
using Witching.Rituals.Bolts.Nuts;
using Common;

namespace Witching
{
    public class Witch : UAE
    {
        public int ImageIndex;

        public List<ICanUpdateRituals> updaters =
            new List<ICanUpdateRituals>();

        public string WitchesTitle = "Witch";

        public Witch(Location location, Society society, int imageIndex)
            : base(location, society)
        {
            ImageIndex = imageIndex;
            person.stat_might = Constants.Might;
            person.stat_lore = Constants.Lore;
            person.stat_intrigue = Constants.Intrigue;
            person.stat_command = Constants.Command;
            person.isMale = false;
            person.age = 42;
            person.hasSoul = true;
            person.gold = Constants.Gold;
            var power = new WitchesPower(this);
            person.receiveTrait(new TruthSpeaker());
            person.receiveTrait(new Soothsayer(0));
            person.receiveTrait(power);
            rituals.Add(new Gathering(location));
            rituals.Add(new WitchesStories(location, power));
            AddWitchesRitual(new Empower.RitualUpdater(this));
            AddWitchesRitual(new RepurposeTheDead.RitualUpdater(this));
            AddWitchesRitual(new WordsNotHerOwn.RitualUpdater());
            AddWitchesRitual(new StartDeepCult.RitualUpdater());
            AddWitchesRitual(new TheWitchesHunger.RitualUpdater());
            AddWitchesRitual(new TheWitchesStarvation.RitualUpdater());
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
            return false;
        }

        public override Sprite getPortraitForeground()
        {
            return EventManager.getImg("witching.witch-" + ImageIndex.ToString() + ".png");
        }

        public void UpdateRituals(WitchesPower witchesPower, Location newLocation)
        {
            updaters.ForEach(a => a.UpdateRituals(this, witchesPower, newLocation));
        }

        public WitchesPower GetPower()
        {
            return person.GetTrait<WitchesPower>();
        }

        public TruthSpeaker GetTruthSpeaker()
        {
            return person.GetTrait<TruthSpeaker>();
        }
    }
}


