namespace Advent_of_Code_2020;

[Category(Category.VectorAlgebra)]
public class Day_03
{
    [Example(answer: 7, Example._1)]
    [Puzzle(answer: 220, O.μs10)]
    public long part_one(Lines input)
        => CountTrees(Row.Parse(input).ToArray(), new Vector(3, 1));

    [Puzzle(answer: 2138320800, O.μs10)]
    public long part_two(Lines input)
    {
        var rows = Row.Parse(input).ToArray();
        return new Vector[] { new(1, 1), new(3, 1), new(5, 1), new(7, 1), new(1, 2) }.Select(slope => CountTrees(rows, slope)).Product();
    }

    static long CountTrees(Row[] rows, Vector slope)
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

    class Row
    {
        private const byte Tree = 17;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly byte[] squares;

        private Row(byte[] sqs) => squares = sqs;

        public int Size => squares.Length;

        public override string ToString() => string.Concat(squares.Select(tree => tree == Tree ? '#' : '.'));

        public bool IsTree(Point point) => squares[point.X.Mod(Size)] == Tree;

        public static IEnumerable<Row> Parse(Lines str) => str.As(AsRow);

        static Row AsRow(string line)
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
