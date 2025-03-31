using Assets.Code;
using UnityEngine;
using Common;
using TheBroken.Modifiers;
using System;
using System.Collections.Generic;
using TheBroken.Challenges;

namespace TheBroken
{

    public class Shard : Property
    {
        private const int threadingCooldownLength = 5;
        public int ThreadingCooldownRemaining;
        public List<Challenge> challenges = new List<Challenge>();

        public Shard(Location location)
            : base(location)
        {
            charge = 1;
            ThreadingCooldownRemaining = threadingCooldownLength;
            if (!location.HasSubSettlement<Sub_AncientRuins>())
                challenges.Add(new Ch_LayLowWilderness(location));
            challenges.Add(new TheFracture(location));
            challenges.Add(new LiturgyOfYield(location));
            challenges.Add(new PreachTheKeeping(location));
            challenges.Add(new Uprising(location));
            GrowTheShard();
        }

        public override string getName()
        {
            return "A Shard";
        }

        public override string getDesc()
        {
            if (charge <= 100)
                return "Not all breaks are loud. Some appear like root through stone.";
            if (charge <= 200)
                return "The stone forgets it was ever whole.";
            return "What was stone is now hollow, and it sings.";
        }

        public override Sprite getSprite(World world)
        {
            if (charge <= 100)
                return EventManager.getImg("the-broken.shrine-of-first-breaking.png");
            if (charge <= 200)
                return EventManager.getImg("the-broken.buried-mouth.png");
            return EventManager.getImg("the-broken.cult-temple.png");
        }

        public override standardProperties getPropType()
        {
            return standardProperties.OTHER;
        }

        public override List<Challenge> getChallenges()
        {
            return challenges;
        }

        public override void turnTick()
        {
            GrowTheShard();
            ThreadTheNeedle();
        }

        private void GrowTheShard()
        {
            if (charge >= 300) return;
            if (charge >= 200)
            {
                influences.Add(new ReasonMsg("Faith takes root in the fissure.", 3.0));
                return;
            }
            if (charge >= 100)
            {
                influences.Add(new ReasonMsg("One more breaks from the root.", 2.0));
                return;
            }
            influences.Add(new ReasonMsg("Another fracture spreads.", 1.0));
            if (charge >= 300) charge = 300;
        }

        private void ThreadTheNeedle()
        {
            if (charge < 50) return;
            if (location.IsFullyInfiltrated()) return;
            if (location.HasProperty<Threading>()) return;
            ThreadingCooldownRemaining--;
            if (ThreadingCooldownRemaining <= 0)
            {
                location.AddProperty(new Threading(location));
                ThreadingCooldownRemaining = threadingCooldownLength;
            }

        }
    }
}