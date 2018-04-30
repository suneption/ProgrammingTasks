using _5_1_3.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Resources._1;

            Console.WriteLine(input);

            var result = Solve(input);

            Console.WriteLine(result);
        }

        static string Solve(string input)
        {
            var state = States.Main;
            var result = new List<char>();

            foreach (var ch in input)
            {
                switch (state)
                {
                    case States.Main:
                        if (ch == '{')
                        {
                            state = States.Inside;
                        }
                        else
                        {
                            result.Add(ch);
                        }
                        break;
                    case States.Inside:
                        if (ch == '}')
                        {
                            result.Add(' ');
                            state = States.Main;
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return string.Join("", result);
        }
    }

    enum States
    {
        Main = 0,
        Inside
    }
}
