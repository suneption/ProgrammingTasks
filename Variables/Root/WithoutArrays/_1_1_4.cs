using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutArrays
{
    public class _1_1_4
    {
        public int Run(int a, int n)
        {
            var result = 1;
            var c = a;
            var i = n;
            while (i >= 1)
            {
                if (i % 2 == 0)
                {
                    i /= 2;
                    c *= c;
                }
                else
                {
                    i -= 1;
                    result *= c;
                }
            }
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void _1_1_4_Test()
            {
                var n = 3;
                var a = 2;
                var testable = new _1_1_4();

                var result = testable.Run(a, n);

                Assert.That(result == 8 && result == Math.Pow(a, n));
            }

            [Test]
            public void _1_1_4Pow_AnyInputValues_Success()
            {
                var testable = new _1_1_4();
                Prop.ForAll(
                    Arb.From<int>().Filter(x => x > 0 && x < 10),
                    Arb.From<int>().Filter(x => x > 0 && x < 10),
                    (a, n) =>
                    {
                        var result = testable.Run(a, n);

                        Assert.That(result == Math.Pow(a, n));
                    }).QuickCheckThrowOnFailure();
            }
        }
    }
}
