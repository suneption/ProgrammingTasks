using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var state = States.Main;
            var result = new List<char>();

            foreach (var ch in input)
            {
                switch (state)
                {
                    case States.Main:
                        if (ch == '*')
                        {
                            state = States.After;
                        }
                        else
                        {
                            result.Add(ch);
                        }
                        break;
                    case States.After:
                        if (ch == '*')
                        {
                            result.Add('^');
                        }
                        else
                        {
                            result.Add('*');
                            result.Add(ch);
                        }
                        state = States.Main;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(string.Join("", result));
        }

        private enum States
        {
            Main = 0,
            After
        }
    }
}
