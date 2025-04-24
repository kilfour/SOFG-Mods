using Assets.Code;
using UnityEngine;
using Common;
using System.Collections.Generic;
using TheBroken.Challenges;
using System.Linq;

namespace TheBroken.Modifiers
{
    [LegacyDoc(Order = "1.2.1", Caption = "Shard", Content =
@"Represents the cult's embedded presence in a society.  
Spawns a Broken (which looks for a new village to establish a new Shard). Prefers uninfiltrated, close by locations.  
If the First's Unique trait is Graven Flock instead of Graven Presence they follow the first around instead.  
Shard magnitude needs to be greater than 250.  
Consumes 200 charge.  
Any village containing a Shard gives your agent an opportunity to perform a Lay Low challenge.  ")]
    public class Shard : Property
    {
        private const int threadingCooldownLength = 5;
        public int ThreadingCooldownRemaining;

        private const int brokenSpawningCooldownLength = 5;
        public int BrokenSpawningCooldownRemaining;
        private const int maxNumberOfWanderingBroken = 5;

        public List<Challenge> challenges = new List<Challenge>();

        public Shard(Location location)
            : base(location)
        {
            charge = 50;
            ThreadingCooldownRemaining = threadingCooldownLength;
            BrokenSpawningCooldownRemaining = 0;
            if (!location.HasSubSettlement<Sub_AncientRuins>())
                challenges.Add(new Ch_LayLowWilderness(location));
            challenges.Add(new LiturgyOfYield(location));
            challenges.Add(new PreachTheKeeping(location));
            challenges.Add(new TheFracture(location));
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
            SpawnWanderingBroken();
        }

        private void GrowTheShard()
        {
            AddCharge("Another fracture spreads.", 1);
            if (charge >= 100)
                AddCharge("One more breaks from the root.", 1);
            if (charge >= 200)
                AddCharge("Faith takes root in the fissure.", 1);
            if (charge >= 300) charge = 300;
        }

        public void AddCharge(string reason, double chargeToAdd)
        {
            if (charge >= 300) return;
            influences.Add(new ReasonMsg(reason, chargeToAdd));
            if (charge >= 300) charge = 300;
        }

        private void ThreadTheNeedle()
        {
            if (location.IsFullyInfiltrated()) return;
            if (location.HasProperty<Threading>()) return;
            location.AddProperty(new Threading(location));
        }

        public void SpawnWanderingBroken()
        {
            if (charge < 250) return;
            BrokenSpawningCooldownRemaining--;
            if (BrokenSpawningCooldownRemaining > 0) return;
            BrokenSpawningCooldownRemaining = brokenSpawningCooldownLength;
            var currentNumberOfWanderingBroken =
                (from unit in map.units where unit is Broken select unit).Count();
            if (currentNumberOfWanderingBroken >= maxNumberOfWanderingBroken) return;


            var targetLocation = Broken.FindTargetLocation(location);
            if (targetLocation == null) return;

            Person p = new Person(map.soc_dark);
            var broken = new Broken(location, map.soc_dark, p);
            location.units.Add(broken);
            map.units.Add(broken);
            broken.targetLocation = targetLocation;
            broken.task = new Task_GoToLocation(targetLocation);

            charge -= 200;

        }
    }
}