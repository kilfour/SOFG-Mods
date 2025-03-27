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
            return "Gains three progress per turn when performing challenges relating to Religion.";
        }

        public override int[] getTags()
        {
            return new int[1] { Tags.RELIGION };
        }

        // public override void onAcquire(Person person)
        // {
        //     base.onAcquire(person);
        //     // var witch = person.unit as Witch;
        //     // witch.WitchesTitle = "Soothsayer";
        //     if (person.society is HolyOrder holyOrder)
        //         holyOrder.prophet = person.unit as UAE;
        // }
    }
}