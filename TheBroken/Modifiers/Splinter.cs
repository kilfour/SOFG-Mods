using Assets.Code;
using UnityEngine;
using System.Collections.Generic;
using TheBroken.Challenges;
using System.Linq;
using Common;

namespace TheBroken.Modifiers
{

    public class Splinter : Property
    {
        public List<Challenge> challenges = new List<Challenge>();

        public Splinter(Location location)
            : base(location)
        {
            charge = 100;
        }

        public override string getName()
        {
            return "A Splinter";
        }

        public override string getDesc()
        {
            return "It does not preach. It does not demand. It only waits, just beneath the surface. And one by one, they begin to itch.";
        }

        public override Sprite getSprite(World world)
        {
            return EventManager.getImg("the-broken.splinter.png");
        }

        public override standardProperties getPropType()
        {
            return standardProperties.OTHER;
        }

        public override void turnTick()
        {
            var unrest = (from unit in location.units where unit is Broken select unit).Count();
            Because.Of("Splinter").Add(unrest).Unrest(location);
        }
    }
}