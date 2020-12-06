using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day06
    {
        [Test]
        public void part_one_example()
        {
            var input = @"abc

a
b
c

ab
ac

a
a
a
a

b
";
            Puzzle.HasAnswer(11, Day06.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 06);
            Puzzle.HasAnswer(7110, Day06.One, with: input);
        }
  
        [Test]
        public void part_two_example()
        {
            var input = @"abc

a
b
c

ab
ac

a
a
a
a

b
";
            Puzzle.HasAnswer(6, Day06.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 06);
            Puzzle.HasAnswer(3628, Day06.Two, with: input);
        }
    }
}
