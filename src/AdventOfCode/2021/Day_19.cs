namespace Advent_of_Code_2021;

public class Day_19
{
    [Example(answer: 79, year: 2021, day: 19, example: 1)]
    [Puzzle(answer: 403, year: 2021, day: 19)]
    public int part_one(string input) => Run(input, out _).Count;

    [Example(answer: 3621, year: 2021, day: 19, example: 1)]
    [Puzzle(answer: 10569, year: 2021, day: 19)]
    public int part_two(string input)
    {
        Run(input, out var scanners);
        return Enumerable.Range(0, scanners.Length)
            .SelectMany(f => Enumerable.Range(0, scanners.Length).Select(s => new { f, s }))
            .Max(p => scanners[p.f].Position.ManhattanDistance(scanners[p.s].Position));
    }

    private static ISet<Point3D> Run(string input, out Scanner[] scanners)
    {
        scanners = input.GroupedLines().Select(Scanner.Parse).ToArray();
        var beacons = new HashSet<Point3D>(scanners[0].Beacons(Point3D.O, Orientation.All[0]));
        var queue = new Queue<Scanner>();
        queue.EnqueueRange(scanners[1..]);

        while (queue.Any())
        {
            var scanner = queue.Dequeue();
            if (!beacons.AddRange(scanner.Locate(beacons)))
            {
                queue.Enqueue(scanner);
            }
        }
        return beacons;
    }

    class Scanner
    {
        public Point3D Position { get; private set; }
        public Vector3D[] Observations { get; init; }

        public IEnumerable<Point3D> Beacons(Point3D pos, Orientation orientation)
             => Observations.Select(o => pos + orientation.Transform(o));
        public IEnumerable<Point3D> Locate(HashSet<Point3D> beacons)
        {
            foreach (var orientation in Orientation.All)
            {
                foreach (var beacon in beacons)
                {
                    foreach (var location in Observations.Select(o => beacon - orientation.Transform(o)))
                    {
                        if (IsCandidate(location, orientation, beacons))
                        {
                            Position = location;
                            return Beacons(location, orientation);
                        }
                    }
                }
            }
            return Array.Empty<Point3D>();

            bool IsCandidate(Point3D location, Orientation orientation, HashSet<Point3D> beacons)
                => !Beacons(location, orientation).Where(c => !beacons.Contains(c)).Skip(Observations.Length - 12).Any();
        }
        public static Scanner Parse(string[] lines) => new() { Observations = lines[1..].Select(Vector3D.Parse).ToArray() };
    }
    record Orientation(int[] Order, int[] Multiplier)
    {
        public Vector3D Transform(Vector3D vector) => new(
            x: vector[Order[0]] * Multiplier[0],
            y: vector[Order[1]] * Multiplier[1],
            z: vector[Order[2]] * Multiplier[2]);
        public static readonly Orientation[] All = Init();
        private static Orientation[] Init()
        {
            var all = new List<Orientation>(48);
            foreach (var order in (new int[] { 0, 1, 2 }).Permutations())
            {
                for (var bits = 0; bits < 8; bits++)
                {
                    all.Add(new(order.ToArray(), new int[] { (bits & 1) == 0 ? +1 : -1, (bits & 2) == 0 ? +1 : -1, (bits & 4) == 0 ? +1 : -1 }));
                }
            }
            return all.ToArray();
        }
    }
}
