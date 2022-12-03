namespace Advent_of_Code_2015;

[Category(Category.Simulation)]
public class Day_16
{
    static readonly Sue Aunt = new(0)
    {
        { Compount.children, 3 },
        { Compount.cats, 7 },
        { Compount.samoyeds, 2 },
        { Compount.pomeranians, 3 },
        { Compount.akitas, 0 },
        { Compount.vizslas, 0 },
        { Compount.goldfish, 5 },
        { Compount.trees, 3 },
        { Compount.cars, 2 },
        { Compount.perfumes, 1 },
    };

    [Puzzle(answer: 103)]
    public int part_one(string input) => input.Lines(Sue.Parse).Single(sue => sue.Matches1(Aunt)).Id;

    [Puzzle(answer: 405)]
    public long part_two(string input) => input.Lines(Sue.Parse).Single(sue => sue.Matches2(Aunt)).Id;

    class Sue: Dictionary<Compount, int>
    {
        public Sue(int id) => Id = id;
        public int Id { get; }

        public bool Matches1(Sue aunt) => this.All(kvp => aunt[kvp.Key] == kvp.Value);
        public bool Matches2(Sue aunt) => this.All(kvp => Matches2(kvp, aunt));
        bool Matches2(KeyValuePair<Compount, int> kvp, Sue aunt)
            => kvp.Key switch
            {
                Compount.cats or Compount.trees => kvp.Value > aunt[kvp.Key],
                Compount.pomeranians or Compount.goldfish => kvp.Value < aunt[kvp.Key],
                _ => kvp.Value == aunt[kvp.Key],
            };
        public static Sue Parse(string line)
        {
            line = line[4..];
            var first = line.IndexOf(':');
            var id = line[..first].Int32();
            var sue = new Sue(id);
            foreach (var block in line[(first + 2)..].CommaSeparated())
            {
                var split = block.Separate(": ");
                sue[Enum.Parse<Compount>(split[0])] = split[1].Int32();
            }
            return sue;
        }
    }

    enum Compount { children, cats, samoyeds, pomeranians, akitas, vizslas, goldfish, trees, cars, perfumes }
}
