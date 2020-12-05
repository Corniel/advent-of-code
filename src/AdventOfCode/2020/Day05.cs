using System.Linq;

namespace AdventOfCode._2020
{
    public class Day05
    {
        [Puzzle(2020, 05, Part.one)]
        public static int One(string input)
            => input.Lines().Select(Seat.Parse).Max(seat => seat.Id);

        [Puzzle(2020, 05, Part.two)]
        public static int Two(string input)
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

        public readonly struct Seat
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