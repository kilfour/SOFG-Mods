using Assets.Code;

namespace Witching.Traits
{
    public class Soothsayer : T_ChallengeBooster
    {
        public Soothsayer() : base(Tags.RELIGION) { }

        public override string getName()
        {
            return "Soothsayer";
        }

        public override string getDesc()
        {
            return "Gain 3 progress per turn when performing challenges relating to Religion.";
        }

        public override int[] getTags()
        {
            return new int[1] { Tags.RELIGION };
        }
    }
}