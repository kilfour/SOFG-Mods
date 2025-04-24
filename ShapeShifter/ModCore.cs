using Assets.Code;
using Assets.Code.Modding;
using Common;
using ShapeShifter.Traits;

namespace ShapeShifter
{
    public class ModCore : ModKernel
    {

        public override void beforeMapGen(Map map)
        {
            map.overmind.agentsGeneric.Add(new ShapeShifterAbstract(map));
            ShapeShifter.NumberAllowed = 1;
            ShapeShifter.CurrentNumberOfShapeShifters = 0;
        }

        public override void onModsInitiallyLoaded()
        {
            base.onModsInitiallyLoaded();
        }

        public override bool interceptChallengeCompletion(Challenge challenge, UA unit, Task_PerformChallenge task_PerformChallenge)
        {
            if (!(unit is ShapeShifter)) return false;
            var mimicTrait = unit.person.GetTrait<Mimic>();
            if (mimicTrait.victim == null) return false;
            unit.task = null;
            challenge.claimedBy = null;
            challenge.complete(unit);
            if (unit.location.settlement is SettlementHuman settlementHuman)
            {
                if (settlementHuman.order != null)
                    settlementHuman.order.challengePerformedInLocation(unit, challenge);
            }
            if (mimicTrait.victim != null)
            {
                mimicTrait.victim.unit.addMenace(challenge.getCompletionMenaceAfterDifficulty());
                mimicTrait.victim.unit.addProfile(challenge.getCompletionProfile());
            }
            int xPGain = challenge.getXPGain(unit);
            unit.person.receiveXP(xPGain);
            bool flag2 = unit.isCommandable() || unit is UAE;
            double priority = 0.75;
            unit.map.addMessage(unit.getName() + " completes: " + challenge.getName(), priority, flag2, unit.location.hex);
            World.log(unit.getName() + " completes challenge: " + challenge.getName());
            unit.completeChallenge(challenge);
            unit.map.stats.event_challengeComplete(unit, challenge);
            foreach (Location location in unit.map.locations)
            {
                location.populateStandardChallenges();
            }
            if (challenge.hasStandardCompletionMessage())
            {
                challenge.popCompletionMessage(unit);
            }
            foreach (Trait trait2 in unit.person.traits)
            {
                trait2.completeChallenge(challenge);
            }
            return true;
        }
    }
}