namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_22
{
    [Puzzle(answer: 644257L, O.μs100)]
    public long part_one(Inputs<Instr> input) => Count(input[..20]);

    [Puzzle(answer: 1235484513229032, O.ms10)]
    public long part_two(Inputs<Instr> input) => Count(input);

    static long Count(IEnumerable<Instr> instructions)
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
        return cubes.Sum(record => record.Item.Points * record.Count);
    }

    public record Instr(bool IsOn, Cube Cube)
    {
        public static Instr Parse(string line)
        {
            int[] ns = [.. line.Int32s()];
            return new(line.StartsWith("on"), new(ns[0], ns[1], ns[2], ns[3], ns[4], ns[5]));
        }
    }

    public readonly struct Cube(int x_min, int x_max, int y_min, int y_max, int z_min, int z_max) : IEquatable<Cube>
    {
        public readonly int Xmin = x_min; public readonly int Xmax = x_max;
        public readonly int Ymin = y_min; public readonly int Ymax = y_max;
        public readonly int Zmin = z_min; public readonly int Zmax = z_max;
        public bool IsEmpty => Xmin > Xmax || Ymin > Ymax || Zmin > Zmax;
        public long Points => IsEmpty ? 0 : 1L * (1 + Xmax - Xmin) * (1 + Ymax - Ymin) * (1 + Zmax - Zmin);

        public bool Intersect(Cube other, out Cube intersection)
        {
            intersection = new(
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
