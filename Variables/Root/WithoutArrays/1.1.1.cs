namespace WithoutArrays
{
    public class _1_1_1
    {
        public (int, int) Run(int a, int b)
        {
            var temp = a;
            a = b;
            b = temp;

            return (a, b);
        }
    }
}
