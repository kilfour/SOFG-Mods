using Xunit;
using Witching.Bolts;

namespace Witching.Tests
{
    public class Buggies
    {
        [Fact]
        public void WhatsWrongWithThisSimpleThing()
        {
            var result = MyMath.Sum(new[] { 1.0, 2.0, 2.0 });  // Replace with your actual class
            Assert.Equal(5, result);
        }
    }
}




