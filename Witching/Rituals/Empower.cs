using Assets.Code;
using Witching.Traits;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;
using Common;

namespace Witching.Rituals
{
    [LegacyDoc(Order = "1.3.2", Caption = "Empower", Content =
@"Whenever two or more witches share the same location,
they get access to this ritual which allows one witch to transfer all of it's power to another one.")]
    public class Empower : WitchesMobileRitual
    {
        public class RitualUpdater : RitualUpdater<Empower>
        {
            public Witch witch;

            public RitualUpdater(Witch witch)
            {
                this.witch = witch;
            }

            protected override bool UnitIsRightTypeOfTarget(Assets.Code.Unit unit)
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

        public Empower(Location location, WitchesPower witchesPowerTrait, Person prey)
            : base(location, witchesPowerTrait, 1, prey) { }

        public override string getName()
        {
            return "Empower " + Prey.Person.getName() + ".";
        }

        public override string getDesc()
        {
            return "Transfer all your Witches Power charge to " + Prey.Person.getName() + ".";
        }

        public override string getRestriction()
        {
            return "Must have atleast one Witches Power.";
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
            var preyPower = Prey.Person.GetTrait<WitchesPower>();
            preyPower.Charges += Power.GetCharges();
            Power.DrainAllCharges();
        }
    }
}