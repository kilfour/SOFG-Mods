using System;
using System.Linq;
using Assets.Code;
using Common;
using UnityEngine;

namespace TheBroken.Modifiers
{
    public class Threading : Property
    {
        public Threading(Location loc)
            : base(loc) { charge = 1; }

        public override string getName()
        {
            return "Threading";
        }

        public override string getDesc()
        {
            return "One needle. One thread. One village at a time. At full power when Shard magnitude is greater than 150.";
        }

        public override Sprite getSprite(World world)
        {
            return EventManager.getImg("the-broken.threading.png");
        }

        public override standardProperties getPropType()
        {
            return standardProperties.OTHER;
        }

        public override void turnTick()
        {
            if (location.IsFullyInfiltrated())
            {
                charge = 0;
                return;
            }
            var shard = location.GetPropertyOrNull<Shard>();
            var chargeToAdd = Math.Max(10, shard.charge / 30);
            influences.Add(new ReasonMsg("The needle moves.", chargeToAdd));
            if (charge >= 100.0)
            {
                var subSettlement =
                    location.settlement.subs
                        .FirstOrDefault(a => a.canBeInfiltrated() && !a.infiltrated);
                if (subSettlement != null) subSettlement.infiltrated = true;
                charge = 0;
            }
        }
    }
}