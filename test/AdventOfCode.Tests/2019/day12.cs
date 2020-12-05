using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day12
    {
        [Test]
        public void part_one_example_1()
        {
            var input = @"
                <x=-1, y=0, z=2>
                <x=2, y=-10, z=-7>
                <x=4, y=-8, z=8>
                <x=3, y=5, z=-1>";

            Assert.AreEqual(179, Day12.One(input, steps: 10));
        }

        [Test]
        public void part_one_example_2()
        {
            var input = @"
                <x=-8, y=-10, z=0>
                <x=5, y=5, z=10>
                <x=2, y=-7, z=3>
                <x=9, y=-8, z=-3";

            Assert.AreEqual(1940, Day12.One(input, steps: 100));
        }

        [Test]
        public void part_one()
        {
            var input = @"
                <x=17, y=-9, z=4>
                <x=2, y=2, z=-13>
                <x=-1, y=5, z=-1>
                <x=4, y=7, z=-7>";

            Puzzle.HasAnswer(7202, Day12.One, with: input);
        }
  
        [Test]
        public void part_two_example_1()
        {
            var input = @"
                <x=-1, y=0, z=2>
                <x=2, y=-10, z=-7>
                <x=4, y=-8, z=8>
                <x=3, y=5, z=-1>";

            Puzzle.HasAnswer(2772L, Day12.Two, with: input);
        }

        [Test]
        public void part_two_example_2()
        {
            var input = @"
                <x=-8, y=-10, z=0>
                <x=5, y=5, z=10>
                <x=2, y=-7, z=3>
                <x=9, y=-8, z=-3";

            Puzzle.HasAnswer(4686774924L, Day12.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = @"
                <x=17, y=-9, z=4>
                <x=2, y=2, z=-13>
                <x=-1, y=5, z=-1>
                <x=4, y=7, z=-7>";

            Puzzle.HasAnswer(537881600740876L, Day12.Two, with: input);
        }
    }
}
