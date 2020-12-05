using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day05
    {
        [TestCase("BFFFBBFRRR", 070, 7, 567)]
        [TestCase("FFFBBBFRRR", 014, 7, 119)]
        [TestCase("BBFFBBFRLL", 102, 4, 820)]
        public void Seats_are_parsed(string str, int row, int col, int id)
        {
            var seat = Day05.Seat.Parse(str);
            Assert.AreEqual(row, seat.Row);
            Assert.AreEqual(col, seat.Col);
            Assert.AreEqual(id, seat.Id);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 05);
            Puzzle.HasAnswer(998, Day05.One, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 05);
            Puzzle.HasAnswer(676, Day05.Two, with: input);
        }
    }
}
