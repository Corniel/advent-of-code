namespace Advent_of_Code_2023;

[Category(Category.Computation)]
public class Day_04
{
    [Example(answer: 13, Example._1)]
    [Puzzle(answer: 28750, O.μs10)]
    public int part_one(Inputs<Card> cards) => cards.Sum(c => c.Points);

    [Example(answer: 30, Example._1)]
    [Puzzle(answer: 10212704, O.μs100)]
    public int part_two(Lines lines)
    {
        var cards = lines.ToArray(Card.Parse);
        for (var i = 0; i < cards.Length; i++)
        {
            var card = cards[i];

            for (var c = i + 1; c <= i + card.Matches && c < cards.Length; c++)
            {
                cards[c].Total += card.Total;
            }
        }
        return cards.Sum(c => c.Total);
    }

    public record Card(int[] Winning, int[] Deck)
    {
        public int Total = 1;
        public int Matches => Deck.Intersect(Winning).Count();
        public int Points => (1 << Matches) >> 1;

        public static Card Parse(string line)
        {
            var split = line.Split(':', '|');
            return new([..split[1].Int32s()], [..split[2].Int32s()]);
        }
    }
}
