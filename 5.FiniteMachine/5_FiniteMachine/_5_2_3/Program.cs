using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "1.113";
            var expected = 1.113;

            var (isSuccess, value) = Solve(input);

            Console.WriteLine(input);
            Console.WriteLine($"isSuccess {isSuccess}, value {value}");
            Contract.Assert(expected == value);
        }

        static (bool isSuccess, double value) Solve(string input)
        {
            var state = States.Initial;
            var value = 0.0;
            var step = 0.1;
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

                    var (newState, newValue, newStep) = GoToNewState(state, chars.Current, value, step);
                    state = newState;
                    value = newValue;
                    step = newStep;
                }
            }

            return (state == States.Accept, value);
        }

        private static (States newState, double newValue) InitialState(char current, double value)
        {
            States newState;
            var newValue = value;

            if (current == ' ')
            {
                newState = States.Initial;
            }
            else if (char.IsDigit(current))
            {
                newState = States.IntPart;
                newValue = int.Parse(current.ToString());
            }
            else
            {
                newState = States.Error;
            }

            return (newState, newValue);
        }

        private static (States newState, double newValue, double newStep) GoToNewState(States state, char current, double value, double step)
        {
            States newState;
            var newValue = value;
            var newStep = step;

            if (state == States.Initial)
            {
                (newState, newValue) = InitialState(current, value);
            }
            else if (state == States.IntPart)
            {
                if (char.IsDigit(current))
                {
                    newState = States.IntPart;
                    var digit = int.Parse(current.ToString());
                    value = value * 10 + digit;
                }
                else if (current == '.')
                {
                    newState = States.DecPoint;
                    newStep = 0.1;
                }
                else
                {
                    newState = States.Accept;
                }
            }
            else if (state == States.DecPoint)
            {
                if (char.IsDigit(current))
                {
                    newState = States.FracPart;
                    (newValue, newStep) = AddFracPart(value, current, step);
                    value = newValue;
                    step = newStep;
                }
                else
                {
                    newState = States.Error;
                }
            }
            else if (state == States.FracPart)
            {
                if (char.IsDigit(current))
                {
                    newState = States.FracPart;
                    (newValue, newStep) = AddFracPart(value, current, step);
                    value = newValue;
                    step = newStep;
                }
                else
                {
                    newState = States.Accept;
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return (newState, value, step);
        }

        private static (double value, double step) AddFracPart(double value, char ch, double step)
        {
            var digit = int.Parse(ch.ToString());
            var newValue = value + digit * step;
            var newStep = step / 10;
            return (newValue, newStep);
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
    }
}
