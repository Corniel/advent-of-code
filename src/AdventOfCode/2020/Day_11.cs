namespace Advent_of_Code_2020;

[Category(Category.Grid)]
public class Day_11
{
    [Example(answer: 37, Example._1)]
    [Puzzle(answer: 2481, O.ms10)]
    public int part_one(CharPixels chars)
        => Plane.Init(chars, Plane.DirectNeighbors).Simulate(4);

    [Example(answer: 26, Example._1)]
    [Puzzle(answer: 2227, O.ms10)]
    public int part_two(CharPixels chars)
        => Plane.Init(chars, Plane.InSightNeighbors).Simulate(5);

    class Plane : Grid<Seat>
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

        public static Plane Init(CharPixels chars, GridNeighbors<Seat> neighbors)
        {
            var plane = new Plane(chars.Cols, chars.Rows);
            var seats = chars.Where(p => p.Value == 'L').Select(p => p.Key);

            plane.InitTiles(
                locations: seats,
                ctor: (id, location) => new Seat(id, location),
                neighbors: neighbors);
            return plane;
        }

        public static IEnumerable<Point> DirectNeighbors(Grid<Seat> plane, Point seat)
           => seat.Projections(directions).Where(plane.OnGrid);

        public static IEnumerable<Point> InSightNeighbors(Grid<Seat> plane, Point seat)
            => directions.Select(dir => seat
                .Repeat(dir)
                .TakeWhile(seat => plane.OnGrid(seat))
                .Select(seat => plane[seat])
                .FirstOrDefault(seat => seat is not null))
            .Where(seat => seat is not null)
            .Select(seat => seat.Location);

        static readonly Vector[] directions = CompassPoints.All.Select(p => p.ToVector()).ToArray();
    }
    class Seat : GridTile<Seat>
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
