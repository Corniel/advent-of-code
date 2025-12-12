namespace Advent_of_Code_2018;

[Category(Category.Sorting)]
public class Day_07
{
    [Example(answer: "CABDFE", Example._1)]
    [Puzzle(answer: "ACHOQRXSEKUGMYIWDZLNBFTJVP", O.μs)]
    public char[] part_one(Lines lines)
    {
        var read = Read(lines); var rules = read.rules; var chs = read.chs;
        var sorted = new char[chs.Count];

        while (chs.Count > 0)
        {
            var first = chs.First(ch => !rules.Any(r => r.After == ch));
            sorted[^chs.Count] = first;
            chs.Remove(first);
            rules.RemoveAll(r => r.Before == first);
        }
        return sorted;
    }

    [Example(answer: 15, null, 2, 00, Example._1)]
    [Puzzle(answer: 985, null, 5, 60, O.μs100)]
    public int part_two(Lines lines, int workerCount, int duration)
    {
        var queue = new Queue<State>();
        var workers = new int[workerCount];
        var read = Read(lines); var rules = read.rules; var chs = read.chs;
        var time = 0;

        while (chs.Count > 0 || workers.Any(w => w > time))
        {
            foreach (var state in queue.DequeueCurrent())
            {
                if (state.Time > time) queue.Enqueue(state);
                else rules.RemoveAll(r => r.Before == state.Ch);
            }

            foreach (var ch in chs.Where(c => !rules.Any(r => r.After == c)))
            {
                for (var w = 0; w < workerCount; w++)
                {
                    if (workers[w] <= time)
                    {
                        workers[w] = ch - 'A' + 1 + duration + time;
                        chs.Remove(ch);
                        queue.Enqueue(new(ch, workers[w]));
                        break;
                    }
                }
            }
            time++;
        }
        return time;
    }

    static (List<Rule> rules, HashSet<char> chs) Read(Lines lines)
    {
        var rules = lines.As(l => new Rule(l[5], l[36])).ToList();
        var chs = rules.SelectMany(r => new char[] { r.Before, r.After }).Order().ToHashSet();
        return (rules, chs);
    }

    readonly record struct Rule(char Before, char After);

    readonly record struct State(char Ch, int Time);
}
