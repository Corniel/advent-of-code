namespace Advent_of_Code_2022;

[Category(Category.μs, Category.ExpressionParsing)]
public class Day_07
{
    [Example(answer: 95437, 1)]
    [Puzzle(answer: 1667443)]
    public long part_one(string input) => Dir.Parse(input).AllDirs().Where(d => d.Size <= 100_000).Sum(d => d.Size);

    [Example(answer: 24933642, 1)]
    [Puzzle(answer: 8998590)]
    public long part_two(string input)
    {
        var root = Dir.Parse(input);
        var required = root.Size - 40_000_000;
        return root.AllDirs().Where(d => d.Size >= required).Select(d => d.Size).Min();
    }

    record File(string Name, long Size);
    
    record Dir(Dir Parent, string Name, List<Dir> Dirs, List<File> Files)
    {
        public long Size => AllDirs().Sum(f => f.Files.Sum(f => f.Size));

        public IEnumerable<Dir> AllDirs() => Dirs.SelectMany(dir => dir.AllDirs()).Concat(Repeat(this, 1));

        public static Dir Parse(string input)
        {
            var root = new Dir(null, "/", new(), new());
            var current = root;

            foreach (var line in input.Lines().Skip(1))
            {
                if (line == "$ ls") { /* scan dir */ }
                else if (line == "$ /") current = root;
                else if (line == "$ cd ..") current = current.Parent;
                else if (line.StartsWith("$ cd ")) current = current.Dirs.First(dir => dir.Name == line[5..]);
                else if (line.StartsWith("dir ")) current.Dirs.Add(new(current, line[4..], new(), new()));
                else current.Files.Add(new(line.SpaceSeparated()[^1], line.SpaceSeparated()[0].Int32()));
            }
            return root;
        }
    }
}
