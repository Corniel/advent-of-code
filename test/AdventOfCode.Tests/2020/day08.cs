using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day08
    {
        [Test]
        public void part_one_example()
        {
            var input = @"
                nop +0
                acc +1
                jmp +4
                acc +3
                jmp -3
                acc -99
                acc +1
                jmp -4
                acc +6";
            Puzzle.HasAnswer(5L, Day08.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 08);
            Puzzle.HasAnswer(1584L, Day08.One, with: input);
        }
  
        [Test]
        public void part_two_example()
        {
            var input = @"nop +0
                acc +1
                jmp +4
                acc +3
                jmp -3
                acc -99
                acc +1
                jmp -4
                acc +6";
            Puzzle.HasAnswer(8, Day08.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 08);
            Puzzle.HasAnswer(920, Day08.Two, with: input);
        }
    }
}
