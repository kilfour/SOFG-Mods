using Assets.Code;
using Common;
using ShapeShifter.Rituals;

namespace ShapeShifter.Traits
{
    [LegacyDoc(Order = "1.3", Caption = "Mimic Trait", Content =
@"The Shapeshifter's signature trait.  
If the shifter is in original form, she loses 2 profile and 1 menace per turn.  
If, however the shifter is mimicking, she:
- gains 1 profile and 2 menace per turn.
- when in the same location as the original hero, that hero loses 1 sanity per turn.
- when in the same location as the original hero's home location, security is reduced by 3.")]
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
                shapeShifter.addMenace(2);
                if (victim.unit.location == shapeShifter.location)
                    victim.sanity--;
            }
            else
            {
                shapeShifter.addProfile(-2);
                shapeShifter.addMenace(-1);
            }
            UpdateRituals(shapeShifter.location);
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
            UpdateRituals(shapeShifter.location);
        }

        public override void onMove(Location current, Location destination)
        {
            base.onMove(current, destination);
            UpdateRituals(destination);
        }

        public void UpdateRituals(Location location)
        {
            shapeShifter.rituals.Clear();
            foreach (var unit in location.units)
            {
                MaybeAddRitualToUnit(unit);
            }
            if (victim != null)
                shapeShifter.rituals.Add(new Revert(location));
            else
                shapeShifter.rituals.Add(new ShadowTendrils(location));

            if (location.PropertyIs<Pr_FallenHuman>(a => a.charge > 0.0))
                shapeShifter.rituals.Add(new RepurposeTheDead(location));
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