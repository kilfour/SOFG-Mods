using System.Linq;
using Assets.Code;
using UnityEngine;

namespace TheBroken
{
    public class FirstAmongTheBrokenAbstract : UAE_Abstraction
    {
        public FirstAmongTheBrokenAbstract(Map map)
            : base(map, -1) { }

        public override bool validTarget(Location location)
        {
            if (AgentCapReached()) return false;
            return true;
            // if (location.settlement == null) return false;
            // return location.settlement.subs.Any(sub => sub.getName() == "Coven");
        }

        private bool AgentCapReached()
        {
            return map.world.map.overmind.nEnthralled >= map.world.map.overmind.getAgentCap();
        }

        public override void createAgent(Location target)
        {
            var uA = new FirstAmongTheBroken(target, map.soc_dark);
            // uA.maxHp = Constants.Hp;
            // uA.hp = Constants.Hp;
            uA.person.stat_might = getStatMight();
            uA.person.stat_lore = getStatLore();
            uA.person.stat_intrigue = getStatIntrigue();
            uA.person.stat_command = getStatCommand();
            target.units.Add(uA);
            target.map.overmind.agents.Add(uA);
            target.map.units.Add(uA);
            uA.person.shadow = 1.0;
            uA.person.clearAllPreferences();
            GraphicalMap.selectedUnit = uA;
            target.map.overmind.availableEnthrallments--;
        }

        public override Sprite getForeground()
        {
            return EventManager.getImg("the-broken.first-among-the-broken.png");
        }

        public override string getName()
        {
            return "First Among The Broken";
        }

        public override string getDesc()
        {
            return "A shattered prophet who rises as a dangerous messiah to the downtrodden. They offer twisted salvation to the desperate, and in doing so, unravel civilization from within.";
        }

        public override string getFlavour()
        {
            return "Todo .";
        }

        public override string getRestrictions()
        {
            return "Todo.";
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

