using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsDeepEqual = NUnit.DeepObjectCompare.Is;

namespace Root.TreeTraversal
{
    public class _3_1_1
    {
        public List<Pos> Run(int n)
        {
            var queens = new Stack<Pos>();
            var currRow = -1;
            var currCol = 0;
            var state = State.Start;
            var hasRight = false;
            var hasDown = false;

            while (state != State.End)
            {
                switch (state)
                {
                    case State.Start:
                        var hasLeftUp = HasLeftUp(currRow, n);
                        if (hasLeftUp)
                        {
                            currRow++;
                            currCol = 0;
                            state = State.Undefined;
                        }
                        else
                        {
                            state = State.CheckedAllLeftUpPositions;
                        }
                        break;
                    case State.Work:
                        var currPos = new Pos { Row = currRow, Col = currCol };
                        queens.Push(currPos);
                        if (!HasLeftUp(currRow, n))
                        {
                            state = State.End;
                        }
                        else
                        {
                            state = State.Start;
                        }
                        break;
                    case State.Undefined:
                        var isCorrect = IsCorrect(currRow, currCol, queens);
                        if (isCorrect)
                        {
                            state = State.Work;
                        }
                        else
                        {
                            state = State.CheckedAllLeftUpPositions;
                        }
                        break;
                    case State.CheckedAllLeftUpPositions:
                        hasRight = HasRight(currCol, n);
                        if (hasRight)
                        {
                            currCol++;
                            state = State.Undefined;
                        }
                        else
                        {
                            hasDown = HasDown(currRow);
                            if (hasDown)
                            {
                                var last = queens.Pop();
                                currRow = last.Row;
                                currCol = last.Col;
                                state = State.CheckedAllLeftUpPositions;
                            }
                            else
                            {
                                state = State.End;
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return new List<Pos>(queens);
        }

        public enum State
        {
            Start = 0,
            Undefined,
            Work,
            CheckedAllLeftUpPositions,
            End
        }

        public bool HasLeftUp(int row, int n)
        {
            if (row < n - 1)
            {
                return true;
            }

            return false;
        }

        public bool HasRight(int col, int n)
        {
            if (col < n - 1)
            {
                return true;
            }

            return false;
        }

        public bool IsCorrect(int currRow, int currCol, Stack<Pos> existing)
        {
            var firstOnSimilarHorizontal = existing.FirstOrDefault(x => x.Row == currRow);
            var firstOnSimilarVertical = existing.FirstOrDefault(x => x.Col == currCol);
            var firstOnSimilarDiagonal = existing.FirstOrDefault(x => Math.Abs(x.Row - currRow) == Math.Abs(x.Col - currCol));

            var isCorrect = firstOnSimilarHorizontal == null && firstOnSimilarVertical == null && firstOnSimilarDiagonal == null;

            return isCorrect;
        }

        public bool HasDown(int currRow)
        {
            return currRow > 0;
        }

        public class Pos
        {
            public int Row { get; set; }
            public int Col { get; set; }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void _3_1_1_Has2x2Field_DontHaveAnyPossibility()
            {
                var n = 2;
                var testable = new _3_1_1();

                var result = testable.Run(n);

                Assert.That(!result.Any());
            }

            [Test]
            public void _3_1_1_Has3x3Field_DontHaveAnyPossibility()
            {
                var n = 3;
                var testable = new _3_1_1();

                var result = testable.Run(n);

                Assert.That(!result.Any());
            }

            [Test]
            public void _3_1_1_Has4x4Field_DontHaveAnyPossibility()
            {
                var n = 4;
                var testable = new _3_1_1();
                var expected = new List<Pos>
                {
                    new Pos { Row = 0, Col = 1 },
                    new Pos { Row = 1, Col = 3 },
                    new Pos { Row = 2, Col = 0 },
                    new Pos { Row = 3, Col = 2 }
                };

                var result = testable.Run(n);
                result.Reverse();

                Assert.That(result.Any());
                Assert.That(result, IsDeepEqual.DeepEqualTo(expected));
            }
        }
    }
}
