using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;
using Witching.Bolts;
namespace Witching.Rituals
{
    public class WitchesStories : WitchesPoweredRitual
    {
        public class RitualUpdater : LocationRitualUpdater<WitchesStories>
        {
            protected override bool CanBeCastOnLocation(Location location)
            {
                return location.settlement is SettlementHuman;
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower)
            {
                return new WitchesStories(location, witchesPower);
            }
        }

        public WitchesStories(Location location, WitchesPower witchesPower)
            : base(location, witchesPower, 1) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("witching.book-of-madness.png");
        }

        public override string getName()
        {
            return "The Witch's Stories";
        }

        public override string getDesc()
        {
            return @"When in a human settlement disperse all your power to spread nightmare's to the population, raising madness by twice your power.";
        }

        public override string getRestriction()
        {
            return "Must have atleast one Witches Power. Only available in a human settlement.";
        }

        public override double getComplexity()
        {
            return 30;
        }

        public override int getCompletionMenace()
        {
            return 5;
        }

        public override int getCompletionProfile()
        {
            return 10;
        }

        public override void complete(UA unit)
        {
            Because.Of("The Witch's Stories").Add(Power.GetCharges() * 2).Madness(unit.location);
            FullPowerRitualComplete();
        }
    }
}