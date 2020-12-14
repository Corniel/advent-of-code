using Advent_of_Code;
using SmartAss;
using SmartAss.Parsing;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_05
    {
        [Puzzle(answer: 998, year: 2020, day: 05)]
        public uint part_one(string input) => input.Lines(Seat).Max();

        [Puzzle(answer: 676, year: 2020, day: 05)]
        public uint part_two(string input)
        {
            var seats = input.Lines(Seat).OrderBy(s => s).ToArray();
            return seats.Where((seat, index) => seats[index + 1] - seat > 1)
                .First() + 1;
        }

        private static uint Seat(string line) => Bits.UInt32.Parse(line, ones: "BR", zeros: "FL");
    }
}