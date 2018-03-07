using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutArrays
{
    public class _1_1_7
    {
        public (int, int) Run(int a, int d)
        {
            var r = 0;
            var q = 0;

            do
            {
                q++;
                r = a - q * d;
            } while (r >= d);

            return (q, r);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void _1_1_7_Test()
            {
                var a = 9;
                var b = 2;
                var testable = new _1_1_7();

                var (q, r) = testable.Run(a, b);

                Assert.That(q == a / b && r == a % b);
            }

            [Test]
            public void _1_1_7_AnyInputValues_Success()
            {
                var testable = new _1_1_7();
                Prop.ForAll(
                    Arb.From<int>().Filter(x => x > 0 && x < Math.Pow(10, 6)),
                    Arb.From<int>().Filter(x => x > 0 && x < Math.Pow(10, 6)),
                    (a, d) =>
                    {
                        if (d > a)
                        {
                            d = a;
                        }

                        var (q, r) = testable.Run(a, d);

                        Assert.That(q == a / d && r == a % d);
                    }).QuickCheckThrowOnFailure();
            }
        }
    }
}
