using Xunit;
using TheBroken.Challenges;
using Assets.Code;
using System.Collections.Generic;

namespace Witching.Tests
{
    public class LiturgyOfYieldTests
    {
        [Fact]
        public void ChargeIs100()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(100, 4);
            Assert.Equal(2, result);
        }

        [Fact]
        public void ChargeIs150()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(150, 4);
            Assert.Equal(3, result);
        }

        [Fact]
        public void ChargeIs200()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(200, 4);
            Assert.Equal(4, result);
        }

        [Fact]
        public void ChargeIs250()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(250, 4);
            Assert.Equal(5, result);
        }

        [Fact]
        public void ChargeIs300()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(300, 4);
            Assert.Equal(6, result);
        }

        [Fact]
        public void ChargeIs10()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(10, 4);
            Assert.Equal(0.2, result);
        }

        [Fact]
        public void ChargeIs150Intrigue6()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(150, 6);
            Assert.Equal(4.5, result);
        }

        [Fact]
        public void Whatever()
        {
            var result = LiturgyOfYield.ApplyShardMagnitudeToStat(76, 4);
            Assert.Equal(1.52, result);
        }
    }
}



