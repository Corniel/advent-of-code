namespace Advent_of_Code_2022;

[Category(Category.VectorAlgebra)]
public class Day_15
{
    [Example(answer: 26, null, 10, Example._1)]
    [Puzzle(answer: 4665948, null, 2_000_000, O.μs)]
    public int part_one(Lines lines, int y) => lines.As(Instruction.Parse).Select(i => i.Range(y)).Merge().Sum(r => r.Size) - 1;

    [Example(answer: 56000011, null, 20, Example._1)]
    [Puzzle(answer: 13543690671045, null, 4_000_000, O.s)]
    public long part_two(Lines lines, int max)
    {
        var instr = lines.As(Instruction.Parse).ToArray();

        for (var y = 0; y <= max; y++)
        {
            if (instr.Select(i => i.Range(y)).Merge() is { Count: 2 } ranges)
            {
                long x = ranges[0].Upper + 1;
                return x * 4_000_000 + y;
            }
        }
        throw new NoAnswer();
    }

    record Instruction(Point Sensor, Point Beacon, int Dinstance)
    {
        public Int32Range Range(int y)
            => (Sensor.Y - y).Abs() is { } dy && dy <= Dinstance
            ? new(Sensor.X - Dinstance + dy, Sensor.X + Dinstance - dy)
            : Int32Range.Empty;

        public static Instruction Parse(string line)
        {
            var ns = line.Int32s().ToArray();
            var s = new Point(ns[0], ns[1]);
            var b = new Point(ns[2], ns[3]);
            return new(s, b, s.ManhattanDistance(b));
        }
    }
}
