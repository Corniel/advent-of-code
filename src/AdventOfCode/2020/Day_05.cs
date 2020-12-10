using Advent_of_Code;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_05
    {
        [Puzzle(answer: 998, year: 2020, day: 05)]
        public int part_one(string input)
            => input.Lines().Select(Seat.Parse).Max(seat => seat.Id);

        [Puzzle(answer: 676, year: 2020, day: 05)]
        public int part_two(string input)
        {
            var seats = input.Lines().Select(Seat.Parse).OrderBy(s => s.Id).ToArray();
            var previous = seats[0];

            foreach (var seat in seats.Skip(1))
            {
                if (seat.Id - 1 != previous.Id)
                {
                    return seat.Id - 1;
                }
                previous = seat;
            }
            throw new NoAnswer();
        }

        internal readonly struct Seat
        {
            public Seat(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public int Row { get; }
            public int Col { get; }
            public int Id => Row * 8 + Col;

            public override string ToString() => $"Row:{Row}, Col: {Col}, ID: {Id}";

            public static Seat Parse(string str)
            {
                var row = 0;
                var col = 0;
                int r = 64;
                int c = 4;

                foreach (var ch in str.Take(7))
                {
                    if (ch == 'B')
                    {
                        row += r;
                    }
                    r /= 2;
                }
                foreach (var ch in str.Skip(7).Take(3))
                {
                    if (ch == 'R')
                    {
                        col += c;
                    }
                    c /= 2;
                }
                return new Seat(row, col);
            }
        }
    }
}