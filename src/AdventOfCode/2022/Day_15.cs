namespace Advent_of_Code_2022;

[Category(Category.VectorAlgebra)]
public class Day_15
{
    [Example(answer: 26, null, 10, Example._1)]
    [Puzzle(answer: 4665948, null, 2_000_000, O.ns100)]
    public int part_one(Point2Ds points, int y) => points.ChunkBy(2).Select(Instruction.New).Select(i => i.Range(y)).Merge().Sum(r => r.Size) - 1;

    [Example(answer: 56000011, null, 20, Example._1)]
    [Puzzle(answer: 13543690671045, null, 4_000_000, O.s)]
    public long part_two(Point2Ds points, int max)
    {
        var instr = points.ChunkBy(2).Select(Instruction.New).Fix();

        for (var y = 0; y <= max; y++)
            if (instr.As(i => i.Range(y)).Merge() is { Count: 2 } ranges)
                return (ranges[0].Upper + 1L) * 4_000_000 + y;

        throw new NoAnswer();
    }

    record Instruction(Point Sensor, Point Beacon, int Dinstance)
    {
        public Int32Range Range(int y)
            => (Sensor.Y - y).Abs() is { } dy && dy <= Dinstance
            ? new(Sensor.X - Dinstance + dy, Sensor.X + Dinstance - dy)
            : Int32Range.Empty;

        public static Instruction New(ImmutableArray<Point> pair)
        {
            var (s, b) = (pair[0], pair[1]);
            return new(s, b, s.ManhattanDistance(b));
        }
    }
}
