using Advent_of_Code;
using Advent_of_Code.Maths;
using SmartAss;
using SmartAss.Topology;
using System;
using System.Linq;
using System.Text;

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
                    var next = seat.GetNextPartTwo(plane);
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

        private class Plane : Raster<Seat>
        {
            public Plane(int cols, int rows)
                : base(cols, rows) => Do.Nothing();

            public int Occupied => this.Count(seat => seat.Occupied);

            public Seat this[Point p] => this[p.X, p.Y];

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (var col = 0; col < Cols; col++)
                {
                    for (var row = 0; row < Rows; row++)
                    {
                        var seat = this[col, row];
                        if (seat is null)
                        {
                            sb.Append('.');
                        }
                        else
                        {
                            sb.Append(seat.Occupied ? '#' : 'L');
                        }
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }

            protected override Seat Create(int index, int col, int row, int neighbors)
                => new Seat(index, col, row, neighbors);

            public static Plane Parse(string input)
            {
                var lines = input.Lines().ToArray();
                var plane = new Plane(lines.Length, lines[0].Length);

                for (var row = 0; row < plane.Rows; row++)
                {
                    for (var col = 0; col < plane.Cols; col++)
                    {
                        var seat = plane[col, row];
                        seat.Changed = true;
                        seat.Occupied = lines[col][row] == '#';
                        if (lines[col][row] == '.')
                        {
                            plane.Remove(seat);
                        }
                    }
                }

                var extra = new[] { new Point(-1, -1), new Point(-1, +1), new Point(+1, -1), new Point(+1, +1), };

                foreach (var seat in plane)
                {
                    foreach (var delta in extra)
                    {
                        var row = seat.Row + delta.X;
                        var col = seat.Col + delta.Y;

                        if (row >= 0 && row < plane.Rows &&
                            col >= 0 && col < plane.Cols)
                        {
                            var neighbor = plane[col, row];
                            if (neighbor != null)
                            {
                                seat.Neighbors.Add(neighbor);
                            }
                        }
                    }
                }
                return plane;
            }
        }
        private class Seat : RasterTile<Seat>
        {
            public Seat(int index, int col, int row, int neighbors)
                : base(index, col, row, neighbors + 4) => Do.Nothing();

            public bool Occupied { get; set; }
            public bool Empty => !Occupied;
            public bool Next { get; set; }
            public bool Changed { get; set; }

            public bool GetNext()
                => Empty
                ? Neighbors.All(n => n.Empty)
                : Neighbors.Count(n => n.Occupied) < 4;

            public bool GetNextPartTwo(Plane plane)
            {
                var occupied_neighbors = 0;
                var empty_neighbors = 0;
                var position = new Point(Col, Row);

                foreach (var direction in directions)
                {
                    var neighbor = position + direction;
                    var found = false;
                    while (!found &&
                        neighbor.X >= 0 && neighbor.X < plane.Cols &&
                        neighbor.Y >= 0 && neighbor.Y < plane.Rows)
                    {
                        var test = plane[neighbor];

                        found = test != null;
                        if (found)
                        {
                            occupied_neighbors += test.Occupied ? 1 : 0;
                            empty_neighbors += test.Empty ? 1 : 0;
                        }
                        neighbor += direction;
                    }
                }
                return Empty
                    ? occupied_neighbors == 0
                    : occupied_neighbors < 5;
            }
            private static readonly Vector[] directions = new[]
            {
                new Vector(-1, -1),
                new Vector(-1, +0), 
                new Vector(-1, +1),
                new Vector(+0, -1),
                new Vector(+0, +1), 
                new Vector(+1, -1), 
                new Vector(+1, +0),
                new Vector(+1, +1),
            };

            public override string ToString() => $"[{Col:00}, {Row:00}] Occupied: {Occupied} ({Next})";
        }
    }
}