using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root._2GenerationOfCombinatorialObjects
{
    public class _2_1_4
    {
        public List<List<int>> SimpleAnalog(int k, int n)
        {
            var vs = Enumerable.Range(0, n + 1).Select(x => new List<int>() { x }).ToList();

            if (k <= 1)
            {
                return vs;
            }

            var rest = SimpleAnalog(k - 1, n + 1);
            var result = vs.SelectMany(x => rest.Select(v => { var r = x.ToList(); r.AddRange(v); return r; })).ToList();

            return result;
        }

        public List<List<int>> Run(int k)
        {
            var initial = Enumerable.Range(0, k).Select(x => 0).ToList();

            var result = new List<List<int>>();
            result.Add(initial);

            var shouldContinue = true;
            do
            {
                var copy = initial.ToList();
                var currentIndex = k - 1;
                for (; currentIndex >= 0; currentIndex--)
                {
                    if (copy[currentIndex] < currentIndex)
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
            public void _2_1_4Combine_ForLength3FromBinaryValues_Success()
            {
                var k = 3;
                var testable = new _2_1_4();
                var expected = new[] { "000", "001", "002", "010", "011", "012" };

                var result = testable.Run(k);

                Assert.That(result.Select(x => string.Join("", x)).ToArray(), Is.EquivalentTo(expected));
            }

            [Test]
            public void PropertyCombine_AnyInputValues_DifferenceShouldBeConstantForAllValues()
            {
                var testable = new _2_1_4();
                Prop.ForAll(
                    Arb.From<int>().Filter(x => x > 0 && x < 7),
                    k =>
                    {
                        var result = testable.Run(k).Select(x => x.ToList()).ToList();
                        var analogResult = testable.SimpleAnalog(k, 0);

                        for (var i = 0; i < result.Count; i++)
                        {
                            Assert.That(result[i].SequenceEqual(analogResult[i]));
                        }
                    }).QuickCheckThrowOnFailure();
            }
        }
    }
}
