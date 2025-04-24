using Assets.Code;
using Common;
using UnityEngine;

namespace ShapeShifter.Rituals
{
    [LegacyDoc(Order = "1.2.3", Caption = "Shadow Tendrils", Content =
@"Only available when the Shapeshifter is in original form. 
A continuos ritual that adds 2 shadow per turn to a location without any profile or menace gain.")]
    public class ShadowTendrils : Ritual
    {
        public ShadowTendrils(Location location)
            : base(location) { }

        public override Sprite getSprite()
        {
            return EventManager.getImg("shape-shifter.shadow-tendrils.png");
        }

        public override int isGoodTernary()
        {
            return Constants.OnlyPerformedByDarkEmpire;
        }

        public override string getName()
        {
            return "Shadow Tendrils.";
        }

        public override string getDesc()
        {
            return "Perform a ritual that sends shadow tendrils crawling through the region. Increases Shadow.";
        }

        public override string getCastFlavour()
        {
            return "A single touch is all it needs. From the wound of a map, the dark seeps outward, slow, patient, and eternal.";
        }

        public override double getProfile()
        {
            return 0;
        }

        public override double getMenace()
        {
            return 0;
        }

        public override bool isIndefinite()
        {
            return true;
        }

        public override bool ignoreInterruptionWarning()
        {
            return true;
        }

        public override void turnTick(UA unit)
        {
            base.turnTick(unit);
            unit.midchallengeTimer = 0;
            unit.location.AddShadow(0.02);
        }
    }
}