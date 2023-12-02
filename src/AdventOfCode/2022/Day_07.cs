namespace Advent_of_Code_2022;

[Category(Category.ExpressionParsing)]
public class Day_07
{
    [Example(answer: 95437, Example._1)]
    [Puzzle(answer: 1667443, O.μs100)]
    public int part_one(Lines lines) => Dir.Parse(lines).All.Where(d => d.Size <= 100_000).Sum(d => d.Size);

    [Example(answer: 24933642, Example._1)]
    [Puzzle(answer: 8998590, O.μs100)]
    public int part_two(Lines lines)
    {
        var root = Dir.Parse(lines);
        var required = root.Size - 40_000_000;
        return root.All.Where(d => d.Size >= required).Select(d => d.Size).Min();
    }

    record Dir(Dir Parent, string Name, List<Dir> Dirs)
    {
        public int Files { get; set; }
        public int Size => All.Sum(f => f.Files);
        public IEnumerable<Dir> All => Dirs.SelectMany(dir => dir.All).Concat(Repeat(this, 1));
        public static Dir Parse(Lines lines)
        {
            var root = new Dir(null, "/", []);
            var current = root;

            foreach (var line in lines.Skip(1))
            {
                if (line == "$ cd ..") current = current.Parent;
                else if (line.StartsWith("$ cd ")) current = current.Dirs.First(dir => dir.Name == line[5..]);
                else if (line.StartsWith("dir ")) current.Dirs.Add(new(current, line[4..], []));
                else current.Files += line.Int32();
            }
            return root;
        }
    }
}
