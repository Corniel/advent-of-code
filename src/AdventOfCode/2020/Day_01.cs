using Advent_of_Code;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class Day_01
    {
        [Example(answer: 514579, @"
            1721
            979
            366
            299
            675
            1456")]
        [Puzzle(answer: 786811, year: 2020, day: 01)]
        public void part_one(long answer, string input)
        {
            Assert.That(SumOfForTwo(2020, UniqueNumbers.Parse(input)), Is.EqualTo(answer));
        }

        [Example(answer: 241861950L, @"
            1721
            979
            366
            299
            675
            1456")]
        [Puzzle(answer: 199068980L, year: 2020, day: 01)]
        public void part_two(long answer, string input)
        {
            Assert.That(SumOfForThree(2020, UniqueNumbers.Parse(input)), Is.EqualTo(answer));
        }

        private static int SumOfForTwo(int sum, UniqueNumbers numbers)
        {
            foreach (var number0 in numbers.Range(max: sum / 2))
            {
                var number1 = sum - number0;
                if (numbers.Contains(number1))
                {
                    return number0 * number1;
                }
            }
            throw new NoAnswer();
        }

        private static long SumOfForThree(int sum, UniqueNumbers numbers)
        {
            foreach (var number0 in numbers.Range(max: sum / 3))
            {
                foreach (var number1 in numbers.Range(min: number0 + 1, max: (sum - number0) / 2))
                {
                    var number2 = sum - number0 - number1;
                    if (numbers.Contains(number2))
                    {
                        return (long)number0 * (long)number1 * (long)number2;
                    }
                }
            }
            throw new NoAnswer();
        }
    }
}