using Assets.Code;

namespace Witching.Rituals
{
    public class GeneratePower : Task
    {
        public Location target;

        public GeneratePower(Location loc)
        {
            target = loc;
        }

        public override string getShort()
        {
            return "Generating Power";
        }

        public override string getLong()
        {
            return getShort();
        }

        public override void turnTick(Unit acolyteUnit)
        {
            foreach (var unit in target.units)
            {
                if (unit is Witch witch && witch.task is Task_PerformChallenge task && task.challenge is Gathering)
                    return;
            }
            acolyteUnit.task = null;
        }

        public override bool isBusy()
        {
            return true;
        }
    }
}