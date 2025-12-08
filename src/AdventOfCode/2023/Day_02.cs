namespace Advent_of_Code_2023;

[Category(Category.ExpressionParsing)]
public class Day_02
{
    [Example(answer: 8, Example._1)]
    [Puzzle(answer: 2551, O.μs10)]
    public int part_one(Inputs<Game> games) => games.Where(g => g.Hands.TrueForAll(h => h.Fits)).Sum(g => g.Id);

    [Example(answer: 2286, Example._1)]
    [Puzzle(answer: 62811, O.μs10)]
    public int part_two(Inputs<Game> games) => games.As(g => g.Smallest.Pow).Sum();

    public record Game(int Id, Hand[] Hands)
    {
        public Hand Smallest => new(Hands.Max(h => h.R), Hands.Max(h => h.G), Hands.Max(h => h.B));

        public static Game Parse(string line) => new(
            line.Int32(),
            [..line[line.IndexOf(':')..].Separate(';').Select(Hand.Parse)]);
    }

    public readonly record struct Hand(int R, int G, int B)
    {
        public int Pow => R * G * B;

        public bool Fits => 12 >= R && 13 >= G && 14 >= B;

        public static Hand Parse(string str)
        {
            var parts = str.Separate(',');
            return new(
                parts.Find(p => p.EndsWith("red"))?.Int32() ?? 0,
                parts.Find(p => p.EndsWith("green"))?.Int32() ?? 0,
                parts.Find(p => p.EndsWith("blue"))?.Int32() ?? 0);
        }
    }
}
