using System.Collections.Generic;
using Assets.Code;
using Assets.Code.Modding;

namespace Witching
{
    public class ModCore : ModKernel
    {
        public override void beforeMapGen(Map map)
        {
            map.overmind.agentsGeneric.Add(new WitchAbstract(map));
        }
    }
}