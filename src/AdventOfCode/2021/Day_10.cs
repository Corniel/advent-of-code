namespace Advent_of_Code_2021;

[Category(Category.ExpressionParsing)]
public class Day_10
{
    const string Open = "([{<";
    const string Close = ")]}>";

    [Example(answer: 0, "[[(<{}>)]][()]")]
    [Example(answer: 1197, "{([(<{}[<>[]}>{[]{[(<()>")]
    [Example(answer: 3, "[[<[([]))<([[{}[[()]]]")]
    [Example(answer: 57, "[{[{({}]{}}([{[{{{}}([]")]
    [Example(answer: 3, "[<(<(<(<{}))><([]([]()")]
    [Example(answer: 25137, "<{([([[(<>()){}]>(<<{{")]
    [Puzzle(answer: 411471, O.μs10)]
    public int part_one(Lines lines) => lines.As(Parse).Select(One).Sum();

    [Example(answer: 288957, "[({(<(())[]>[[{[]{<()<>>")]
    [Puzzle(answer: 3122628974L, O.μs10)]
    public long part_two(Lines lines)
    {
        var scores = lines.As(Parse).Select(Two).Where(sc => sc != 0).ToList();
        scores.Sort();
        return scores[scores.Count / 2];
    }

    static int One(string str) => str switch { ")" => 3, "]" => 57, "}" => 1197, ">" => 25137, _ => 0 };

    static long Two(string str)
    {
        long score = 0;
        foreach (var ch in str.Reverse()) score = score * 5 + Open.IndexOf(ch) + 1;
        return score;
    }

    static string Parse(string line)
    {
        var chars = new char[line.Length];
        var pos = -1;
        foreach (var ch in line)
        {
            if (Open.IndexOf(ch) != -1) { chars[++pos] = ch; }
            else if (pos != -1 && chars[pos] == Open[Close.IndexOf(ch)]) { pos--; }
            else return ch.ToString();
        }
        return new string(chars, 0, pos + 1);
    }
}
