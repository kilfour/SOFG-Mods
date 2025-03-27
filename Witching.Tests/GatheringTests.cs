using Xunit;
using System;

namespace Witching.Tests
{
    public class GatheringTests
    {
        public class UnrestCalculation
        {
            public double PowerToAdd { get; }
            public double UnrestToRemove { get; }
            public bool Continue { get; }
            public UnrestCalculation(double unrest, double power)
            {
                var suspicion = Math.Max(0, power + 1 - unrest);
                PowerToAdd = Math.Round(power - suspicion);
                UnrestToRemove = PowerToAdd + 1;
                Continue = Math.Round(unrest) > UnrestToRemove;
            }
        }

        [Fact]
        public void UnrestBiggerThanPowerPlusOne()
        {
            var result = new UnrestCalculation(12, 5);
            Assert.Equal(5, result.PowerToAdd);
            Assert.Equal(6, result.UnrestToRemove);
            Assert.True(result.Continue);
        }

        [Fact]
        public void NoIntUnrestBiggerThanPowerPlusOne()
        {
            var result = new UnrestCalculation(12.65, 5);
            Assert.Equal(5, result.PowerToAdd);
            Assert.Equal(6, result.UnrestToRemove);
            Assert.True(result.Continue);
        }

        [Fact]
        public void UnrestEqualsPowerPlusOne()
        {
            var result = new UnrestCalculation(6, 5);
            Assert.Equal(5, result.PowerToAdd);
            Assert.Equal(6, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void UnrestEqualsPower()
        {
            var result = new UnrestCalculation(5, 5);
            Assert.Equal(4, result.PowerToAdd);
            Assert.Equal(5, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void UnrestSmallerThanPowerPlusOne()
        {
            var result = new UnrestCalculation(3, 5);
            Assert.Equal(2, result.PowerToAdd);
            Assert.Equal(3, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void NoIntUnrestSmallerThanPowerPlusOne()
        {
            var result = new UnrestCalculation(3.1, 5);
            Assert.Equal(2, result.PowerToAdd);
            Assert.Equal(3, result.UnrestToRemove);
            Assert.False(result.Continue);
        }

        [Fact]
        public void BigUnrest()
        {
            var result = new UnrestCalculation(30, 0);
            Assert.Equal(0, result.PowerToAdd);
            Assert.Equal(1, result.UnrestToRemove);
            Assert.True(result.Continue);
        }
    }
}



