using System;
using System.Linq;
using Assets.Code;
using Common;
using UnityEngine;

namespace TheBroken.Modifiers
{
    [LegacyDoc(Order = "1.2.3", Caption = "Threading", Content =
@"This modifier appears once in a while if there are uninfiltrated POI in a settlement with a Shard.    
Upon completion a POI is infiltrated.")]
    public class Threading : Property
    {
        public Threading(Location loc)
            : base(loc) { charge = 10; }

        public override string getName()
        {
            return "Threading";
        }

        public override string getDesc()
        {
            return "One needle. One thread. One village at a time.";
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
            var chargeToAdd = Math.Min(10, shard.charge / 30);
            influences.Add(new ReasonMsg("The needle moves.", chargeToAdd));
            if (charge >= 100.0)
            {
                var subSettlement =
                    location.settlement.subs
                        .FirstOrDefault(a => a.canBeInfiltrated() && !a.infiltrated);
                if (subSettlement != null) subSettlement.infiltrated = true;
                //charge = 0;
                location.RemoveProperty<Threading>();
                map.addUnifiedMessage(location, null, "Stitched into the Whole", "They welcomed the silence. The Shard does not knock twice.", "The Threading Concludes", force: true);
            }
        }
    }
}