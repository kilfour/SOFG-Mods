using Assets.Code;
using Witching.Rituals;

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
            return "Any newly hired Soothsayer immediately becomes the prophet of their coven and gains three progress per turn when performing challenges relating to Religion.";
        }

        public override int[] getTags()
        {
            return new int[1] { Tags.RELIGION };
        }

        public override void onAcquire(Person person)
        {
            base.onAcquire(person);
            var witch = person.unit as Witch;
            witch.WitchesTitle = "Soothsayer";
            if (person.society is HolyOrder holyOrder)
                holyOrder.prophet = person.unit as UAE;
            witch.rituals.Add(new Gathering(witch.location));
        }
    }
}