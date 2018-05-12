using _5_2_1.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Resources._1;
            var expected = bool.Parse(Resources._1a);

            Console.WriteLine(input);

            var result = Solve(input);

            Console.WriteLine(result);
            Contract.Assert(result == expected);
        }

        static bool Solve(string input)
        {
            var state = States.Initial;
            using (var chars = input.GetEnumerator())
            {
                while (state != States.Accept && state != States.Error)
                {
                    if (!chars.MoveNext())
                    {
                        if (state == States.FracPart || state == States.IntPart)
                        {
                            state = States.Accept;
                        }
                        else
                        {
                            state = States.Error;
                        }

                        break;
                    }

                    if (state == States.Initial)
                    {
                        if (chars.Current == ' ')
                        {
                            state = States.Initial;
                        }
                        else if (char.IsDigit(chars.Current))
                        {
                            state = States.IntPart;
                        }
                        else
                        {
                            state = States.Error;
                        }
                    }
                    else if (state == States.IntPart)
                    {
                        if (char.IsDigit(chars.Current))
                        {
                            state = States.IntPart;
                        }
                        else if (chars.Current == '.')
                        {
                            state = States.DecPoint;
                        }
                        else
                        {
                            state = States.Accept;
                        }
                    }
                    else if (state == States.DecPoint)
                    {
                        if (char.IsDigit(chars.Current))
                        {
                            state = States.FracPart;
                        }
                        else
                        {
                            state = States.Error;
                        }
                    }
                    else if (state == States.FracPart)
                    {
                        if (char.IsDigit(chars.Current))
                        {
                            state = States.FracPart;
                        }
                        else
                        {
                            state = States.Accept;
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            }
            
            return state == States.Accept;
        }

        enum States
        {
            Initial = 1,
            Accept,
            Error,
            IntPart,
            DecPoint,
            FracPart
        }

        class CustomEnumerator : IDisposable
        {
            private IEnumerable<char> _initial;
            private IEnumerator<char> _enumerator;
            private bool _hasNext;
            private char _next;
            private char _current;

            public CustomEnumerator(IEnumerable<char> initial)
            {
                _initial = initial;
                _enumerator = _initial.GetEnumerator();
                _hasNext = true;
                if (_enumerator.MoveNext())
                {
                    _next = _enumerator.Current;
                }
                else
                {
                    _hasNext = false;
                }
            }
            
            public void Dispose()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                }
            }

            public bool HasNext()
            {
                return _hasNext;
            }

            public char Next()
            {
                if (!_hasNext)
                {
                    throw new InvalidOperationException();
                }

                return _next;
            }

            public void Move()
            {
                if (_enumerator.MoveNext())
                {
                    _next = _enumerator.Current;
                }
                else
                {
                    _hasNext = false;
                }
            }
        }
    }
}
