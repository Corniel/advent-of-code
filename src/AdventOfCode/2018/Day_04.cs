namespace Advent_of_Code_2018;

[Category(Category.Simulation)]
public class Day_04
{
    [Example(answer: 10 * 24, Example._1)]
    [Puzzle(answer: 26281)]
    public long part_one(string input) => Occurences(input)
        .OrderByDescending(c => c.Value.Total).Select(Score).First();

    [Example(answer: 99 * 45, Example._1)]
    [Puzzle(answer: 73001, O.s)]
    public long part_two(string input) => Occurences(input)
        .OrderByDescending(c => AsleepMostMinutes(c.Value)).Select(Score).First();

    static long AsleepMostMinutes(ItemCounter<int> o) => o.OrderByDescending(kvp => kvp.Count).First().Count;
    static int MinuteAsleepMost(ItemCounter<int> o) => o.OrderByDescending(kvp => kvp.Count).Select(kvp => kvp.Item).First();
    static long Score(KeyValuePair<int, ItemCounter<int>> o) => o.Key* MinuteAsleepMost(o.Value);

    static Dictionary<int, ItemCounter<int>> Occurences(string input)
    {
        var instructions = input.Lines(Log.Parse).OrderBy(r => r.Timestamp).ToArray();
        var occurences = new Dictionary<int, ItemCounter<int>>();
        var id = -1;
        var last = DateTime.MinValue;
        foreach (var instruction in instructions)
        {
            if (instruction.Action == Action.Begins) id = instruction.Id;
            else if (instruction.Action == Action.Sleeps) last = instruction.Timestamp;
            else
            {
                var sleeps = occurences.GetOrCreate(id, () => new());
                while (last < instruction.Timestamp)
                {
                    sleeps[last.Minute]++;
                    last += TimeSpan.FromMinutes(1);
                }
            }
        }
        return occurences;
    }

    record Log(DateTime Timestamp, string Text)
    {
        public Action Action => Text[0..5] switch
        {
            "Guard" => Action.Begins,
            "falls" => Action.Sleeps,
            _ => Action.WakesUp,
        };
        public int Id => Text.Int32();
        public static Log Parse(string str) => new(DateTime.Parse(str[1..17]), str[19..]);
    }
    enum Action { Begins, Sleeps, WakesUp }
}


