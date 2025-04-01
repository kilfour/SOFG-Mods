using Xunit;
using Witching;
using Assets.Code;
using System.Security.Cryptography.X509Certificates;
using Common.M;
using System.Collections.Generic;
namespace Witching.Tests
{
    public class Spike
    {
        private class Root
        {
            public int[] List = new[] { 1, 2, 3, 4 };
            public int? Simple = 42;
        }
        [Fact]
        public void Maybe()
        {
            var root = new Root();
            var result =
                from r in root
                from s in r.Simple
                let t = s + 1
                select t;
            Assert.True(result.HasValue);
            Assert.IsType<int>(result.Value);

            // Most verbose form, most strictly correct
            var m1 = 5
                .ToMaybe()
                .Select(x => x + 10);

            // prettier but result will be of type int
            var m2 = from x in 5
                     select x + 10;

            // if you use nullable structs, it's pretty and result will be HasValue=false if anything is null
            var m3 = from x in (int?)5
                     from y in (int?)10
                     select x + y;

            // identical to above except the long form, with a null
            int? five = 5;
            int? ten = null;
            var m4 = five.Select(x => ten.SelectMany(y => x + y));

            // Pretty form for reference types, check for null on result.
            var m5 = from x in "testing"
                     from y in (string)null
                     select x + y;

            // Maybe form of above, check for HasValue
            var m6 = from x in "testing".ToMaybe()
                     from y in ((string)null).ToMaybe()
                     select x + y;

            // Longer select chains
            var m7 = from x in new Example()
                     from y in x.Inner
                     from z in y.Inner
                     select z.Value;

            var m8 = from x in new Example { Inner = new Example { Inner = new Example { Value = "success!" } } }
                     from y in x.Inner
                     from z in y.Inner
                     select z.Value;

            // Console.WriteLine("m1: {0}->{1}", m1.HasValue, m1.Value);
            // Console.WriteLine("m2: True->{0} (non-nullable)", m2);
            // Console.WriteLine("m3: {0}->{1}", m3.HasValue, m3.Value);
            // Console.WriteLine("m4: {0}", m4.HasValue);
            // Console.WriteLine("m5: {0}", m5 != null);
            // Console.WriteLine("m6: {0}", m6.HasValue);
            // Console.WriteLine("m7: {0}", m7 != null);
            // Console.WriteLine("m8: {0}->{1}", m8 != null, m8);
        }
    }

    internal class Example
    {
        public Example Inner { get; set; }
        public string Value { get; internal set; }
    }
}
