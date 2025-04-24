using Assets.Code;
using Common;
using Common.ImageSelector;
using ShapeShifter.Traits;
using UnityEngine;

namespace ShapeShifter
{
    public class ShapeShifter : UAE, IHaveMultipleImages
    {
        public int ImageIndex { get; set; }

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
            return "Shapeshifter";
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
    }
}


