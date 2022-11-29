namespace Advent_of_Code_2020;

[Category(Category.Grid)]
public class Day_11
{
    [Example(answer: 37, example: 1)]
    [Puzzle(answer: 2481)]
    public int part_one(string input)
        => Plane.Init(input, Plane.DirectNeighbors).Simulate(4);


    [Example(answer: 26, example: 1)]
    [Puzzle(answer: 2227)]
    public long part_two(string input)
        => Plane.Init(input, Plane.InSightNeighbors).Simulate(5);

    private class Plane : Grid<Seat>
    {
        public Plane(int cols, int rows) : base(cols, rows) => Do.Nothing();

        public int Simulate(int occupied)
        {
            while (Tiles.Any(seat => seat.Changed))
            {
                foreach (var seat in Tiles) { seat.Changed = false; }
                foreach (var seat in Tiles)
                {
                    var next = seat.GetNext(occupied);
                    seat.Changed = next != seat.Occupied;
                    seat.Next = next;
                }
                foreach (var seat in Tiles) { seat.Occupied = seat.Next; }
            }
            return Tiles.Count(seat => seat.Occupied);
        }

        public static Plane Init(string input, GridNeighbors<Seat> neighbors)
        {
            var chars = input.CharPixels();
            var plane = new Plane(chars.Cols, chars.Rows);
            var seats = chars.Where(p => p.Value == 'L').Select(p => p.Key);

            plane.InitTiles(
                locations: seats,
                ctor: (id, location) => new Seat(id, location),
                neighbors: neighbors);
            return plane;
        }

        public static IEnumerable<Point> DirectNeighbors(Grid<Seat> plane, Point seat)
           => seat.Projections(directions).Where(p => plane.OnGrid(p));

        public static IEnumerable<Point> InSightNeighbors(Grid<Seat> plane, Point seat)
            => directions.Select(dir => seat
                .Repeat(dir)
                .TakeWhile(seat => plane.OnGrid(seat))
                .Select(seat => plane[seat])
                .FirstOrDefault(seat => seat is not null))
            .Where(seat => seat is not null)
            .Select(seat => seat.Location);

        private static readonly Vector[] directions = CompassPoints.All.Select(p => p.ToVector()).ToArray();
    }
    private class Seat : GridTile<Seat>
    {
        public Seat(int id, Point location)
            : base(id, location, 8) => Do.Nothing();

        public bool Occupied { get; set; }
        public bool Empty => !Occupied;
        public bool Next { get; set; }
        public bool Changed { get; set; } = true;

        public SimpleList<Seat> InSight { get; } = new SimpleList<Seat>(8);

        public bool GetNext(int occupied)
            => Empty
            ? Neighbors.All(n => n.Empty)
            : Neighbors.Count(n => n.Occupied) < occupied;

        public override string ToString() => $"[{Col:00}, {Row:00}] Occupied: {Occupied} ({Next})";
    }
}
