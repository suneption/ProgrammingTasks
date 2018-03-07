using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutArrays
{
    public class _1_1_9
    {
        public int Run(int n)
        {
            var arr = Enumerable.Range(0, n + 1).Select(x => 0).ToList();

            var i = 0;
            while (i <= n)
            {
                if (i <= 1)
                {
                    arr[i] = i;
                }
                else
                {
                    arr[i] = arr[i - 1] + arr[i - 2];
                }

                i++;
            }

            return arr[n];
        }

        //public int Rec(int n, List<int?> arr)
        //{
        //    if (n == 0 || n == 1)
        //    {
        //        return n;
        //    }

        //    var fN_1 = arr[n - 1] ?? Rec(n - 1, arr);
        //    arr[n - 1] = fN_1;
        //    var fN_2 = arr[n - 2] ?? Rec(n - 2, arr);
        //    arr[n - 2] = fN_2;

        //    return fN_1 + fN_2;
        //}

        public int Rec1(int n, int i, int fn_1, int fn_2)
        {
            int fn = 0;
            if (i <= 1)
            {
                fn = i;
            }
            else
            {
                fn = fn_1 + fn_2;
            }

            if (i == n)
            {
                return fn;
            }

            return Rec1(n, i + 1, fn, fn_1);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void _1_1_9_Test()
            {
                var n = 9;
                var testable = new _1_1_9();

                var arr = Enumerable.Range(0, n + 1).Select(x => (int?)null).ToList();

                var f = testable.Run(n);
                //var fr = testable.Rec(n, arr);
                var fr = testable.Rec1(n, 0, 0, 0);

                Assert.That(f == fr && f == 34);
            }

            [Test]
            public void _1_1_9_AnyInputValues_Success()
            {
                var testable = new _1_1_9();
                Prop.ForAll(
                    Arb.From<int>().Filter(x => x > 0 && x < Math.Pow(10, 5)),
                    n =>
                    {
                        var arr = Enumerable.Range(0, n + 1).Select(x => (int?)null).ToList();
                        var f = testable.Run(n);
                        //var fr = testable.Rec(n, arr);
                        var fr = testable.Rec1(n, 0, 0, 0);

                        Assert.That(f == fr);
                    }).QuickCheckThrowOnFailure();
            }
        }
    }
}
