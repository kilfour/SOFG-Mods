using System.Linq;
using Assets.Code;
using Witching.Traits;
using Witching.Rituals.Bolts;

namespace Witching.Rituals
{
    public class RaiseFunds : WitchesRitual
    {
        public class RitualUpdater : RitualUpdater<RaiseFunds>
        {
            public Witch witch;

            public RitualUpdater(Witch witch)
            {
                this.witch = witch;
            }

            protected override bool CanBeCastOnHeroes => false;

            protected override bool CanBeCastOnRuler(SettlementHuman humanSettlement)
            {
                return !humanSettlement.fundingActions.Any(a => a.heroIndex == witch.person.index);
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower, Person prey)
            {
                return new RaiseFunds(location, witchesPower, prey);
            }
        }

        protected override int RequiredCharges => 1;

        public RaiseFunds(Location location, WitchesPower witchesPowerTrait, Person prey)
            : base(location, witchesPowerTrait, prey) { }

        public override string getName()
        {
            return "Speak to " + prey.getName() + ".";
        }

        public override string getDesc()
        {
            return "Spend a Witches Power in order to motivate " + prey.getName() + " to hire you as a Truth Speaker.";
        }

        public override string getRestriction()
        {
            return "Requires one Witches Power, and ruler is not already  funding you.";
        }

        public override string getCastFlavour()
        {
            return "You have endeared yourself to " + prey.getName() + " and the ruler might decide to give you some gold in the future.";
        }

        public override double getComplexity()
        {
            return 20;
        }

        public override int getCompletionMenace()
        {
            return 3;
        }

        public override int getCompletionProfile()
        {
            return 5;
        }

        public override void complete(UA witch)
        {
            var oldLocation = map.locations[witch.homeLocation];
            if (oldLocation.settlement is SettlementHuman humanSettlement)
            {
                humanSettlement.fundingActions.RemoveAll(a => a.heroIndex == witch.person.index);
            }
            witch.homeLocation = witch.location.index;
            var settlement = witch.location.settlement as SettlementHuman;
            settlement.fundingActions.Add(new Act_FundHero(witch.location, witch.person));
            settlement.ruler.increasePreference(witch.person.index + 10000);
            RitualComplete();
        }
    }
}