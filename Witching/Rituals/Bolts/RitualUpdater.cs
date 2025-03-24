using System.Collections.Generic;
using Assets.Code;
using System.Linq;
using Witching.Traits;

namespace Witching.Rituals.Bolts
{
    public abstract class RitualUpdater<TRitual> : ICanUpdateRituals where TRitual : Ritual
    {
        protected virtual bool CanBeCastOnHeroes => true;

        protected virtual bool CanBeCastOnRulers => true;

        protected virtual bool CanBeCastOnHero(Person person) { return false; }

        protected virtual bool CanBeCastOnRuler(Person person) { return false; }

        protected abstract Ritual GetRitual(Location newLocation, WitchesPower witchesPower, Person prey);

        public void UpdateRituals(UAE caster, WitchesPower witchesPower, Location newLocation)
        {
            RemoveRituals(caster, GetRitualsToRemove(caster).ToList());
            MaybeAddRitualToUnits(caster, witchesPower, newLocation);
            MaybeAddRitualToRuler(caster, witchesPower, newLocation);
        }

        private static void RemoveRituals(UAE caster, List<Challenge> toRemove)
        {
            foreach (var item in toRemove) caster.rituals.Remove(item);
        }

        private static IEnumerable<Challenge> GetRitualsToRemove(UAE caster)
        {
            return
                from ritual in caster.rituals
                where ritual is TRitual
                select ritual;
        }

        private void MaybeAddRitualToUnits(UAE caster, WitchesPower witchesPower, Location newLocation)
        {
            foreach (var unit in newLocation.units)
                MaybeAddRitualToUnit(caster, witchesPower, newLocation, unit);
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

        protected void MaybeAddRitualToRuler(UAE caster, WitchesPower witchesPower, Location newLocation)
        {
            if (!CanBeCastOnRulers) return;
            var ruler = GetRulerOrNull(newLocation);
            if (ruler == null) return;
            if (!CanBeCastOnRuler(ruler)) return;
            caster.rituals.Add(GetRitual(newLocation, witchesPower, ruler));
        }

        protected static Person GetRulerOrNull(Location location)
        {
            if (location.settlement is SettlementHuman humanSettlement)
                return humanSettlement.ruler;
            return null;
        }
    }
}


