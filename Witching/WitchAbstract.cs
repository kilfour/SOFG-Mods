using System.Linq;
using Assets.Code;
using UnityEngine;


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
            var society =
                target.settlement.subs
                    .Select(a => a as Sub_Temple)
                    .First(a => a != null).order;
            var uA = new Witch(target, society);
            uA.person.stat_might = getStatMight();
            uA.person.stat_lore = getStatLore();
            uA.person.stat_intrigue = getStatIntrigue();
            uA.person.stat_command = getStatCommand();
            uA.person.skillPoints++;
            target.units.Add(uA);
            target.map.overmind.agents.Add(uA);
            target.map.units.Add(uA);
            uA.person.shadow = 1.0;
            uA.person.extremeHates.Clear();
            uA.person.extremeHates.Clear();
            uA.person.hates.Clear();
            uA.person.likes.Clear();
            GraphicalMap.selectedUnit = uA;
            target.map.overmind.availableEnthrallments--;
        }

        public override Sprite getBackground()
        {
            return map.world.iconStore.standardBack;
        }

        public override Sprite getForeground()
        {
            return EventManager.getImg("witching.witch.png");
        }

        public override string getName()
        {
            return "A Witch";
        }

        public override string getDesc()
        {
            return "The Witch starts as a Coven's acolyte.";
        }

        public override string getFlavour()
        {
            return "Manipulates the powers of darkness.";
        }

        public override string getRestrictions()
        {
            return "Requires a Coven.";
        }

        public override int getStatMight()
        {
            return 1;
        }

        public override int getStatLore()
        {
            return 4;
        }

        public override int getStatIntrigue()
        {
            return 2;
        }

        public override int getStatCommand()
        {
            return 3;
        }
    }

}

