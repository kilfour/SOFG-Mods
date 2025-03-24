using Assets.Code;

namespace Witching.Traits
{
    public class Evangelist : T_ChallengeBooster
    {
        public Evangelist() : base(Tags.RELIGION) { }

        public override string getTitle(Person p)
        {
            return "Preacher";
        }

        public override string getName()
        {
            return "Evangelist";
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