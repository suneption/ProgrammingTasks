using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Root._2GenerationOfCombinatorialObjects
{
    public class _2_1_1
    {
        public List<List<int>> SimpleAnalog(int k, int n)
        {
            var vs = Enumerable.Range(0, n + 1).Select(x => new List<int>() { x }).ToList();

            if (k <= 1)
            {
                return vs;
            }

            var rest = SimpleAnalog(k - 1, n);
            var result = vs.SelectMany(x => rest.Select(v => { var r = x.ToList(); r.AddRange(v); return r; } )).ToList();

            return result;
        }

        public List<int[]> Run(int k, int n)
        {
            var initial = Enumerable.Range(0, k).Select(x => 0).ToArray();
            var last = Enumerable.Range(0, k).Select(x => n).ToArray();

            var result = new List<int[]>();
            result.Add(initial);

            do
            {
                var copy = initial.ToArray();
                var currentIndex = k - 1;
                for (var i = currentIndex; i >= 0; i--)
                {
                    if (copy[i] < n)
                    {
                        currentIndex = i;
                        break;
                    }
                }
                copy[currentIndex] = copy[currentIndex] + 1;
                for (var i = currentIndex + 1; i < k; i++)
                {
                    copy[i] = 0;
                }
                result.Add(copy);
                initial = copy;
            } while (!initial.SequenceEqual(last));

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
                var testable = new _2_1_1();
                var expected = new[] { "000", "001", "010", "011", "100", "101", "110", "111" };

                var result = testable.Run(k, n);

                Assert.That(result.Select(x => string.Join("", x)).ToArray(), Is.EquivalentTo(expected));
            }

            [Test]
            public void PropertyCombine_AnyInputValues_AnalogAndMainSolutionShouldBeEqual()
            {
                var testable = new _2_1_1();
                Prop.ForAll(Arb.From<int>().Filter(x => x > 0 && x < 5), Arb.From<int>().Filter(x => x > 0 && x < 10),
                    (k, n) =>
                    {
                        var result = testable.Run(k, n).Select(x => x.ToList()).ToList();
                        var analogResult = testable.SimpleAnalog(k, n);

                        for (var i = 0; i < result.Count; i++)
                        {
                            Assert.That(result[i].SequenceEqual(analogResult[i]));
                        }
                    }).QuickCheckThrowOnFailure();
            }

            [Test]
            public void PropertyCombine_AnyInputValues_FirstAndLastValuesAreCorrect()
            {
                var testable = new _2_1_1();
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
