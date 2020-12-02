using AdventOfCode;
using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day03
    {
        [TestCase(159, @"
            R75,D30,R83,U83,L12,D49,R71,U7,L72
            U62,R66,U55,R34,D71,R55,D58,R83")]
        [TestCase(135, @"
            R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
            U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
        public void part_one_example(int distance, string input)
        {
            Puzzle.HasAnswer(distance, Day03.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 03);
            Puzzle.HasAnswer(1195, Day03.One, with: input);
        }

        [TestCase(610, @"
            R75,D30,R83,U83,L12,D49,R71,U7,L72
            U62,R66,U55,R34,D71,R55,D58,R83")]
        [TestCase(410, @"
            R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
            U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
        public void part_two_example(int steps, string input)
        {
            Puzzle.HasAnswer(steps, Day03.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 03);
            Puzzle.HasAnswer(91518, Day03.Two, with: input);
        }
    }
}
