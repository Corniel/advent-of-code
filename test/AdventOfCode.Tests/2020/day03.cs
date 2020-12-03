using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day03
    {
        [Test]
        public void part_one_example()
        {
            var input = @"
                ..##.......
                #...#...#..
                .#....#..#.
                ..#.#...#.#
                .#...##..#.
                ..#.##.....
                .#.#.#....#
                .#........#
                #.##...#...
                #...##....#
                .#..#...#.#";
            Puzzle.HasAnswer(7, Day03.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 03);
            Puzzle.HasAnswer(220, Day03.One, with: input);
        }
  
        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 03);
            Puzzle.HasAnswer(2138320800L, Day03.Two, with: input);
        }
    }
}
