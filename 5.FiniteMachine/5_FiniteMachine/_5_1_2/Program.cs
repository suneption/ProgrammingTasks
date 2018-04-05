using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_1_2
{
    class Program
    {
        public const string PATTERN = "abcda";

        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var simple = Simple(input);
            Console.WriteLine(simple);

            var _3states = _3States(input);
            Console.WriteLine(_3states);

            var _2st = By2States(input);
            Console.WriteLine(_2st);
        }

        static string Simple(string input)
        {
            var result = input.Replace(PATTERN, "");
            return result;
        }

        static string _3States(string input)
        {
            var state = States.Main;
            var result = new List<char>();

            foreach (var ch in input)
            {
                switch (state)
                {
                    case States.Main:
                        if (ch == 'a')
                        {
                            state = States.AfterA;
                        }
                        else
                        {
                            result.Add(ch);
                        }
                        break;
                    case States.AfterA:
                        if (ch == 'b')
                        {
                            state = States.AfterB;
                        }
                        else if (ch == 'a')
                        {
                            state = States.AfterA;
                            result.Add('a');
                        }
                        else
                        {
                            state = States.Main;
                            result.Add('a');
                            result.Add(ch);
                        }
                        break;
                    case States.AfterB:
                        if (ch == 'c')
                        {
                            state = States.Main;
                        }
                        else
                        {
                            state = States.Main;
                            result.Add('a');
                            result.Add('b');
                            result.Add(ch);
                        }
                        break;
                    default:
                        break;
                }
            }

            var output = string.Join("", result);
            return output;
        }

        static string By2States(string input)
        {
            var state = StatesOfGeneralSolution.Main;
            var result = new List<char>();
            var buffer = new Queue<char>();

            foreach (var ch in input)
            {
                switch (state)
                {
                    case StatesOfGeneralSolution.Main:
                        if (ch == PATTERN.First())
                        {
                            state = StatesOfGeneralSolution.After;
                            buffer.Enqueue(ch);
                        }
                        else
                        {
                            result.Add(ch);
                        }
                        break;
                    case StatesOfGeneralSolution.After:
                        var expectedCh = PATTERN.Skip(buffer.Count).First();
                        if (expectedCh == ch)
                        {
                            buffer.Enqueue(ch);
                            if (buffer.Count == PATTERN.Length - 1)
                            {
                                state = StatesOfGeneralSolution.Ending;
                            }
                        }
                        else if (ch == PATTERN.First())
                        {
                            result.AddRange(buffer);
                            buffer.Clear();
                            buffer.Enqueue(ch);
                            state = StatesOfGeneralSolution.After;
                        }
                        else
                        {
                            result.AddRange(buffer);
                            buffer.Clear();
                            result.Add(ch);
                            state = StatesOfGeneralSolution.Main;
                        }
                        break;
                    case StatesOfGeneralSolution.Ending:
                        if (PATTERN.Last() == ch)
                        {
                            buffer.Clear();
                            state = StatesOfGeneralSolution.Main;
                        }
                        else if (ch == PATTERN.First())
                        {
                            result.AddRange(buffer);
                            buffer.Clear();
                            buffer.Enqueue(ch);
                            state = StatesOfGeneralSolution.After;
                        }
                        else
                        {
                            result.AddRange(buffer);
                            buffer.Clear();
                            result.Add(ch);
                            state = StatesOfGeneralSolution.Main;
                        }
                        break;
                    default:
                        break;
                }
            }

            var output = string.Join("", result);
            return output;
        }

        private enum States
        {
            Main = 0,
            AfterA,
            AfterB
        }

        private enum StatesOfGeneralSolution
        {
            Main = 0,
            After,
            Ending
        }
    }
}
