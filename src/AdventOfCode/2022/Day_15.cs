namespace Advent_of_Code_2022;

[Category(Category.sec, Category.VectorAlgebra)]
public class Day_15
{
    [Example(answer: 26, 1)]
    public int example_one(string input) => One(input, 10);

    [Puzzle(answer: 4665948)]
    public int part_one(string input) =>  One(input, 2_000_000);

    [Example(answer: 56000011, 1)]
    public long example_two(string input) => Two(input, 20);

    [Puzzle(answer: 13543690671045)]
    public long part_two(string input) => Two(input, 4_000_000);

    private static int One(string input, int y)  => input.Lines(Instruction.Parse).Select(i => i.Range(y)).Merge().Sum(r => r.Size);

    private static long Two(string input, int max)
    {
        var instr = input.Lines(Instruction.Parse).ToArray();
        var line = new Int32Range(0, max);

        for (var y = 0; y <= max; y++)
        {
            if (instr.Select(i => i.Range(y).Intersection(line)).Merge() is { Count: 2 } ranges)
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
