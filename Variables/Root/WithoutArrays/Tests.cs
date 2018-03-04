using FsCheck;
using NUnit.Framework;
using System;

namespace WithoutArrays
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void _1_1_1_TwoVariablesAsInput_ReturnSwappedValues()
        {
            var a = 10;
            var b = 20;

            var (aNew, bNew) = new _1_1_1().Run(a, b);

            Assert.That(a == bNew && b == aNew);
        }
       
        [Test]
        public void _1_1_1AfterDoubleSwap_TwoVariablesAsInput_ValuesRemainsAsTheOrigin()
        {
            var obj = new _1_1_1();
            Prop.ForAll<int, int>((a, b) => {
                var (aNew, bNew) = obj.Run(a, b);
                var (aAsOrigin, bAsOrigin) = obj.Run(aNew, bNew);
                Assert.That(aAsOrigin == a && bAsOrigin == b);
            }).QuickCheckThrowOnFailure();
        }
    }
}
