namespace Advent_of_Code_2021;

public class Day_12
{
    [Example(answer: 10, "start-A;start-b;A-c;A-b;b-d;A-end;b-end")]
    [Puzzle(answer: 3485, year: 2021, day: 12)]
    public int part_one(string input) => Run(input, SmallCavesOnce);
  
    [Example(answer: 36, "start-A;start-b;A-c;A-b;b-d;A-end;b-end")]
    [Example(answer: 103, "dc-end;HN-start;start-kj;dc-start;dc-HN;LN-dc;HN-end;kj-sa;kj-HN;kj-dc")]
    [Example(answer: 3509, "fs-end;he-DX;fs-he;start-DX;pj-DX;end-zg;zg-sl;zg-pj;pj-he;RW-he;fs-DX;pj-RW;zg-RW;start-pj;he-WI;zg-he;pj-fs;start-RW")]
    [Puzzle(answer: 85062, year: 2021, day: 12)]
    public int part_two(string input) => Run(input, OnSmallCaveTwice);

    static bool SmallCavesOnce(Cave cave, IEnumerable<Cave> path) => cave.IsSmall && path.Contains(cave);

    static bool OnSmallCaveTwice(Cave cave, IEnumerable<Cave> path)
    {
        if (!cave.IsSmall) return false;
        Unique.Clear();
        var caveTwice = false;
        var containsAddition = false;

        foreach (var prev in path.Skip(1).Where(t => t.IsSmall))
        {
            if (prev == cave)
            {
                if (caveTwice || containsAddition) return true;
                else { containsAddition = true; }
            }
            else if(!Unique.Add(prev))
            {
                if (caveTwice || containsAddition) return true;
                else { caveTwice = true; }
            }
        }
        return false;
    }
    static readonly HashSet<Cave> Unique = new();

    private static int Run(string input, Func<Cave, IEnumerable<Cave>, bool> invalidPath)
    {
        var paths = 0;
        var stack = new Stack<List<Cave>>();
        stack.Push(new List<Cave> { Start(input) });

        while (stack.Any())
        {
            var path = stack.Pop();

            foreach (var cave in path[^1].Neighbors)
            {
                if (cave.IsEnd) { paths++; }
                else if (!invalidPath(cave, path))
                {
                    var copy = path.ToList();
                    copy.Add(cave);
                    stack.Push(copy);
                }
            }
        }
        return paths;
    }

    static Cave Start(string input)
    {
        var start = new Cave("start");
        var caves = new[] { start, new Cave("end") }.ToDictionary(cave => cave.Name, cave => cave);

        foreach (var parts in input.Lines(line => line.Seperate("-")))
        {
            if (!caves.TryGetValue(parts[0], out var from))
            {
                from = new Cave(parts[0]);
                caves[from.Name] = from;
            }
            if (!caves.TryGetValue(parts[1], out var to))
            {
                to = new Cave(parts[1]);
                caves[to.Name] = to;
            }
            if (to != start) { from.Neighbors.Add(to); }
            if (from != start) { to.Neighbors.Add(from); }
        }
        return start;
    }

    class Cave
    {
        public Cave(string name)
        {
            Name = name;
            IsSmall = char.IsLower(Name[0]);
            IsEnd = Name == "end";
        }
        public string Name { get; }
        public bool IsSmall { get; }
        public bool IsEnd { get; }
        public HashSet<Cave> Neighbors { get; } = new();
        public override string ToString() => Name;
    }
}
