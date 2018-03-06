using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutArrays
{
    public class _1_1_5
    {
        public int Run(int a, int b)
        {
            var result = Enumerable.Range(0, b).Aggregate(0, (acc, curr) => a + acc);
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void _1_1_5_Test()
            {
                var b = 5;
                var a = 11;
                var testable = new _1_1_5();

                var result = testable.Run(a, b);

                Assert.That(result == a * b);
            }

            [Test]
            public void _1_1_5Product_AnyInputValues_Success()
            {
                var testable = new _1_1_5();
                Prop.ForAll<int, int>(
                    Arb.From<int>().Filter(x => x > 0 && x < Math.Pow(10, 6)),
                    Arb.From<int>().Filter(x => x > 0 && x < Math.Pow(10, 3)),
                    (a, b) =>
                    {
                        var result = testable.Run(a, b);

                        Assert.That(result == a * b);
                    }).QuickCheckThrowOnFailure();
            }
        }
    }
}
