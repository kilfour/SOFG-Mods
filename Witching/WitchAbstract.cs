using System.Linq;
using Assets.Code;
using UnityEngine;
using Common;
using Common.ImageSelector;

namespace Witching
{
    public class WitchAbstract : UAE_Abstraction
    {
        public WitchAbstract(Map map)
            : base(map, -1) { }

        public override bool validTarget(Location location)
        {
            if (AgentCapReached()) return false;
            if (location.settlement == null) return false;
            return location.settlement.subs.Any(sub => sub.getName() == "Coven");
        }

        private bool AgentCapReached()
        {
            return map.world.map.overmind.nEnthralled >= map.world.map.overmind.getAgentCap();
        }

        public override void createAgent(Location target)
        {
            var society = target.GetHolyOrderOrNull();
            var uA = new Witch(target, society, GetRandomImageIndex.For<Witch>(5, target.map));
            uA.maxHp = 3;
            uA.hp = 3;
            uA.person.stat_might = getStatMight();
            uA.person.stat_lore = getStatLore();
            uA.person.stat_intrigue = getStatIntrigue();
            uA.person.stat_command = getStatCommand();
            target.units.Add(uA);
            target.map.overmind.agents.Add(uA);
            target.map.units.Add(uA);
            uA.person.shadow = 1.0;
            uA.person.clearAllPreferences();
            target.map.overmind.availableEnthrallments--;
            map.world.ui.checkData();
        }

        public override Sprite getForeground()
        {
            return EventManager.getImg("witching.witch-1.png");
        }

        public override string getName()
        {
            return "A Witch";
        }

        public override string getDesc()
        {
            return "Gathering at their Coven the witches seek to bring chaos into this world. They have developed a fascination for, and ways to, manipulate the hunger and madness. They gather at places of unrest in order to obtain their power.";
        }

        public override string getFlavour()
        {
            return "Witches have decent lore, but not much else. Don't take them into combat. Most of their rituals/challenges only show up when in the right location.";
        }

        public override string getRestrictions()
        {
            return "Requires a Coven.";
        }

        public override int getStatMight()
        {
            return Constants.Might;
        }

        public override int getStatLore()
        {
            return Constants.Lore;
        }

        public override int getStatIntrigue()
        {
            return Constants.Intrigue;
        }

        public override int getStatCommand()
        {
            return Constants.Command;
        }
    }
}

