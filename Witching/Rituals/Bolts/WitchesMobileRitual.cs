using Assets.Code;
using Witching.Traits;

namespace Witching.Rituals.Bolts
{
    public abstract class WitchesMobileRitual : WitchesRitual
    {
        public WitchesMobileRitual(Location location, WitchesPower witchesPower, Person prey)
            : base(location, witchesPower, prey) { }

        public override void turnTick(UA caster)
        {
            base.turnTick(caster);
            Follow(caster, prey, map);
        }

        private static void Follow(UA stalker, Person target, Map map)
        {
            if (stalker.location == target.getLocation())
            {
                return;
            }
            if (stalker.movesTaken < stalker.getMaxMoves())
            {
                foreach (Location neighbour in stalker.location.getNeighbours())
                {
                    if (neighbour == target.getLocation())
                    {
                        map.adjacentMoveTo(stalker, neighbour);
                        return;
                    }
                }
            }
            if (stalker.task is Task_PerformChallenge task_PerformChallenge)
            {
                task_PerformChallenge.challenge.claimedBy = null;
                stalker.task = null;
            }
            return;
        }
    }
}