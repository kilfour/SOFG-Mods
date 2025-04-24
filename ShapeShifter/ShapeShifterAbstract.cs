using Assets.Code;
using UnityEngine;
using Common.ImageSelector;

namespace ShapeShifter
{
    public class ShapeShifterAbstract : UAE_Abstraction
    {
        public ShapeShifterAbstract(Map map)
            : base(map, -1) { }

        public override bool validTarget(Location location)
        {
            if (AgentCapReached()) return false;
            return ShapeShifter.CurrentNumberOfShapeShifters < ShapeShifter.NumberAllowed;
        }

        private bool AgentCapReached()
        {
            return map.world.map.overmind.nEnthralled >= map.world.map.overmind.getAgentCap();
        }

        public override void createAgent(Location target)
        {
            ShapeShifter.CurrentNumberOfShapeShifters++;
            var uA = new ShapeShifter(target, GetRandomImageIndex.For<ShapeShifter>(4, target.map));
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
            return EventManager.getImg("shape-shifter.shape-shifter-1.png");
        }

        public override string getName()
        {
            return "A Shapeshifter";
        }

        public override string getDesc()
        {
            return "Mimics a living hero. While in disguise, uses that hero's stats. Profile and Menace from actions are transferred to the real hero. Can revert at will.";
        }

        public override string getFlavour()
        {
            return "It walks in the skin of heroes, wears their glories, commits their crimes.";
        }

        public override string getRestrictions()
        {
            return $"Can have a maximum of {ShapeShifter.NumberAllowed} shifters.";
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

