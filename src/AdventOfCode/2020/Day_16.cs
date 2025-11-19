namespace Advent_of_Code_2020;

[Category(Category.Simulation)]
public class Day_16
{
    [Example(answer: 71, Example._1)]
    [Puzzle(answer: 27850, O.μs100)]
    public int part_one(GroupedLines groups)
    {
        var rules = groups[0].Select(Rule.Parse).ToArray();
        var nearby = groups[2][1..].Select(Ticket.Parse).SelectMany(n => n.Parts).ToArray();
        return nearby.Where(number => !rules.Exists(rule => rule.Valid(number))).Sum();
    }

    [Puzzle(answer: 491924517533, O.μs100)]
    public long part_two(GroupedLines groups)
    {
        var rules = groups[0].Select(Rule.Parse).ToArray();
        var tickets = new List<Ticket> { Ticket.Parse(groups[1][1]) };
        tickets.AddRange(groups[2][1..].Select(Ticket.Parse).Where(ticket => ticket.Valid(rules)));
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

    record Option(Rule Rule, List<int> Positions)
    {
        public int Position => Positions[0];
        public bool HasSingle => Positions.Count == 1;
        public bool HasMultiple => Positions.Count > 1;
        public static Option New(Rule rule, IEnumerable<Ticket> tickets)
            => new(rule, [.. Range(0, 20).Where(option => tickets.All(ticket => rule.Valid(ticket.Parts[option])))]);
    }
    record Rule(string Name, Int32Range[] Ranges)
    {
        public bool Valid(int part) => Ranges.Exists(range => range.Contains(part));
        public bool Valid(Ticket ticket) => ticket.Parts.TrueForAll(Valid);
        public static Rule Parse(string str)
        {
            var split = str.Separate(':');
            return new Rule(split[0], [.. split[1].Separate("or").Select(Int32Range.Parse)]);
        }
    }

    record Ticket(int[] Parts)
    {
        public bool Valid(IEnumerable<Rule> rules) => rules.Any(rule => rule.Valid(this));
        public static Ticket Parse(string str) => new([.. str.Int32s()]);
    }
}
