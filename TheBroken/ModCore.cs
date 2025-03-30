using Assets.Code;
using Assets.Code.Modding;

namespace TheBroken
{
    public class ModCore : ModKernel
    {
        public override void beforeMapGen(Map map)
        {
            map.overmind.agentsUnique.Add(new FirstAmongTheBrokenAbstract(map));
        }
    }
}