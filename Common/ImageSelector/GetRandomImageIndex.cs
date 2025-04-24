using System.Linq;
using Assets.Code;

namespace Common.ImageSelector
{
    public interface IHaveMultipleImages
    {
        int ImageIndex { get; set; }
    }

    public class GetRandomImageIndex
    {
        public static int For<TAgent>(int imageCount, Map map) where TAgent : UAE, IHaveMultipleImages
        {
            var allreadyUsed = map.units.Where(a => a is TAgent).Select(a => (a as TAgent).ImageIndex);
            var all = Enumerable.Range(1, imageCount);
            var available = all.Where(a => !allreadyUsed.Contains(a)).ToList();
            if (available.Count == 0)
                available = all.ToList();
            return available[Eleven.random.Next(available.Count)];
        }
    }
}