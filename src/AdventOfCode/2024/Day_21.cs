namespace Advent_of_Code_2024;

[Category(Category.Computation, Category.PathFinding)]
public partial class Day_21
{
    [Example(answer: 126384, "029A\n980A\n179A\n456A\n379A")]
    [Example(answer: 224326, "208A\n540A\n685A\n879A\n826A")]
    [Puzzle(answer: 109758L, "319A\n085A\n143A\n286A\n789A", O.μs)]
    public long part_one(Lines lines) => Solve(lines, 2);

    [Example(answer: 279638326609472, "208A\n540A\n685A\n879A\n826A")]
    [Puzzle(answer: 0134341709499296, "319A\n085A\n143A\n286A\n789A", O.μs10)]
    public long part_two(Lines lines) => Solve(lines, 25);

    long Solve(Lines lines, int level)
    {
        // Otherwise the benchmark will not test anything.
        Sizes = [.. Range(0, level + 1).Select(_ => new Dictionary<string, long>())];
        return lines.As(c => Solve(c, level)).Sum();
    }

    long Solve(string code, int level) => code.Int32() * A(Path(code)).SelectWithPrevious().Sum(ft => Size(ft, level));

    string Path(string code) => string.Concat(A(code).SelectWithPrevious().Select(Num));

    long Size(string ft, int level)
    {
        if (Sizes[level].TryGetValue(ft, out var size)) return size;
        return Sizes[level][ft] = level == 1
            ? Key(ft).Length
            : A(Key(ft)).SelectWithPrevious().Sum(ft_ => Size(ft_, level - 1));
    }
    
    string Num(string ft) => Paths.TryGetValue(ft, out var path) ? path : Paths[ft] = Move(ft, Nums);

    string Key(string ft) => Paths.TryGetValue(ft, out var path) ? path : Paths[ft] = Move(ft, Keys);

    static string Move(string ft, Dictionary<char, Point> pad)
    {
        var (dc, dr) = pad[ft[1]] - pad[ft[0]];

        // <v^> is the order of moving.
        return D('<', -dc) + D('v', +dr) + D('^', -dr) + D('>', +dc) + 'A';

        string D(char dir, int size) => new(dir, Math.Max(0, size));
    }

    /// <remarks>
    /// +---+---+---+
    /// | 7 | 8 | 9 |
    /// +---+---+---+
    /// | 4 | 5 | 6 |
    /// +---+---+---+
    /// | 1 | 2 | 3 |
    /// +---+---+---+
    ///   * | 0 | A |
    ///     +---+---+
    /// </remarks>
    readonly Dictionary<char, Point> Nums = "789\n456\n123\n*0A".CharPixels().ToDictionary(px => px.Value, kvp => kvp.Key);

    /// <remarks>
    ///     +---+---+
    ///   * | ^ | A |
    /// +---+---+---+
    /// | < | v | > |
    /// +---+---+---+
    /// </remarks>
    readonly Dictionary<char, Point> Keys = "*^A\n<v>".CharPixels().ToDictionary((px) => px.Value, px => px.Key);

    /// <summary>Contains Both num as key paths.</summary>
    /// <remarks>
    /// Differences due to off track constraints are predefined.
    /// </remarks>
    readonly Dictionary<string, string> Paths = new()
    {
        // Nums
        ["01"] = "^<A",  ["04"] = "^^<A",  ["07"] = "^^^<A",
        ["10"] = ">vA",  ["40"] = ">vvA",  ["70"] = ">vvvA",
        ["1A"] = ">>vA", ["4A"] = ">>vvA", ["7A"] = ">>vvvA",
        ["A1"] = "^<<A", ["A4"] = "^^<<A", ["A7"] = "^^^<<A",

        // Keys
        ["^<"] = "v<A", ["<^"] = ">^A", ["<A"] = ">>^A", ["A<"] = "v<<A",
    };

    Dictionary<string, long>[] Sizes = [];

    /// <summary>Prefix with A as that is the start button.</summary>
    static string A(string s) => 'A' + s;
}
