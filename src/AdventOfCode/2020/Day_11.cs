using Advent_of_Code;
using SmartAss;
using SmartAss.Collections;
using SmartAss.Navigation;
using SmartAss.Parsing;
using SmartAss.Topology;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_11
    {
        [Example(answer: 37, @"
            L.LL.LL.LL
            LLLLLLL.LL
            L.L.L..L..
            LLLL.LL.LL
            L.LL.LL.LL
            L.LLLLL.LL
            ..L.L.....
            LLLLLLLLLL
            L.LLLLLL.L
            L.LLLLL.LL")]
        [Puzzle(answer: 2481, year: 2020, day: 11)]
        public int part_one(string input)
        {
            var plane = Plane.Parse(input);
            while (plane.Any(seat => seat.Changed))
            {
                foreach (var seat in plane) { seat.Changed = false; }
                foreach (var seat in plane)
                {
                    var next = seat.GetNext();
                    seat.Changed = next != seat.Occupied;
                    seat.Next = next;
                }
                foreach (var seat in plane)
                {
                    seat.Occupied = seat.Next;
                }
            }
            return plane.Occupied;
        }

        [Example(answer: 26, @"
            L.LL.LL.LL
            LLLLLLL.LL
            L.L.L..L..
            LLLL.LL.LL
            L.LL.LL.LL
            L.LLLLL.LL
            ..L.L.....
            LLLLLLLLLL
            L.LLLLLL.L
            L.LLLLL.LL")]
        [Puzzle(answer: 2227, year: 2020, day: 11)]
        public long part_two(string input)
        {
            var plane = Plane.Parse(input);
            while (plane.Any(seat => seat.Changed))
            {
                foreach (var seat in plane) { seat.Changed = false; }
                foreach (var seat in plane)
                {
                    var next = seat.GetNextInsight();
                    seat.Changed = next != seat.Occupied;
                    seat.Next = next;
                }
                foreach (var seat in plane)
                {
                    seat.Occupied = seat.Next;
                }
            }
            return plane.Occupied;
        }

        private class Plane : Grid<Seat>
        {
            public Plane(int cols, int rows)
                : base(cols, rows, GridType.Grid | GridType.Diagonal) => Do.Nothing();

            public int Occupied => this.Count(seat => seat.Occupied);

            public override string ToString() => ToString(Formatter);

            private static string Formatter(Seat seat)
            {
                if (seat is null) { return "."; }
                else { return seat.Occupied ? "#" : "L"; }
            }

            protected override Seat Create(int index, int col, int row, int neighbors)
                => new Seat(index, col, row, neighbors);

            public static Plane Parse(string input)
            {
                var chars = input.CharPixels();
                var plane = new Plane(chars.Cols, chars.Rows);

                foreach (var tile in chars)
                {
                    var seat = plane[tile.Position];
                    seat.Occupied = tile.Char == '#';
                    if (tile.Char == '.')
                    {
                        plane.Remove(seat);
                    }
                }
                foreach (var seat in plane)
                {
                    seat.InSight.AddRange(directions
                        .Select(dir => seat.Position
                            .Repeat(dir)
                            .TakeWhile(p => plane.OnGrid(p))
                            .Select(p => plane[p])
                            .FirstOrDefault(s => s != null))
                        .Where(s => s != null));
                }

                return plane;
            }
            private static readonly Vector[] directions = CompassPoints.All.Select(p => p.ToVector()).ToArray();

        }
        private class Seat : GridTile<Seat>
        {
            public Seat(int index, int col, int row, int neighbors)
                : base(index, col, row, neighbors) => Do.Nothing();

            public bool Occupied { get; set; }
            public bool Empty => !Occupied;
            public bool Next { get; set; }
            public bool Changed { get; set; } = true;

            public SimpleList<Seat> InSight { get; } = new SimpleList<Seat>(8);

            public bool GetNext()
                => Empty
                ? Neighbors.All(n => n.Empty)
                : Neighbors.Count(n => n.Occupied) < 4;

            public bool GetNextInsight()
              => Empty
                ? InSight.All(n => n.Empty)
                : InSight.Count(n => n.Occupied) < 5;

            public override string ToString() => $"[{Col:00}, {Row:00}] Occupied: {Occupied} ({Next})";
        }
    }
}