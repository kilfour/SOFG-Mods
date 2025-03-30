using System.Collections.Generic;
using Assets.Code;
using System.Linq;

namespace Witching.Rituals.Bolts.Nuts
{
    public static class RemoveRituals<TRitual> where TRitual : Ritual
    {
        public static void From(UAE caster)
        {
            foreach (var item in GetRitualsToRemove(caster).ToList())
                caster.rituals.Remove(item);
        }

        private static IEnumerable<Challenge> GetRitualsToRemove(UAE caster)
        {
            return
                from ritual in caster.rituals
                where ritual is TRitual
                select ritual;
        }
    }
}


