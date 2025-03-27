// using System.Linq;
// using Assets.Code;
// using Witching.Traits;
// using Witching.Rituals.Bolts;
// using UnityEngine;
// using System;
// using System.Collections.Generic;

// namespace Witching.Rituals
// {
//     public class TheWitchesVampire : Ritual //: WitchesMobileRitual
//     {
//         public Witch Witch;
//         public Pr_FallenHuman Soul;

//         public TheWitchesVampire(Location location, Witch witch)
//             : base(location)
//         {
//             Witch = witch;

//         }

//         public override string getDesc()
//         {
//             return "Create a <b>Vampire</b> by exploiting a soul, raising them to undeath as an uncontrolled evil character who will attempt to spread <b>shadow</b>, and whose use of <b>Feed</b> will cause heroes and nobles to gain <b>The Hunger</b> as well";
//         }

//         public override string getRestriction()
//         {
//             return "Requires fifty Witches Power and a soul of a character who died under the influence of <b>The Hunger</b>, and a character with <b>Mastery of Death Magic</b> of " + map.param.mg_vampireDeathReq + " or more";
//         }

//         public override string getCastFlavour()
//         {
//             return "The Hunger never ceases, not even in death, and the curse's victim finds themselves driven to leave their tomb to feed on their former friends and family.";
//         }

//         public override double getProfile()
//         {
//             return 10.0;
//         }

//         public override double getMenace()
//         {
//             return 100.0;
//         }

//         public override challengeStat getChallengeType()
//         {
//             return challengeStat.LORE;
//         }

//         public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
//         {
//             msgs?.Add(new ReasonMsg("Stat: Lore", Math.Max(1, unit.getStatLore())));
//             return Math.Max(1, unit.getStatLore());
//         }

//         public override double getComplexity()
//         {
//             return 50.0;
//         }

//         public override int getCompletionMenace()
//         {
//             return 10;
//         }

//         public override int getCompletionProfile()
//         {
//             return 10;
//         }

//         public override bool validFor(UA unit)
//         {
//             if (unit.location.properties. is Task_PerformChallenge task && task.challenge is Gathering)
//                 return true;
//             var unrest = getStandardPropertyLevel(location, Property.standardProperties.UNREST);
//             return unrest > 50;
//         }

//         public override Sprite getSprite()
//         {
//             return map.world.iconStore.theHunger;
//         }

//         public override int isGoodTernary()
//         {
//             return -1;
//         }

//         public override void complete(UA u)
//         {
//             createVampire(map, Soul);
//         }

//         public static void createVampire(Map map, Pr_FallenHuman soul)
//         {
//             Person person = map.persons[soul.personIndex];
//             UAEN_Vampire uAEN_Vampire = new UAEN_Vampire(soul.location, map.soc_dark, person);
//             soul.location.units.Add(uAEN_Vampire);
//             map.units.Add(uAEN_Vampire);
//             person.isDead = false;
//             person.shadow = 1.0;
//             person.awareness = 1.0;
//             soul.location.properties.Remove(soul);
//             person.hasSoul = false;
//             while (uAEN_Vampire.getStatLore() < 4)
//             {
//                 person.stat_lore++;
//             }
//             while (uAEN_Vampire.getStatMight() < 4)
//             {
//                 person.stat_might++;
//             }
//         }

//         public override bool valid()
//         {
//             foreach (Trait trait in map.persons[Soul.personIndex].traits)
//             {
//                 if (trait is T_TheHunger)
//                 {
//                     return true;
//                 }
//             }
//             return false;
//         }

//         public override int[] buildPositiveTags()
//         {
//             return new int[2]
//             {
//             Tags.UNDEAD,
//             Tags.SHADOW
//             };
//         }

//         public override int[] buildNegativeTags()
//         {
//             return new int[0];
//         }
//     }
// }