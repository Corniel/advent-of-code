namespace Advent_of_Code_2020;

public class Day_03
{
    [Example(answer: 7, year: 2020, day: 03, example: 1)]
    [Puzzle(answer: 220, year: 2020, day: 03)]
    public long part_one(string input)
        => CountTrees(Row.Parse(input).ToArray(), new Vector(3, 1));

    [Puzzle(answer: 2138320800, year: 2020, day: 03)]
    public long part_two(string input)
    {
        var rows = Row.Parse(input).ToArray();
        var slopes = new[]
          {
                new Vector(1, 1),
                new Vector(3, 1),
                new Vector(5, 1),
                new Vector(7, 1),
                new Vector(1, 2),
            };
        return slopes.Select(slope => CountTrees(rows, slope)).Product();
    }

    private static long CountTrees(Row[] rows, Vector slope)
    {
        var position = Point.O + slope;
        var trees = 0;
        while (position.Y < rows.Length)
        {
            var row = rows[position.Y];
            trees += row.IsTree(position) ? 1 : 0;
            position += slope;
        }
        return trees;
    }

    private class Row
    {
        private const byte Tree = 17;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly byte[] squares;

        private Row(byte[] sqs) => squares = sqs;

        public int Size => squares.Length;

        public override string ToString() => string.Concat(squares.Select(tree => tree == Tree ? '#' : '.'));

        public bool IsTree(Point point) => squares[point.X.Mod(Size)] == Tree;

        public static IEnumerable<Row> Parse(string str)
            => str.Lines(AsRow);

        private static Row AsRow(string line)
        {
            var sqs = new byte[line.Length];
            for (var p = 0; p < sqs.Length; p++)
            {
                if (line[p] == '#') sqs[p] = Tree;
            }
            return new Row(sqs);
        }
    }
}
