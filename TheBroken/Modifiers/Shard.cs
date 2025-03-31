using Assets.Code;
using UnityEngine;
using Common;
using TheBroken.Modifiers;
using System;

namespace TheBroken
{

    public class Shard : Property
    {
        private const int cooldownLength = 5;
        public int CooldownRemaining;

        public Shard(Location loc)
            : base(loc) { charge = 1; CooldownRemaining = cooldownLength; }

        public override string getName()
        {
            return "A Shard";
        }

        public override string getDesc()
        {
            return "Not all breaks are loud. Some appear like root through stone.";
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

        public override void turnTick()
        {
            GrowTheShard();
            ThreadTheNeedle();
        }

        private void GrowTheShard()
        {
            if (charge >= 300) return;
            influences.Add(new ReasonMsg("Another fracture spreads.", 1.0));
            if (charge >= 300) charge = 300;
        }

        private void ThreadTheNeedle()
        {
            if (charge < 50) return;
            if (location.IsFullyInfiltrated()) return;
            if (location.HasProperty<Threading>()) return;
            CooldownRemaining--;
            if (CooldownRemaining <= 0)
            {
                location.AddProperty(new Threading(location));
                CooldownRemaining = cooldownLength;
            }

        }
    }
}