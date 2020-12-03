using AdventOfCode;
using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day07
    {
        [TestCase(43210, "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0")]
        [TestCase(54321, "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0")]
        [TestCase(65210, "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0")]
        public void part_one_example(int signal, string input)
        {
            Puzzle.HasAnswer(signal, Day07.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 07);
            Puzzle.HasAnswer(330, Day07.One, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 07);
            Puzzle.HasAnswer(0, Day07.Two, with: input);
        }
    }
}
