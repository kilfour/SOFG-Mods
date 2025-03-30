using Assets.Code;
using Witching.Traits;

namespace Witching.Rituals.Bolts.Nuts
{
    public abstract class LocationRitualUpdater<TRitual> : ICanUpdateRituals where TRitual : Ritual
    {
        protected abstract Ritual GetRitual(Location newLocation, WitchesPower witchesPower);

        public void UpdateRituals(UAE caster, WitchesPower witchesPower, Location newLocation)
        {
            RemoveRituals<TRitual>.From(caster);
            MaybeAddRitualToLocation(caster, witchesPower, newLocation);
        }

        protected virtual bool CanBeCastOnLocation(Location location) { return false; }

        protected void MaybeAddRitualToLocation(UAE caster, WitchesPower witchesPower, Location location)
        {
            if (!CanBeCastOnLocation(location)) return;
            caster.rituals.Add(GetRitual(location, witchesPower));
        }
    }
}


