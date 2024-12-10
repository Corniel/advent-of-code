namespace Advent_of_Code_2017;

[Category(Category.VectorAlgebra)]
public class Day_11
{
    [Example(answer: 3, "ne,ne,ne")]
    [Example(answer: 0, "ne,ne,sw,sw")]
    [Example(answer: 2, "ne,ne,s,s")]
    [Example(answer: 3, "se,sw,se,sw,sw")]
    [Puzzle(answer: 743, O.μs100)]
    public int part_one(string str) => Process(str, _ => 0);

    [Puzzle(answer: 1493, O.μs100)]
    public int part_two(string str) => Process(str, d => d.Sum());

    int Process(string str, Func<int[], int> maximum)
    {
        var max = 0; var steps = new int[6];

        foreach (var dir in str.CommaSeparated(Dirs.IndexOf))
        {
            var dis = ++steps[dir];

            if (steps[(dir + 3) % 6] >= dis)
            {
                steps[dir] = 0;
                steps[(dir + 3) % 6] -= dis;
            }
            else
            {
                var cw = Math.Min(dis, steps[(dir + 2) % 6]);
                steps[dir] -= cw;
                steps[(dir + 2) % 6] -= cw;
                steps[(dir + 1) % 6] += cw;
                var cc = Math.Min(dis, steps[(dir + 4) % 6]);
                steps[dir] -= cc;
                steps[(dir + 4) % 6] -= cc;
                steps[(dir + 5) % 6] += cc;
            }
            max = Math.Max(max, maximum(steps));
        }
        return Math.Max(max, steps.Sum());
    }

    readonly string[] Dirs = ["n", "ne", "se", "s", "sw", "nw"];
}
