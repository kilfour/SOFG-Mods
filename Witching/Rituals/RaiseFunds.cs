using System.Linq;
using Assets.Code;
using Witching.Traits;
using Witching.Rituals.Bolts;
using UnityEngine;

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

            protected override bool CanBeCastOnRuler(Person person)
            {
                var settlement = person.unit.location.settlement as SettlementHuman;
                return !settlement.fundingActions.Any(a => a.heroIndex == witch.person.index);
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
            return "Embezzle from " + prey.getName() + ".";
        }

        public override string getDesc()
        {
            return "Spend a Witches Power in order to motivate " + prey.getName() + " to fund you.";
        }

        public override string getRestriction()
        {
            return "Must be a ruler that is not yet funding you.";
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
            var settlement = prey.unit.location.settlement as SettlementHuman;
            settlement.fundingActions.Add(new Act_FundHero(prey.unit.location, witch.person));
            RitualComplete();
        }
    }
}