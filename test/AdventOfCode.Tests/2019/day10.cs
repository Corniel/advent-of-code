using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day10
    {
        [Test]
        public void part_one_example_1()
        {
            var input = @"
                .#..#
                .....
                #####
                ....#
                ...###";
            Puzzle.HasAnswer(8, Day10.One, with: input);
        }
        [Test]
        public void part_one_example_2()
        {
            var input = @"
                ......#.#.
                #..#.#....
                ..#######.
                .#.#.###..
                .#..#.....
                ..#....#.#
                #..#....#.
                .##.#..###
                ##...#..#.
                .#....####";
            Puzzle.HasAnswer(33, Day10.One, with: input);
        }

        [Test]
        public void part_one_example_3()
        {
            var input = @"
                #.#...#.#.
                .###....#.
                .#....#...
                ##.#.#.#.#
                ....#.#.#.
                .##..###.#
                ..#...##..
                ..##....##
                ......#...
                .####.###.";
            Puzzle.HasAnswer(35, Day10.One, with: input);
        }

        [Test]
        public void part_one_example_4()
        {
            var input = @"
                .#..#..###
                ####.###.#
                ....###.#.
                ..###.##.#
                ##.##.#.#.
                ....###..#
                ..#.#..#.#
                #..#.#.###
                .##...##.#
                .....#.#..";
            Puzzle.HasAnswer(41, Day10.One, with: input);
        }

        [Test]
        public void part_one_example_5()
        {
            var input = @"
                .#..##.###...#######
                ##.############..##.
                .#.######.########.#
                .###.#######.####.#.
                #####.##.#.##.###.##
                ..#####..#.#########
                ####################
                #.####....###.#.#.##
                ##.#################
                #####.##.###..####..
                ..######..##.#######
                ####.##.####...##..#
                .#####..#.######.###
                ##...#.##########...
                #.##########.#######
                .####.#.###.###.#.##
                ....##.##.###..#####
                .#.#.###########.###
                #.#.#.#####.####.###
                ###.##.####.##.#..##";
            Puzzle.HasAnswer(210, Day10.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 10);
            Puzzle.HasAnswer(347, Day10.One, with: input);
        }

        [Test]
        public void part_two_example()
        {
            var input = @"
                .#..##.###...#######
                ##.############..##.
                .#.######.########.#
                .###.#######.####.#.
                #####.##.#.##.###.##
                ..#####..#.#########
                ####################
                #.####....###.#.#.##
                ##.#################
                #####.##.###..####..
                ..######..##.#######
                ####.##.####...##..#
                .#####..#.######.###
                ##...#.##########...
                #.##########.#######
                .####.#.###.###.#.##
                ....##.##.###..#####
                .#.#.###########.###
                #.#.#.#####.####.###
                ###.##.####.##.#..##";
            Puzzle.HasAnswer(802, Day10.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 10);
            Puzzle.HasAnswer(829, Day10.Two, with: input);
        }
    }
}
