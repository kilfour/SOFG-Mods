using Assets.Code;

namespace Witching.Traits
{
    public class Prophet : Trait
    {
        public override string getName()
        {
            return "Dark Prophet";
        }

        public override string getDesc()
        {
            return "The Witch becomes the prophet of the coven where it first appears and turns it towards the dark.";
        }

        public override void onAcquire(Person person)
        {
            if (person.society is HolyOrder holyOrder)
            {
                if (holyOrder.tenet_alignment.status > -3)
                    holyOrder.tenet_alignment.status--;
                holyOrder.prophet = person.unit as UAE;
            }
        }
    }
}