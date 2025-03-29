using Assets.Code;
using UnityEngine;

namespace Witching.Rituals.Bolts.Nuts
{
    public class PreyComponent
    {
        public Person Person;

        public PreyComponent(Person person)
        {
            Person = person;
        }

        public Sprite GetSprite()
        {
            if (Person.unit is Witch witch)
                return witch.getPortraitForeground();
            return Person.getPortrait();
        }
    }
}