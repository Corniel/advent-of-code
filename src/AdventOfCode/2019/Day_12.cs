namespace Advent_of_Code_2019;

[Category(Category.VectorAlgebra)]
public class Day_12
{
    [Example(answer: 263, @"
        <x=-1, y=0, z=2>
        <x=2, y=-10, z=-7>
        <x=4, y=-8, z=8>
        <x=3, y=5, z=-1>")]
    [Example(answer: 7262, @"
        <x=-8, y=-10, z=0>
        <x=5, y=5, z=10>
        <x=2, y=-7, z=3>
        <x=9, y=-8, z=-3")]
    [Puzzle(answer: 56202, @"
        <x=17, y=-9, z=4>
        <x=2, y=2, z=-13>
        <x=-1, y=5, z=-1>
        <x=4, y=7, z=-7>", O.μs100)]
    public int part_one(Lines lines)
    {
        var moons = lines.ToArray(Moon.Parse);

        for (var step = 1; step <= 10_000; step++)
        {
            Moon.SetStep(moons);
        }
        return moons.Sum(moon => moon.TotalEnergy);
    }

    [Example(answer: 2772, @"
        <x=-1, y=0, z=2>
        <x=2, y=-10, z=-7>
        <x=4, y=-8, z=8>
        <x=3, y=5, z=-1>")]
    [Example(answer: 4686774924L, @"
        <x=-8, y=-10, z=0>
        <x=5, y=5, z=10>
        <x=2, y=-7, z=3>
        <x=9, y=-8, z=-3")]
    [Puzzle(answer: 537881600740876L, @"
        <x=17, y=-9, z=4>
        <x=2, y=2, z=-13>
        <x=-1, y=5, z=-1>
        <x=4, y=7, z=-7>", O.ms10)]
    public long part_two(Lines lines)
    {
        var moons = lines.ToArray(Moon.Parse);
        return Maths.Lcm([
            Cycle(moons.Select(moon => Pair(moon.Position.X, default)).ToArray()),
            Cycle(moons.Select(moon => Pair(moon.Position.Y, default)).ToArray()),
            Cycle(moons.Select(moon => Pair(moon.Position.Z, default)).ToArray())]);
    }

    static long Cycle((int, int)[] pairs)
    {
        var initial = pairs.ToArray();
        var steps = 0;

        do
        {
            Step(pairs);
            steps++;
        }
        while (!initial.SequenceEqual(pairs));

        return steps;
    }

    static void Step((int, int)[] pairs)
    {
        var deltas = new int[pairs.Length];

        for (var i = 0; i < pairs.Length; i++)
        {
            foreach (var other in pairs)
            {
                deltas[i] += Math.Sign(other.Item1 - pairs[i].Item1);
            }
        }

        for (var i = 0; i < pairs.Length; i++)
        {
            var velocity = deltas[i] + pairs[i].Item2;
            pairs[i] = new(pairs[i].Item1 + velocity, velocity);
        }
    }

    static (int, int) Pair(int l, int r) => (l, r);

    sealed class Moon(Point3D position, Vector3D velocity)
    {
        public Point3D Position { get; set; } = position;
        public Vector3D Velocity { get; set; } = velocity;

        public int TotalEnergy => PotentialEnergy * KeneticEnergy;

        public int PotentialEnergy => Position.X.Abs() + Position.Y.Abs() + Position.Z.Abs();

        public int KeneticEnergy => Velocity.X.Abs() + Velocity.Y.Abs() + Velocity.Z.Abs();

        public static Moon Parse(string line) => new(Ctor.New<Point3D>(line.Int32s()), default);

        public static void SetStep(IEnumerable<Moon> moons)
        {
            foreach (var moon in moons)
            {
                foreach (var other in moons)
                {
                    var x = (other.Position.X - moon.Position.X).Sign();
                    var y = (other.Position.Y - moon.Position.Y).Sign();
                    var z = (other.Position.Z - moon.Position.Z).Sign();
                    moon.Velocity = moon.Velocity.Adjust(x, y, z);
                }
            }
            foreach (var moon in moons)
            {
                moon.Position += moon.Velocity;
            }
        }
    }
}
