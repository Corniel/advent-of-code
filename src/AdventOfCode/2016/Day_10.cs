namespace Advent_of_Code_2016;

/// <summary>
/// There is a list bots that process low and high values that result in output.
///
/// Part one: Which bot processes 17 and 61.
/// Part two: The product of the first 3 outputs.
/// </summary>
[Category(Category.ExpressionParsing)]
public class Day_10
{
    [Puzzle(answer: 47, O.μs)]
    public int part_one(Inputs<object> inputs) => Exe(inputs, 17, 61);

    [Puzzle(answer: 2666, O.μs)]
    public int part_two(Inputs<object> inputs) => Exe(inputs, -1, -1);

    private static int Exe(Inputs<object> inputs, int lo, int hi)
    {
        var bots = array(inputs.OfType<Bot>().OrderBy(b => b.Id));
        var outp = new int[32];

        foreach (var i in inputs.OfType<Value>()) bots[i.Id].Add(i.Val);

        while (bots.FirstOrDefault(b => b is { Todo: true, Count: 2 }) is { } bot)
        {
            bot.Sort();
            if (bot[0] == lo && bot[1] == hi) return bot.Id;
            if (bot.Lo < 0) outp[-bot.Lo] = bot[0];
            else bots[bot.Lo].Add(bot[0]);
            if (bot.Hi < 0) outp[-bot.Hi] = bot[1];
            else bots[bot.Hi].Add(bot[1]);
            bot.Todo = false;
        }
        return outp[1] * outp[2] * outp[31];
    }

    // minus for output, -31 for output 0
    public static object Parse(string line)
        => line[0] is 'v'
        ? Ctor.New<Value>(line.Int32s())
        : Ctor.New<Bot>(line.Replace("output ", "-").Replace("-0", "-31").Int32s());

    record Value(int Val, int Id);

    class Bot(int id, int lo, int hi) : List<int>
    {
        public int Id { get; } = id;
        public int Lo { get; } = lo;
        public int Hi { get; } = hi;
        public bool Todo { get; set; } = true;
    }
}
