using Assets.Code;
using Common;

namespace Witching.Traits
{
    [LegacyDoc(Order = "1.2.2", Caption = "Soothsayer", Content =
@"The witch gains three extra progress per turn when performing challenges relating to Religion.")]
    public class Soothsayer : T_ChallengeBooster
    {
        // int in ctor needed to avoid NRE during deserializing
        // empty ctor gets called when loading and crashes further up the chain,
        // because Tags.names is possibly not yet initialized
        public Soothsayer(int irrelevant) : base(Tags.RELIGION) { }

        public override string getName()
        {
            return "Soothsayer";
        }

        public override string getDesc()
        {
            return "Gains three progress per turn when performing challenges relating to Religion.";
        }

        public override int[] getTags()
        {
            return new int[1] { Tags.RELIGION };
        }
    }
}