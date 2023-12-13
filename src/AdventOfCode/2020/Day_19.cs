namespace Advent_of_Code_2020;

[Category(Category.ExpressionParsing)]
public class Day_19
{
    [Example(answer: 2, Example._1)]
    [Puzzle(answer: 239, O.ms)]
    public int part_one(GroupedLines groups) => Patterns.Parse(groups[0]).Matches(groups[1]);

    [Example(answer: 12, Example._2)]
    [Puzzle(answer: 405, O.ms)]
    public int part_two(GroupedLines groups)
    {
        var patterns = Patterns.Parse(groups[0]);
        patterns[08] = $"({patterns[42]})+";
        patterns[11] = $"(?<special>{patterns[42]})+(?<-special>{patterns[31]})+(?(special)(?!))";
        return patterns.Matches(groups[1]);
    }
    class Patterns : Dictionary<int, object>
    {
        public int Matches(IEnumerable<string> messages)
        {
            var regex = new Regex($"^{this[0]}$");
            return messages.Count(regex.IsMatch);
        }
        public static Patterns Parse(IEnumerable<string> lines)
        {
            var patterns = new Patterns();
            foreach (var line in lines)
            {
                var split = line.Separate(':');
                var id = split[0].Int32();
                var pattern = split[1];

                if (pattern[1] == 'a' || pattern[1] == 'b') { patterns[id] = pattern[1]; }
                else
                {
                    var piped = pattern.Separate('|');
                    patterns[id] = piped.Length == 1
                        ? Combined.Parse(pattern, patterns)
                        : new Or(Combined.Parse(piped[0], patterns), Combined.Parse(piped[1], patterns));
                }
            }
            return patterns;
        }
    }
    private record Reference(int Id, Patterns Patterns) { public override string ToString() => $"{Patterns[Id]}"; }
    private record Or(object Left, object Right) { public override string ToString() => $"({Left}|{Right})"; }
    private record Combined(object[] Sequance)
    {
        public override string ToString() => string.Concat(Sequance.Select(s => s.ToString()));
        public static Combined Parse(string str, Patterns patterns)
            => new(str.SpaceSeparated(r => new Reference(r.Int32(), patterns)).ToArray());
    }
}
