using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutArrays
{
    public class _1_1_2
    {
        public (int aNew, int bNew) Run(int a, int b)
        {
            a = a + b;
            b = a - b;
            a = a - b;

            return (a, b);
        }
    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void MakeDoubleSwap_TwoVariablesAsInput_ReturnValuesAsOrigin()
        {
            var testable = new _1_1_2();
            Prop.ForAll<int, int>((a, b) =>
            {
                var (aNew, bNew) = testable.Run(a, b);
                var (aAsOrigin, bAsOrigin) = testable.Run(aNew, bNew);

                Assert.That(aNew == b && bNew == a);
                Assert.That(aAsOrigin == a && bAsOrigin == b);
            }).QuickCheckThrowOnFailure();
        }
    }
}
