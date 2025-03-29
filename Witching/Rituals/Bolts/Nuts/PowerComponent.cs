using Witching.Traits;

namespace Witching.Rituals.Bolts.Nuts
{
    public class PowerComponent
    {
        public WitchesPower Power;
        public int RequiredCharges;

        public PowerComponent(WitchesPower power, int requiredCharges)
        {
            Power = power;
            RequiredCharges = requiredCharges;
        }

        public bool HasEnoughCharges()
        {
            return Power.Charges >= RequiredCharges;
        }

        public bool NotEnoughCharges()
        {
            return !HasEnoughCharges();
        }

        public void ConsumeCharges()
        {
            Power.Charges -= RequiredCharges;
        }

        public int GetCharges()
        {
            return Power.Charges;
        }

        public void AddCharge(int charge)
        {
            Power.Charges += charge;
        }

        public void DrainAllCharges()
        {
            Power.Charges = 0;
        }
    }
}