using Assets.Code;
using UnityEngine;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;
using Witching.Traits;
using Common;
using System.Linq;

namespace Witching.Rituals
{
    public class WordsNotHerOwn : WitchesPoweredRitual
    {
        public class RitualUpdater : LocationRitualUpdater<WordsNotHerOwn>
        {
            protected override bool CanBeCastOnLocation(Location location)
            {
                return location.HasTemple();
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower)
            {
                return new WordsNotHerOwn(location, witchesPower);
            }
        }

        public WordsNotHerOwn(Location location, WitchesPower witchesPower)
            : base(location, witchesPower, 10) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("witching.prophet.png");
        }

        public override string getName()
        {
            return "Words Not Her Own";
        }

        public override string getDesc()
        {
            return "Petition the order, in order to become it's vessel, it's prophet.";
        }

        public override string getCastFlavour()
        {
            return "Her lips move, but the voice is not hers. The order speaks through her now, ancient, many, and patient. What truths she tells may bind, may bless, or may break.";
        }

        public override string getRestriction()
        {
            return "Requires 10 Witches Power. The ritual needs to be started at a coven or temple and the witch can not already be a prophet.";
        }

        public override double getComplexity()
        {
            return 30;
        }

        public override int getCompletionMenace()
        {
            return 0;
        }

        public override int getCompletionProfile()
        {
            return 10;
        }

        public override bool validFor(UA unit)
        {
            return !unit.IsAProphet();
        }

        public override void complete(UA unit)
        {
            unit.location.GetHolyOrderOrNull().prophet = unit;
            RitualComplete();
        }
    }
}