namespace Advent_of_Code_2021;

[Category(Category.Simulation)]
public class Day_04
{
    [Example(answer: 4512, Example._1)]
    [Puzzle(answer: 27027, O.ms)]
    public int part_one(GroupedLines groups)
    {
        var game = Game.Parse(groups);
        foreach (var number in game.Numbers)
        {
            if (game.Play(number) is { } bingo) return bingo.Score * number;
        }
        throw new NoAnswer();
    }

    [Example(answer: 1924, Example._1)]
    [Puzzle(answer: 36975, O.ms)]
    public int part_two(GroupedLines groups)
    {
        var game = Game.Parse(groups);
        foreach (var number in game.Numbers)
        {
            if (game.Play(number) is { } bingo && !game.Cards.NotEmpty())
            {
                return bingo.Score * number;
            }
        }
        throw new NoAnswer();
    }

    public sealed record Game(List<Card> Cards, int[] Numbers)
    {
        public Card Play(int number)
        {
            foreach (var card in Cards)
            {
                card.Play(number);
            }
            var bingos = Cards.Where(card => card.Bingo).ToArray();
            foreach (var bingo in bingos)
            {
                Cards.Remove(bingo);
            }
            return bingos.FirstOrDefault();
        }

        public static Game Parse(GroupedLines groups) => new(groups.Skip(1).Select(Card.Parse).ToList(), groups[0][0].Int32s().ToArray());
    }

    public sealed record Card(int[] Numbers)
    {
        public bool Bingo => Range(0, 5).Any(i => Row(i) || Col(i));
        public int Score => Numbers.Sum();

        public void Play(int number)
        {
            var index = Numbers.IndexOf(number);
            if (index != -1) Numbers[index] = 0;
        }
        private bool Row(int r) => Numbers.Skip(r * 5).Take(5).All(n => n == 0);
        private bool Col(int c) => Numbers.Skip(c).WithStep(5).Take(5).All(n => n == 0);
        public static Card Parse(string[] lines) => new(string.Join(" ", lines).Int32s().ToArray());
    }
}
