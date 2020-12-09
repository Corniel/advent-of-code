using System.Linq;

namespace AdventOfCode._2020
{
    public class Day09
    {
        private const int size = 25;
        public static long One(string input)
        {
            var numbers = input.Int64s().ToArray();

            for (var i = size; i < numbers.Length; i++)
            {
                var number = numbers[i];
                if (!Matches(numbers, i, number))
                {
                    return number;
                }
            }

            throw new NoAnswer();
        }

        private static bool Matches(long[] numbers, int index, long number)
        {
            for (var p0 = index - size; p0 < index; p0++)
            {
                var n0 = numbers[p0];

                for (var p1 = p0 + 1; p1 < index; p1++)
                {
                    var n1 = numbers[p1];
                    if(n0 + n1 == number)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static long Two(string input)
        {
            var numbers = input.Int64s().ToArray();

            long sum = 144381670;

            var lo = 0;
            var hi = 1;
            var bottom = numbers[lo];
            var top = numbers[hi];
            var test = bottom + top;

            while (hi < numbers.Length)
            {
                if (test == sum)
                {
                    var min = numbers.Skip(lo).Take(hi - lo).Min();
                    var max = numbers.Skip(lo).Take(hi - lo).Max();
                    return min + max;
                }
                else if (test < sum)
                {
                    top = numbers[++hi];
                    test += top;
                }
                else
                {
                    test -= bottom;
                    bottom = numbers[++lo];
                }
            }
            throw new NoAnswer();
        }
    }
}