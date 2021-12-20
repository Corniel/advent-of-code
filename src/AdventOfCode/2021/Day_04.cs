namespace Advent_of_Code_2021;

[Category(Category.Simulation)]
public class Day_04
{
    private const string Example = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";

    [Example(answer: 4512, Example)]
    [Puzzle(answer: 27027, year: 2021, day: 04)]
    public long part_one(string input)
    {
        var game = Game.Parse(input);
        foreach (var number in game.Numbers)
        {
            if (game.Play(number) is { } bingo) return bingo.Score * number;
        }
        throw new NoAnswer();
    }

    [Example(answer: 1924, Example)]
    [Puzzle(answer: 36975, year: 2021, day: 04)]
    public long part_two(string input)
    {
        var game = Game.Parse(input);
        foreach (var number in game.Numbers)
        {
            if (game.Play(number) is { } bingo && !game.Cards.Any())
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

        public static Game Parse(string input)
        {
            var groups = input.GroupedLines().ToArray();
            return new(groups.Skip(1).Select(Card.Parse).ToList(), groups[0][0].Int32s().ToArray());
        }
    }

    public sealed record Card(int[] Numbers)
    {
        public bool Bingo => Enumerable.Range(0, 5).Any(i => Row(i) || Col(i));
        public int Score => Numbers.Sum();

        public void Play(int number)
        {
            var index = Array.IndexOf(Numbers, number);
            if (index != -1) Numbers[index] = 0;
        }
        private bool Row(int r) => Numbers.Skip(r * 5).Take(5).All(n => n == 0);
        private bool Col(int c) => Numbers.Skip(c).WithStep(5).Take(5).All(n => n == 0);
        public static Card Parse(string[] lines)  => new(string.Join(" ", lines).Int32s().ToArray());
    }
}
