using AdventOfCode;
using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day04
    {
        [TestCase(1, 111111)]
        [TestCase(0, 223450)]
        [TestCase(0, 123789)]
        [TestCase(1, 123779)]
        public void part_one_example(int count, int number)
        {
            var input = $"{number}-{number}";
            Puzzle.HasAnswer(count, Day04.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = "235741-706948";
            Puzzle.HasAnswer(1178, Day04.One, with: input);
        }

        [TestCase(1, 112233)]
        [TestCase(0, 123444)]
        [TestCase(1, 111122)]
        public void part_two_example(int count, int number)
        {
            var input = $"{number}-{number}";
            Puzzle.HasAnswer(count, Day04.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = "235741-706948";
            Puzzle.HasAnswer(763, Day04.Two, with: input);
        }
    }
}
