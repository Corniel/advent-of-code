namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_22
{
    [Puzzle(answer: 644257, O.μs100)]
    public long part_one(string input) => Count(input.Lines(Instruction.Parse).Take(20));

    [Puzzle(answer: 1235484513229032, O.ms10)]
    public long part_two(string input) => Count(input.Lines(Instruction.Parse));

    private static long Count(IEnumerable<Instruction> instructions)
    {
        var cubes = new ItemCounter<Cube>();
        var buffer = new ItemCounter<Cube>();

        foreach (var instruction in instructions)
        {
            buffer.Clear();
            foreach (var record in cubes)
            {
                if (record.Item.Intersect(instruction.Cube, out var intersection))
                {
                    buffer[intersection] -= record.Count;
                }
            }
            foreach (var buffered in buffer)
            {
                cubes[buffered.Item] += buffered.Count;
            }
            cubes[instruction.Cube] += instruction.IsOn ? 1 : 0;
        }
        return cubes.Sum(record => record.Item.Count * record.Count);
    }

    record Instruction(bool IsOn, Cube Cube)
    {
        public static Instruction Parse(string line)
        {
            var numbers = line.Int32s().ToArray();
            return new(line.StartsWith("on"), new(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5]));
        }
    }
    readonly struct Cube : IEquatable<Cube>
    {
        public Cube(int x_min, int x_max, int y_min, int y_max, int z_min, int z_max)
        {
            Xmin = x_min; Xmax = x_max;
            Ymin = y_min; Ymax = y_max;
            Zmin = z_min; Zmax = z_max;
        }
        public readonly int Xmin; public readonly int Xmax;
        public readonly int Ymin; public readonly int Ymax;
        public readonly int Zmin; public readonly int Zmax;
        public bool IsEmpty => Xmin > Xmax || Ymin > Ymax || Zmin > Zmax;
        public long Count => IsEmpty ? 0 : 1L * (1 + Xmax - Xmin) * (1 + Ymax - Ymin) * (1 + Zmax - Zmin);

        public bool Intersect(Cube other, out Cube intersection)
        {
            intersection  = new(
                Math.Max(Xmin, other.Xmin), Math.Min(Xmax, other.Xmax),
                Math.Max(Ymin, other.Ymin), Math.Min(Ymax, other.Ymax),
                Math.Max(Zmin, other.Zmin), Math.Min(Zmax, other.Zmax));
            return !intersection.IsEmpty;
        }
        public override bool Equals(object obj) => obj is Cube other && Equals(other);
        public bool Equals(Cube other)
            => Xmin == other.Xmin
            && Xmax == other.Xmax
            && Ymin == other.Ymin
            && Ymax == other.Ymax
            && Zmin == other.Zmin
            && Zmax == other.Zmax;
        public override int GetHashCode()
            => Xmin ^ (Xmax << 5)
            ^ (Ymin << 10) ^ (Ymax << 15)
            ^ (Zmin << 20) ^ (Xmax << 24);
    }
}
