using Assets.Code;
using Witching.Traits;

namespace Witching.Rituals.Bolts.Nuts
{
    public interface ICanUpdateRituals
    {
        void UpdateRituals(UAE caster, WitchesPower witchesPower, Location newLocation);
    }
}


