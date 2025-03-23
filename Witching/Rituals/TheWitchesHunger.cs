using System.Linq;
using Assets.Code;
using Witching.Traits;
using Witching.Rituals.Bolts;

namespace Witching.Rituals
{
    public class TheWitchesHunger : WitchesRitual
    {
        public class RitualUpdater : KnownRitual<TheWitchesHunger>
        {
            protected override bool CanBeCastOnHero(Person person)
            {
                return !person.traits.Any(a => a is T_TheHunger);
            }

            protected override bool CanBeCastOnRuler(Person person)
            {
                return !person.traits.Any(a => a is T_TheHunger);
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower, Person prey)
            {
                return new TheWitchesHunger(location, witchesPower, prey);
            }
        }

        protected override int RequiredCharges => 3;

        public TheWitchesHunger(Location location, WitchesPower witchesPowerTrait, Person prey)
            : base(location, witchesPowerTrait, prey) { }

        public override string getName()
        {
            return "Infect " + prey.getName();
        }

        public override string getDesc()
        {
            return "Spend three Hunger Power charges in order to inflict 'The Hunger' on " + prey.getName() + ", which causes them to periodically be compelled to feed on the civilians of the location they are in, increasing shadow there by " + (int)(100.0 * map.param.mg_theHungerLocationShadowGain) + "%, their own shadow by " + (int)(100.0 * map.param.mg_theHungerPersonalShadowGain) + " and their menace by " + map.param.mg_theHungerMenace + " (if they are a hero) or increases unrest (if they are a rule). The feeding's motivation is affected by their personal shadow and their preferences for cruelty and shadow.";
        }

        public override string getRestriction()
        {
            if (prey.rulerOf > -1)
            {
                return prey.getName() + "The location must be fully infiltrated. Can not be allready infected.";
            }
            return "Can not target the Chosen One or someone allready infected.";
        }

        public override string getCastFlavour()
        {
            return "The Hunger slowly changes its victim, over the course of months, as their humanity is sapped. What they previously valued, the people they previously cherished, are lost to them. Their previous desires slip away, to be replaced by a near animalistic desire to feed, a constant yearning for the sensation of blood flowing past their ever-sharpening teeth.";
        }

        public override double getComplexity()
        {
            return map.param.mg_theHungerComplexity;
        }

        public override int getCompletionMenace()
        {
            return map.param.mg_theHungerMenaceCaster;
        }

        public override int getCompletionProfile()
        {
            return map.param.mg_theHungerProfile;
        }

        public override void complete(UA _)
        {
            prey.receiveTrait(new T_TheHunger());
            RitualComplete();
        }
    }
}