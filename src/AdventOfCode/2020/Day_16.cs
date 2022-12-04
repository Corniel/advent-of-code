namespace Advent_of_Code_2020;

[Category(Category.Simulation)]
public class Day_16
{
    [Example(answer: 71, example: 1)]
    [Puzzle(answer: 27850)]
    public int part_one(string input)
    {
        var blocks = input.GroupedLines().ToArray();
        var rules = blocks[0].Select(Rule.Parse).ToArray();
        var nearby = blocks[2][1..].Select(Ticket.Parse).SelectMany(n => n.Parts).ToArray();
        return nearby.Where(number => !rules.Any(rule => rule.Valid(number))).Sum();
    }

    [Puzzle(answer: 491924517533)]
    public long part_two(string input)
    {
        var blocks = input.GroupedLines().ToArray();
        var rules = blocks[0].Select(Rule.Parse).ToArray();
        var tickets = new List<Ticket> { Ticket.Parse(blocks[1][1]) };
        tickets.AddRange(blocks[2][1..].Select(Ticket.Parse).Where(ticket => ticket.Valid(rules)));
        var options = rules.Select(rule => Option.New(rule, tickets)).ToArray();

        while (options.Take(6).Any(option => option.HasMultiple))
        {
            foreach (var option in options.Where(option => option.HasSingle))
            {
                foreach (var other in options.Where(o => option != o))
                {
                    other.Positions.Remove(option.Position);
                }
            }
        }

        var yours = tickets[0];
        return options.Take(6).Select(option => (long)yours.Parts[option.Position]).Product();

    }

    private record Option(Rule Rule, List<int> Positions)
    {
        public int Position => Positions[0];
        public bool HasSingle => Positions.Count == 1;
        public bool HasMultiple => Positions.Count > 1;
        public static Option New(Rule rule, IEnumerable<Ticket> tickets)
        {
            var positions = Enumerable.Range(0, 20).Where(option => tickets.All(ticket => rule.Valid(ticket.Parts[option])));
            return new Option(rule, positions.ToList());
        }
    }
    private record Rule(string Name, Int32Range[] Ranges)
    {
        public bool Valid(int part) => Ranges.Any(range => range.Contains(part));
        public bool Valid(Ticket ticket) => ticket.Parts.All(part => Valid(part));
        public static Rule Parse(string str)
        {
            var split = str.Separate(':');
            return new Rule(split[0], split[1].Separate("or").Select(Int32Range.Parse).ToArray());
        }
    }

    private record Ticket(int[] Parts)
    {
        public bool Valid(IEnumerable<Rule> rules) => rules.Any(rule => rule.Valid(this));
        public static Ticket Parse(string str) => new Ticket(str.Int32s().ToArray());
    }
}
