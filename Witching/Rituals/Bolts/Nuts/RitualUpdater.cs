using Assets.Code;
using Witching.Traits;

namespace Witching.Rituals.Bolts.Nuts
{
    public abstract class RitualUpdater<TRitual> : ICanUpdateRituals where TRitual : Ritual
    {
        protected virtual bool CanBeCastOnHeroes => true;

        protected virtual bool CanBeCastOnRulers => true;

        protected virtual bool CanBeCastOnHero(Person person) { return false; }

        protected virtual bool CanBeCastOnRuler(SettlementHuman humanSettlement) { return false; }

        protected abstract Ritual GetRitual(Location newLocation, WitchesPower witchesPower, Person prey);

        public void UpdateRituals(UAE caster, WitchesPower witchesPower, Location newLocation)
        {
            RemoveRituals<TRitual>.From(caster);
            MaybeAddRitualToUnits(caster, witchesPower, newLocation);
            MaybeAddRitualToRuler(caster, witchesPower, newLocation);
        }

        private void MaybeAddRitualToUnits(UAE caster, WitchesPower witchesPower, Location location)
        {
            foreach (var unit in location.units)
                MaybeAddRitualToUnit(caster, witchesPower, location, unit);
        }

        protected void MaybeAddRitualToUnit(UAE caster, WitchesPower witchesPower, Location newLocation, Unit unit)
        {
            if (!CanBeCastOnHeroes) return;
            if (!UnitIsValidTarget(caster, unit)) return;
            if (!UnitIsRightTypeOfTarget(unit)) return;
            if (!CanBeCastOnHero(unit.person)) return;
            caster.rituals.Add(GetRitual(newLocation, witchesPower, unit.person));
        }
        protected static bool UnitIsValidTarget(UAE caster, Unit unit)
        {
            return unit != caster && unit.person != null && unit != caster.map.awarenessManager.getChosenOne();
        }

        protected virtual bool UnitIsRightTypeOfTarget(Unit unit)
        {
            return UnitIsEnemyHeroOrAcolyte(unit);
        }

        protected static bool UnitIsEnemyHeroOrAcolyte(Unit unit)
        {
            return !unit.isCommandable() && (unit is UAG || unit is UAA);
        }

        protected static bool UnitIsDarkEmpireAgent(Unit unit)
        {
            return unit.isCommandable() && (unit is UAE);
        }

        protected void MaybeAddRitualToRuler(UAE caster, WitchesPower witchesPower, Location location)
        {
            if (!CanBeCastOnRulers) return;
            var humanSettlement = location.settlement as SettlementHuman;
            if (humanSettlement == null) return;
            if (humanSettlement.ruler == null) return;
            if (!CanBeCastOnRuler(humanSettlement)) return;
            caster.rituals.Add(GetRitual(location, witchesPower, humanSettlement.ruler));
        }
    }
}


