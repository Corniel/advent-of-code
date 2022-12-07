namespace Advent_of_Code_2022;

[Category(Category.ms, Category.ExpressionParsing)]
public class Day_07
{
    [Example(answer: 95437, 1)]
    [Puzzle(answer: 1667443)]
    public int part_one(string input) => Dir.Parse(input).All.Where(d => d.Size <= 100_000).Sum(d => d.Size);

    [Example(answer: 24933642, 1)]
    [Puzzle(answer: 8998590)]
    public int part_two(string input)
    {
        var root = Dir.Parse(input);
        var required = root.Size - 40_000_000;
        return root.All.Where(d => d.Size >= required).Select(d => d.Size).Min();
    }

    record Dir(Dir Parent, string Name, List<Dir> Dirs)
    {
        public int Files { get; set; }
        public int Size => All.Sum(f => f.Files);
        public IEnumerable<Dir> All => Dirs.SelectMany(dir => dir.All).Concat(Repeat(this, 1));
        public static Dir Parse(string input)
        {
            var root = new Dir(null, "/", new());
            var current = root;

            foreach (var line in input.Lines().Skip(1))
            {
                if (line == "$ cd ..") current = current.Parent;
                else if (line.StartsWith("$ cd ")) current = current.Dirs.First(dir => dir.Name == line[5..]);
                else if (line.StartsWith("dir ")) current.Dirs.Add(new(current, line[4..], new()));
                else current.Files += line.Int32s().FirstOrDefault();
            }
            return root;
        }
    }
}
