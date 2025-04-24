using System.Collections.Generic;
using System.Linq;
using Assets.Code;
using Common;
using UnityEngine;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;
using Witching.Traits;

namespace Witching.Rituals
{
    public class TheWitchesPromise : WitchesPoweredRitual
    {
        public Witch witch;

        public class RitualUpdater : LocationRitualUpdater<TheWitchesPromise>
        {
            public Witch witch;

            public RitualUpdater(Witch witch)
            {
                this.witch = witch;
            }

            protected override bool CanBeCastOnLocation(Location location)
            {
                return FindSouls(location).Any();
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower)
            {
                return new TheWitchesPromise(witch, location, witchesPower);
            }
        }

        public TheWitchesPromise(Witch witch, Location location, WitchesPower witchesPower)
            : base(location, witchesPower, 100) { this.witch = witch; }

        public override Sprite getSprite()
        {
            return map.world.iconStore.theHunger;
        }

        public override string getName()
        {
            return "Witches: Promised Return";
        }

        public override string getDesc()
        {
            return "Removes a <b>Human Soul</b>, returning the dead to life as a Vampire";
        }

        public override string getRestriction()
        {
            return "Needs a soul infected with the Hunger.";
        }
        public override double getComplexity()
        {
            return 50;
        }

        public override int getCompletionMenace()
        {
            return 10;
        }

        public override int getCompletionProfile()
        {
            return 10;
        }

        public override bool valid()
        {
            if (!base.valid()) return false;
            return FindSouls(witch.location).Any();
        }

        public override void complete(UA u)
        {
            var soul = FindSouls(witch.location).First();
            Mg_Vampire.createVampire(map, soul);
            RitualComplete();
        }

        private static IEnumerable<Pr_FallenHuman> FindSouls(Location location) =>
            from soul in location.GetAllPropertiesOf<Pr_FallenHuman>()
            where location.map.persons[soul.personIndex].HasTrait<T_TheHunger>()
            where soul.charge > 0
            select soul;
    }
}
