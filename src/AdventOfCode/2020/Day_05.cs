using Advent_of_Code;
using NUnit.Framework;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_05
    {
        [Puzzle(answer: 998, year: 2020, day: 05)]
        public void part_one(long answer, string input)
        {
            var outcome = input.Lines().Select(Seat.Parse).Max(seat => seat.Id);
            Assert.That(outcome, Is.EqualTo(answer));
        }

        [Puzzle(answer: 676, year: 2020, day: 05)]
        public void part_two(long answer, string input)
        {
            var outcome = Gap(input.Lines().Select(Seat.Parse).OrderBy(s => s.Id).ToArray());
            Assert.That(outcome, Is.EqualTo(answer));
        }

        private static int Gap(Seat[] seats)
        {
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