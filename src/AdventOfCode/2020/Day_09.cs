using Advent_of_Code;
using NUnit.Framework;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_09
    {
        [Puzzle(144381670L, year: 2020, day: 09)]
        public void part_one(long answer, string input)
        {
            var inconsistency = Inconsistency(input.Int64s().ToArray());
            Assert.That(inconsistency, Is.EqualTo(answer));
        }

        [Puzzle(20532569L, year: 2020, day: 09)]
        public void part_two(long answer, string input)
        {
            var weakness = Weaknmess(input.Int64s().ToArray());
            Assert.That(weakness, Is.EqualTo(answer));
        }

        private static long Inconsistency(long[] numbers)
        {
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
        private static long Weaknmess(long[] numbers)
        {
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
        private const int size = 25;
    }
}