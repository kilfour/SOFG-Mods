using Assets.Code;
using Common;
using Common.ImageSelector;
using ShapeShifter.Traits;
using UnityEngine;

namespace ShapeShifter
{
    [LegacyDoc(Order = "1", Caption = "Shapeshifter", Content =
@"This contains one generic agent: 'A Shapeshifter'.  
Mimics a living hero. While in disguise, uses that hero's stats. 
Profile and Menace from actions are transferred to the real hero. Can revert at will.  
Initially you can only have one of these, but there are ways to raise this limit.  

Upon death adds 25 shadow to it's location and 10 shadow to the neighbouring locations.")]
    public class ShapeShifter : UAE, IHaveMultipleImages
    {
        public int ImageIndex { get; set; }
        public static int NumberAllowed = 1;
        public static int CurrentNumberOfShapeShifters = 0;

        public ShapeShifter(Location location, int imageIndex)
            : base(location, location.map.soc_dark)
        {
            ImageIndex = imageIndex;
            person.stat_might = Constants.Might;
            person.stat_lore = Constants.Lore;
            person.stat_intrigue = Constants.Intrigue;
            person.stat_command = Constants.Command;
            person.isMale = false;
            person.age = 21;
            person.hasSoul = false;
            person.gold = Constants.Gold;
            person.receiveTrait(new Mimic(this));
        }


        public override string getName()
        {
            if (person.overrideName != null && person.overrideName.Length != 0)
            {
                return person.overrideName;
            }
            return "Shape " + person.firstName;
        }

        public override bool isCommandable()
        {
            return true;
        }

        public override bool hasStartingTraits()
        {
            return false;
        }

        public override Sprite getPortraitForeground()
        {
            var mimicTrait = person.GetTrait<Mimic>();
            if (mimicTrait.victim == null)
                return EventManager.getImg("shape-shifter.shape-shifter-" + ImageIndex.ToString() + ".png");
            return mimicTrait.victim.getPortrait();
        }

        public override void die(Map map, string v, Person killer = null)
        {
            CurrentNumberOfShapeShifters--;
            location.AddShadow(0.25);
            location.getNeighbours().ForEach(a => a.AddShadow(0.1));
            base.die(map, v, killer);
        }
    }
}


