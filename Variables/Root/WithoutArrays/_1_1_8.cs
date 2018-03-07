using FsCheck;
using NUnit.Framework;

namespace WithoutArrays
{
    public class _1_1_8
    {
        public int Run(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            var result = n;
            var i = n - 1;
            while (i > 0)
            {
                result *= i;
                i--;
            }
            return result;
        }

        public int Rec(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            return n * Rec(n - 1);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void _1_1_8_Test()
            {
                var n = 9;
                var testable = new _1_1_8();

                var f = testable.Run(n);
                var fr = testable.Rec(n);

                Assert.That(f == fr && f == 362880);
            }

            [Test]
            public void _1_1_8_AnyInputValues_Success()
            {
                var testable = new _1_1_8();
                Prop.ForAll(
                    Arb.From<int>().Filter(x => x > 0 && x < 13),
                    n =>
                    {
                        var f = testable.Run(n);
                        var fr = testable.Rec(n);

                        Assert.That(f == fr);
                    }).QuickCheckThrowOnFailure();
            }
        }
    }
}
