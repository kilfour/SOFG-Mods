using System.Linq;
using Assets.Code;
using Witching.Traits;
using Witching.Rituals.Bolts;
using UnityEngine;

namespace Witching.Rituals
{
    public class Empower : WitchesMobileRitual
    {
        public class RitualUpdater : RitualUpdater<Empower>
        {
            public Witch witch;

            public RitualUpdater(Witch witch)
            {
                this.witch = witch;
            }

            protected override bool UnitIsRightTypeOfTarget(Unit unit)
            {
                return unit is Witch;
            }

            protected override bool CanBeCastOnHero(Person person)
            {
                return person.society == witch.society;
            }

            protected override bool CanBeCastOnRulers => false;

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower, Person prey)
            {
                return new Empower(location, witchesPower, prey);
            }
        }

        protected override int RequiredCharges => 1;

        public Empower(Location location, WitchesPower witchesPowerTrait, Person prey)
            : base(location, witchesPowerTrait, prey) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("witching.witch.png");
        }

        public override string getName()
        {
            return "Empower " + prey.getName() + ".";
        }

        public override string getDesc()
        {
            return "Transfer all your Witches Power charge to " + prey.getName() + ".";
        }

        public override string getRestriction()
        {
            return "Must be a fellow member witch of your coven.";
        }

        public override string getCastFlavour()
        {
            return "The Power is transferred.";
        }

        public override double getComplexity()
        {
            return 10;
        }

        public override int getCompletionMenace()
        {
            return 0;
        }

        public override int getCompletionProfile()
        {
            return 0;
        }

        public override void complete(UA _)
        {
            var preyPower = prey.traits.FirstOrDefault(a => a is WitchesPower) as WitchesPower;
            preyPower.Charges += witchesPower.Charges;
            witchesPower.Charges = 0;
        }
    }
}