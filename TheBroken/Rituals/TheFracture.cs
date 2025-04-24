// using System;
// using System.Collections.Generic;
// using Assets.Code;
// using Common;
// using TheBroken.Modifiers;
// using UnityEngine;

// namespace TheBroken.Rituals
// {
//     public class TheFracture : Ritual
//     {
//         public FirstAmongTheBroken theFirst;
//         public TheFracture(FirstAmongTheBroken theFirst, Location location)
//             : base(location)
//         {
//             this.theFirst = theFirst;
//         }

//         public override string getName()
//         {
//             return "The Fracture";
//         }

//         public override string getDesc()
//         {
//             return "Increases the magnitude of the Shard. Switches the broken from settlers to followers when performed anywhere else.";
//         }

//         public override string getRestriction()
//         {
//             return "Can be performed anywhere, anytime.";
//         }

//         public override string getCastFlavour()
//         {
//             return "A village doesn't fall in a day. First comes the fracture. Then the rot. Then the worship.";
//         }

//         public override Sprite getSprite()
//         {
//             return EventManager.getImg("the-broken.preach-the-fracture.png");
//         }

//         public override int isGoodTernary()
//         {
//             return Constants.OnlyPerformedByDarkEmpire;
//         }

//         public override challengeStat getChallengeType()
//         {
//             return challengeStat.INTRIGUE;
//         }

//         public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
//         {
//             msgs?.Add(new ReasonMsg("Stat: Intrigue", unit.getStatIntrigue()));
//             return Math.Max(1, unit.getStatIntrigue());
//         }

//         public override double getComplexity()
//         {
//             return 20;
//         }

//         public override int getCompletionMenace()
//         {
//             return 0;
//         }

//         public override int getCompletionProfile()
//         {
//             return 3;
//         }
//         public override bool validFor(UA unit)
//         {
//             return true;
//         }

//         public override void complete(UA unit)
//         {
//             var shard = unit.location.GetPropertyOrNull<Shard>();
//             if (shard != null)
//             {
//                 shard.charge += 50;
//                 return;
//             }
//             theFirst.LeadingFlock = !theFirst.LeadingFlock;
//         }
//     }
// }