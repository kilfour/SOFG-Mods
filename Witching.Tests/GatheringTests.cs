using Xunit;
using Witching.Rituals;

namespace Witching.Tests
{
    public class GatheringTests
    {
        [Fact]
        public void UnrestBiggerThanPowerPlusOne()
        {
            var result = new Gathering.UnrestCalculation(12, 5);
            Assert.Equal(5, result.PowerToAdd);
            Assert.Equal(6, result.UnrestToRemove);
            Assert.True(result.Continue);
        }

        [Fact]
        public void NoIntUnrestBiggerThanPowerPlusOne()
        {
            var result = new Gathering.UnrestCalculation(12.65, 5);
            Assert.Equal(5, result.PowerToAdd);
            Assert.Equal(6, result.UnrestToRemove);
            Assert.True(result.Continue);
        }

        [Fact]
        public void UnrestEqualsPowerPlusOne()
        {
            var result = new Gathering.UnrestCalculation(6, 5);
            Assert.Equal(5, result.PowerToAdd);
            Assert.Equal(6, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void UnrestEqualsPower()
        {
            var result = new Gathering.UnrestCalculation(5, 5);
            Assert.Equal(4, result.PowerToAdd);
            Assert.Equal(5, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void UnrestSmallerThanPowerPlusOne()
        {
            var result = new Gathering.UnrestCalculation(3, 5);
            Assert.Equal(2, result.PowerToAdd);
            Assert.Equal(3, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void NoIntUnrestSmallerThanPowerPlusOne()
        {
            var result = new Gathering.UnrestCalculation(3.1, 5);
            Assert.Equal(2, result.PowerToAdd);
            Assert.Equal(3, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void BigUnrest()
        {
            var result = new Gathering.UnrestCalculation(30, 0);
            Assert.Equal(0, result.PowerToAdd);
            Assert.Equal(1, result.UnrestToRemove);
            Assert.True(result.Continue);
        }
    }
}



