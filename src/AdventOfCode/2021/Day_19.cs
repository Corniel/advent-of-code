namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra, Category._3D)]
public class Day_19
{
    [Example(answer: 79, Example._1)]
    [Puzzle(answer: 403, O.s)]
    public int part_one(GroupedLines groups) => Run(groups, out _).Count;

    [Example(answer: 3621, Example._1)]
    [Puzzle(answer: 10569, O.s)]
    public int part_two(GroupedLines groups)
    {
        Run(groups, out var scanners);
        return Range(0, scanners.Length)
            .SelectMany(f => Range(0, scanners.Length).Select(s => new { f, s }))
            .Max(p => scanners[p.f].Position.ManhattanDistance(scanners[p.s].Position));
    }

    static HashSet<Point3D> Run(GroupedLines groups, out Scanner[] scanners)
    {
        scanners = groups.Select(Scanner.Parse).ToArray();
        var beacons = new HashSet<Point3D>(scanners[0].Beacons(Point3D.O, Orientation.All[0]));
        var queue = new Queue<Scanner>();
        queue.EnqueueRange(scanners[1..]);

        while (queue.NotEmpty())
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
            foreach (var beacon in beacons)
            {
                foreach (var orientation in Orientation.All)
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
                => !Beacons(location, orientation).Where(c => !beacons.Contains(c)).Skip(Observations.Length - 12).NotEmpty();
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
        static Orientation[] Init()
        {
            int[] numbers = [0, 1, 2];
            var all = new List<Orientation>(48);
            foreach (var order in numbers.Permutations())
            {
                for (var bits = 0; bits < 8; bits++)
                {
                    all.Add(new([.. order], [(bits & 1) == 0 ? +1 : -1, (bits & 2) == 0 ? +1 : -1, (bits & 4) == 0 ? +1 : -1]));
                }
            }
            return [.. all];
        }
    }
}
