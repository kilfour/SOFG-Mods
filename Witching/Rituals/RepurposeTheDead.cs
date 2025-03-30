
using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;
namespace Witching.Rituals
{
    public class RepurposeTheDead : WitchesPoweredRitual
    {
        public Witch witch;

        public class RitualUpdater : LocationRitualUpdater<RepurposeTheDead>
        {
            public Witch witch;

            public RitualUpdater(Witch witch)
            {
                this.witch = witch;
            }

            protected override bool CanBeCastOnLocation(Location location)
            {
                return location.settlement is SettlementHuman;
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower)
            {
                return new RepurposeTheDead(witch, location, witchesPower);
            }
        }

        public RepurposeTheDead(Witch witch, Location location, WitchesPower witchesPower)
            : base(location, witchesPower, 1) { this.witch = witch; }

        public override Sprite getSprite()
        {
            return EventManager.getImg("witching.skull-and-ball.png");
        }

        public override string getName()
        {
            return "Repurpose the Dead";
        }

        public override string getDesc()
        {
            return @"Claim a fallen soul for your own.";
        }

        public override string getRestriction()
        {
            return "Must have atleast one Witches Power. Only available in a location with a human soul.";
        }

        public override double getComplexity()
        {
            return 50;
        }

        public override int getCompletionMenace()
        {
            return 20;
        }

        public override int getCompletionProfile()
        {
            return 10;
        }

        public override void complete(UA u)
        {
            witch.location.RemoveProperty<Pr_FallenHuman>();
            RitualComplete();
        }

        public override bool valid()
        {
            if (!base.valid()) return false;
            map.overmind.availableEnthrallments++;
            return witch.location.PropertyIs<Pr_FallenHuman>(a => a.charge > 0.0);
        }
    }
}