using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutArrays
{
    public class _1_1_3
    {
        public int Run(int a, int n)
        {
            var result = 1;
            for (var i = 0; i < n; i++)
            {
                result *= a;
            }
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test()
            {
                var n = 2;
                var a = 4;
                var testable = new _1_1_3();

                var result = testable.Run(a, n);

                Assert.That(result == 16 && result == Math.Pow(a, n));
            }

            [Test]
            public void Pow_AnyInputValues_Success()
            {
                var testable = new _1_1_3();
                Prop.ForAll<int, int>(
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
