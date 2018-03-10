using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root._2GenerationOfCombinatorialObjects
{
    public class _2_4_1
    {
        public List<List<int>> Run(int n)
        {
            var result = new List<List<int>>();
            var arr = Enumerable.Repeat(1, n).ToList();
            result.Add(arr.ToList());

            var k = n - 1;

            while (arr[0] < n)
            {
                var s = k - 1;
                while (s > 0 && arr[s - 1] <= arr[s])
                {
                    s -= 1;
                }

                arr[s] = arr[s] + 1;

                var sum = 0;
                for (var i = s + 1; i <= k; i++)
                {
                    sum += arr[i];
                }

                for (var i = s + 1; i <= k; i++)
                {
                    arr[i] = 1;
                }

                k = s + sum - 1;

                arr[k + 1] = 0;

                result.Add(arr.Take(k + 1).ToList());
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void _2_4_1_Combine_ForLength3FromBinaryValues_Success()
            {
                var n = 4;
                var testable = new _2_4_1();
                var expected = new[] { "1111", "211", "22", "31", "4" };

                var result = testable.Run(n);

                Assert.That(result.Select(x => string.Join("", x)).ToArray(), Is.EquivalentTo(expected));
            }
        }
    }
}
