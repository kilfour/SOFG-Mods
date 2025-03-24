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
            uA.maxHp = 3;
            uA.hp = 3;
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

        // public override Sprite getBackground()
        // {
        //     return map.world.iconStore.standardBack;
        // }

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
            return "Gathering at their Coven the witches seek to bring chaos into this world. They have developed a fascination for, and ways to, manipulate the hunger. They perform blaspmephous rituals and worship at their covens in order to obtain their power, with which they can encourage people infected by the hunger to feed. Their High Priestesses can even inflict the hunger on unsuspectiong bystanders. Their Truth Speaking ability allows them to extract funds from nearby rulers. Their prophets, the soothsayers do not have access to the normal Witches Rituals, instead focusing on preaching and temple building. ";
        }

        public override string getFlavour()
        {
            return "Witches have decent lore, but not much else. Don't take them into combat. When in the same location, one witch can gift all her power to another.";
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
            return 1;
        }

        public override int getStatCommand()
        {
            return 1;
        }
    }

}

