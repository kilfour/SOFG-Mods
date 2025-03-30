
using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;

namespace Witching.Rituals
{
    public class StartDeepCult : WitchesPoweredRitual
    {
        public class RitualUpdater : LocationRitualUpdater<StartDeepCult>
        {
            protected override bool CanBeCastOnLocation(Location location)
            {
                return location.HasProperty<Pr_MalignCatch>();
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower)
            {
                return new StartDeepCult(location, witchesPower);
            }
        }

        public StartDeepCult(Location location, WitchesPower witchesPower)
            : base(location, witchesPower, 30) { }

        public override Sprite getSprite()
        {
            return map.world.iconStore.deepOnes;
        }

        public override string getName()
        {
            return "Start Deep Cult";
        }

        public override string getDesc()
        {
            return "Starts a Deep One Cult in this location, removes the Malign Catch modifier. Deep Ones require you to keep them safe as they mature, but once they do they can spread by themselves and count towards victory";
        }

        public override string getCastFlavour()
        {
            return "The creatures were human, once. Driven by madness and lust for power, immortality or knowledge, they fell into the worship of creatures from the abyssal planes beneath the waves, and made bargains no sane man could make, underwent rituals which slowly changed them into nightmarish blasphemies unfit for the world of mankind.";
        }

        public override string getRestriction()
        {
            return "Costs 30 Power. Only available in a location with a Malign Catch.";
        }

        public override double getProfile()
        {
            return map.param.ch_deeponesstartcult_aiProfile;
        }

        public override double getMenace()
        {
            return map.param.ch_deeponesstartcult_aiMenace;
        }

        public override double getComplexity()
        {
            return map.param.ch_deeponesstartcult_complexity;
        }

        public override int getCompletionProfile()
        {
            return map.param.ch_deeponesstartcult_completionProfile;
        }

        public override int getCompletionMenace()
        {
            return map.param.ch_deeponesstartcult_completionMenace;
        }

        public override int getSimplificationLevel()
        {
            return 0;
        }

        public override void complete(UA unit)
        {
            unit.location.AddProperty(new Pr_DeepOneCult(unit.location) { charge = 1.0 });
            unit.location.RemoveProperty<Pr_MalignCatch>();
            RitualComplete();
        }
    }
}