using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutArrays
{
    public class _1_1_6
    {
        public int Run(int a, int b)
        {
            var a1 = Enumerable.Range(0, a).Select(x => 1).Sum();
            var b1 = Enumerable.Range(0, b).Select(x => 1).Sum();

            return a1 + b1;
        }
    }
}
