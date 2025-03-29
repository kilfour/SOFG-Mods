using System.Linq;
using Assets.Code;
using UnityEngine;
using Witching.Rituals.Bolts;
using Witching.Rituals.Bolts.Nuts;
using Witching.Traits;

namespace Witching.Rituals
{
    public class WordsNotHerOwn : WitchesPoweredRitual
    {
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
            // ----------------------------------------------------
            //Being very careful ;-)
            if (unit.location == null) return false;
            if (unit.location.settlement == null) return false;
            if (unit.location.settlement.subs == null) return false;
            // --
            var temple =
                unit.location.settlement.subs
                    .Select(a => a as Sub_Temple)//Sub_HolyOrderCapital
                    .Where(a => a != null)
                    .FirstOrDefault();
            if (temple == null) return false;
            foreach (SocialGroup socialGroup in map.socialGroups)
            {
                if (socialGroup is HolyOrder order)
                    if (order.prophet == unit) return false;
            }
            return true;
        }

        public override void complete(UA unit)
        {
            unit.location.settlement.subs
                .Select(a => a as Sub_Temple)//Sub_HolyOrderCapital
                .Where(a => a != null)
                .First().order.prophet = unit;

            // ------------------------------------------------------------
            // // Maybe add witch to society, could conflict with gathering
            // --
            // var order =
            //     unit.location.settlement.subs
            //         .Select(a => a as Sub_Temple)//Sub_HolyOrderCapital
            //         .Where(a => a != null)
            //         .First().order;
            // order.prophet = unit;
            // unit.person.society = order;
            // order.people.Add(unit.person.index);
        }
    }
}