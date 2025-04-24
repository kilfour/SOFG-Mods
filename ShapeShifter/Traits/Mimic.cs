using Assets.Code;
using ShapeShifter.Rituals;

namespace ShapeShifter.Traits
{
    public class Mimic : Trait
    {
        public ShapeShifter shapeShifter;
        public Person victim;

        public Mimic(ShapeShifter shapeShifter)
        {
            this.shapeShifter = shapeShifter;
        }

        public override string getName()
        {
            if (victim != null)
                return "Mimic: " + victim.getName();
            return "Mimic";
        }

        public override string getDesc()
        {
            return "It has no voice of its own, no will but the master's.";
        }

        public override void turnTick(Person person)
        {
            base.turnTick(person);
            if (victim != null)
            {
                shapeShifter.addProfile(1);
                shapeShifter.addMenace(0.5);
                if (victim.unit.location == shapeShifter.location)
                    victim.sanity--;
            }
            else
            {
                shapeShifter.addProfile(-2);
                shapeShifter.addMenace(-1);
            }
            UpdateRituals();
        }

        public override int getMightChange()
        {
            if (victim != null) return victim.getStatMight();
            return 0;
        }
        public override int getCommandChange()
        {
            if (victim != null) return victim.getStatCommand();
            return 0;
        }

        public override int getIntrigueChange()
        {
            if (victim != null) return victim.getStatIntrigue();
            return 0;
        }

        public override int getLoreChange()
        {
            if (victim != null) return victim.getStatLore();
            return 0;
        }

        public override int getSecurityChange(Unit unit, SettlementHuman settlementHuman)
        {
            if (victim == null) return 0;
            if (settlementHuman.location.index != victim.unit.homeLocation)
                return 0;
            return -3;
        }

        public override void onAcquire(Person person)
        {
            UpdateRituals();
        }

        public override void onMove(Location current, Location destination)
        {
            base.onMove(current, destination);
            UpdateRituals();
        }

        public void UpdateRituals()
        {
            shapeShifter.rituals.Clear();
            foreach (var unit in shapeShifter.location.units)
            {
                MaybeAddRitualToUnit(unit);
            }
            if (victim != null)
                shapeShifter.rituals.Add(new Revert(shapeShifter.location));
            else
                shapeShifter.rituals.Add(new ShadowTendrils(shapeShifter.location));
        }

        private void MaybeAddRitualToUnit(Unit unit)
        {
            if (unit.person == null) return;
            if (unit.isCommandable()) return;
            if (unit == unit.map.awarenessManager.getChosenOne()) return;
            if (victim != null && unit == victim.unit) return;
            if (!(unit is UAG || unit is UAA)) return;
            shapeShifter.rituals.Add(new MimicRitual(shapeShifter.location, shapeShifter, unit.person));
        }
    }
}