using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day06
    {
        [Test]
        public void part_one_example()
        {
            var input = @"
                COM)B
                B)C
                C)D
                D)E
                E)F
                B)G
                G)H
                D)I
                E)J
                J)K
                K)L";
            Puzzle.HasAnswer(42, Day06.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 06);
            Puzzle.HasAnswer(333679, Day06.One, with: input);
        }
  
        [Test]
        public void part_two_example()
        {
            var input = @"
                COM)B
                B)C
                C)D
                D)E
                E)F
                B)G
                G)H
                D)I
                E)J
                J)K
                K)L
                K)YOU
                I)SAN";
            Puzzle.HasAnswer(4, Day06.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 06);
            Puzzle.HasAnswer(370, Day06.Two, with: input);
        }
    }
}
