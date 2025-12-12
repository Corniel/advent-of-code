namespace Advent_of_Code_2023;

[Category(Category.Grid)]
public class Day_18
{
    [Example(answer: 62, Example._1)]
    [Puzzle(answer: 62500L, O.μs100)]
    public long part_one(Lines lines) => Count(lines.As(Dig.One));

    [Example(answer: 952408144115, Example._1)]
    [Puzzle(answer: 122109860712709, O.μs100)]
    public long part_two(Lines lines) => Count(lines.As(Dig.Two));

    static long Count(IEnumerable<Dig> digs)
    {
        var lines = Lines(digs);
        var total = lines.Values.Sum(gr => gr.Sum(r => r.Size));

        foreach (var p in lines.SelectWithPrevious())
        {
            var dt = p.Current.Key - p.Previous.Key - 1;
            var ln = p.Current.Value.Sum(r => r.Size);
            total += dt * ln;
        }
        return total;
    }

    static Dictionary<long, Int64Ranges> Lines(IEnumerable<Dig> digs)
    {
        var horizontal = Line.Horizontals(digs).Fix();
        var fills = new Dictionary<long, Int64Ranges>();
        var filled = Int64Ranges.Empty;

        foreach (var y in horizontal.Select(l => l.Y).Distinct().Order())
        {
            var prev = filled;
            fills.TryAdd(y - 1, prev);

            foreach (var line in horizontal.Where(h => h.Y == y).Select(l => l.X))
            {
                if (filled.FirstOrDefault(range => range.FullyContains(line)) is { IsEmpty: false } container)
                {
                    var except = line;
                    if (container.Lower < except.Lower) except = new(except.Lower + 1, except.Upper);
                    if (container.Upper > except.Upper) except = new(except.Lower, except.Upper - 1);
                    filled = filled.Except(except);
                }
                else
                {
                    filled = filled.Merge(line);
                }
            }
            fills[y] = filled.Merge(prev);
            fills.Add(y + 1, filled);
        }
        return fills;
    }

    record Dig(Vector Dir, int Dist)
    {
        public static Dig One(string s) => new(V(s[0]), s.Int32());

        public static Dig Two(string s) => new(V(s[^2]), Convert.ToInt32(s[^7..^2], 16));

        static Vector V(char v) => v switch { '0' => Vector.E, '1' => Vector.S, '2' => Vector.W, '3' => Vector.N, _ => Parse.Dir(v) };
    };

    record struct Line(int Y, Int64Range X)
    {
        public static IEnumerable<Line> Horizontals(IEnumerable<Dig> digs)
        {
            var prev = Point.O;

            foreach (var dig in digs)
            {
                var curr = prev + (dig.Dir * dig.Dist);
                if (dig.Dir.IsHorizontal) yield return new(curr.Y, new(Math.Min(curr.X, prev.X), Math.Max(curr.X, prev.X)));
                prev = curr;
            }
        }
    }
}
