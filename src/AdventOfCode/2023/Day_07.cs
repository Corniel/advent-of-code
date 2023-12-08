namespace Advent_of_Code_2023;

[Category(Category.ExpressionParsing)]
public class Day_07
{
    [Example(answer: 6440, "32T3K 765;T55J5 684;KK677 28;KTJJT 220;QQQJA 483")]
    [Puzzle(answer: 249748283, O.μs100)]
    public int part_one(string str) => Sort(str);

    [Example(answer: 5905, "32T3K 765;T55J5 684;KK677 28;KTJJT 220;QQQJA 483")]
    [Puzzle(answer: 248029057, O.μs100)]
    public int part_two(string str) => Sort(str.Replace('J', '*'));

    static int Sort(string str) => str.Lines(Hand.Parse).Order().Select((c, i) => (i + 1) * c.Bid).Sum();

    readonly struct Hand : IComparable<Hand>
    {
        public readonly int Bid;
        readonly int Value;

        public Hand(string cards, int bid)
        {
            Bid = bid;
            var groups = Goups(cards);

            Value = (int)(groups.Length switch
            {
                1 => HandType.FiveOfKind,
                2 => groups[0] == 4 ? HandType.FourOfKind : HandType.FullHouse,
                3 => groups[0] == 2 ? HandType.TwoPair : HandType.ThreeOfKind,
                4 => HandType.OnePair,
                _ => default
            });

            foreach (var ch in cards)
            {
                Value = (Value << 4) | Order.IndexOf(ch);
            }
        }

        static int[] Goups(string cards)
        {
            if (cards.Replace("*", "") is { Length: > 0 } noJoker)
            {
                var groups = Order
                    .Select(o => noJoker.Count(o))
                    .Where(g => g != 0)
                    .OrderDescending().ToArray();

                groups[0] += 5 - noJoker.Length;
                return groups;
            }
            else return [5];
        }

        public int CompareTo(Hand other) => Value.CompareTo(other.Value);

        public static Hand Parse(string line) => new(line[0..5], line[6..].Int32());
    }

    enum HandType { HighCard, OnePair, TwoPair, ThreeOfKind, FullHouse, FourOfKind, FiveOfKind };

    const string Order = "*23456789TJQKA";
}
