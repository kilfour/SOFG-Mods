using Assets.Code;
using Common;
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
            return location.HasFarms();
        }

        private bool AgentCapReached()
        {
            return map.world.map.overmind.nEnthralled >= map.world.map.overmind.getAgentCap();
        }

        public override void createAgent(Location target)
        {
            var firstAmongTheBroken = new FirstAmongTheBroken(target, map.soc_dark);
            firstAmongTheBroken.person.stat_might = getStatMight();
            firstAmongTheBroken.person.stat_lore = getStatLore();
            firstAmongTheBroken.person.stat_intrigue = getStatIntrigue();
            firstAmongTheBroken.person.stat_command = getStatCommand();
            target.units.Add(firstAmongTheBroken);
            target.map.overmind.agents.Add(firstAmongTheBroken);
            target.map.units.Add(firstAmongTheBroken);
            firstAmongTheBroken.person.shadow = 1.0;
            firstAmongTheBroken.person.clearAllPreferences();
            target.map.overmind.availableEnthrallments--;
            target.map.overmind.agentsUnique.Remove(this);
            GraphicalMap.selectedUnit = firstAmongTheBroken;
            firstAmongTheBroken.person.skillPoints++;
            map.world.ui.checkData();
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
            return "They were the first to kneel, and the last to be remembered by any true name. Their village vanished not in fire, but in silence — fields left unharvested, doors left open, prayers carved into the walls in ash and blood. When they rose again, they did not speak. They did not need to. Wherever they walk, obedience follows like rot in the root. They are not a leader. They are a fracture given form, the living shape of surrender. To see them is not to fear death. It is to forget resistance ever made sense at all";
        }

        public override string getRestrictions()
        {
            return "Can only be called upon where there is a Farming Village.";
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

