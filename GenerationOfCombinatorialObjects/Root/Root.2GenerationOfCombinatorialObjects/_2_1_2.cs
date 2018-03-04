using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root._2GenerationOfCombinatorialObjects
{
    public class _2_1_2
    {
        public List<int[]> Run(int k, int n)
        {
            var initial = Enumerable.Range(0, k).Select(x => 0).ToArray();
            var last = Enumerable.Range(0, k).Select(x => n).ToArray();

            var result = new List<int[]>();
            result.Add(initial);

            var shouldContinue = true;
            do
            {
                var copy = initial.ToArray();
                var currentIndex = k - 1;
                for (; currentIndex >= 0; currentIndex--)
                {
                    if (copy[currentIndex] < n)
                    {
                        break;
                    }
                }

                if (currentIndex == -1)
                {
                    shouldContinue = false;
                }
                else
                {
                    copy[currentIndex] = copy[currentIndex] + 1;
                    for (var i = currentIndex + 1; i < k; i++)
                    {
                        copy[i] = 0;
                    }
                    result.Add(copy);
                    initial = copy;
                }
            } while (shouldContinue);

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Combine_ForLength3FromBinaryValues_Success()
            {
                var k = 3;
                var n = 1; //0, 1
                var testable = new _2_1_2();
                var expected = new[] { "000", "001", "010", "011", "100", "101", "110", "111" };

                var result = testable.Run(k, n);

                Assert.That(result.Select(x => string.Join("", x)).ToArray(), Is.EquivalentTo(expected));
            }

            [Test]
            public void PropertyCombine_AnyInputValues_DifferenceShouldBeConstantForAllValues()
            {
                var testable = new _2_1_2();
                Prop.ForAll<int, int>(Arb.From<int>().Filter(x => x > 0 && x < 5), Arb.From<int>().Filter(x => x > 0 && x < 10),
                    (k, n) =>
                    {
                        var init = Enumerable.Range(0, k).Select(x => 0).ToArray();
                        var last = Enumerable.Range(0, k).Select(x => n).ToArray();

                        var result = testable.Run(k, n);

                        Assert.That(result.First().SequenceEqual(init));
                        Assert.That(result.Last().SequenceEqual(last));
                    }).QuickCheckThrowOnFailure();
            }
        }
    }
}
