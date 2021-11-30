namespace Advent_of_Code_2020;

public class Day_22
{
    [Example(answer: 306, "Player 1:\n9\n2\n6\n3\n1\n\nPlayer 2:\n5\n8\n4\n7\n10")]
    [Puzzle(answer: 33403, year: 2020, day: 22)]
    public int part_one(string input)
    {
        var decks = input.GroupedLines().Select(Deck.Parse).ToArray();
        return Deck.Play(decks[0], decks[1]);
    }

    [Example(answer: 105, "Player 1:\n43\n19\n\nPlayer 2:\n2\n29\n14")]
    [Example(answer: 291, "Player 1:\n9\n2\n6\n3\n1\n\nPlayer 2:\n5\n8\n4\n7\n10")]
    [Puzzle(answer: 29177, year: 2020, day: 22)]
    public long part_two(string input)
    {
        var decks = input.GroupedLines().Select(Deck.Parse).ToArray();
        return Deck.RecursivePlay(decks[0], decks[1]);
    }

    public class Deck : Queue<int>
    {
        public Deck(IEnumerable<int> cards) : base(cards) => Do.Nothing();
        public bool HasAny => Count != 0;
        public int Draw() => Dequeue();
        public void Add(int card0, int card1)
        {
            Enqueue(card0);
            Enqueue(card1);
        }
        public int Score
        {
            get
            {
                var score = 0;
                var factor = Count;
                foreach (var card in this)
                {
                    score += factor-- * card;
                }
                return score;
            }
        }
        public Deck Copy(int cards) => new Deck(this.Take(cards));
        public override string ToString() => string.Join(',', this);
        public static Deck Parse(string[] str) => new Deck(str.Skip(1).Select(NumberParsing.Int32));

        public static int Play(Deck player1, Deck player2)
            => Player1Wins(player1, player2) ? player1.Score : player2.Score;

        public static int RecursivePlay(Deck player1, Deck player2)
            => Players1WinsRecursive(player1, player2) ? player1.Score : player2.Score;

        private static bool Player1Wins(Deck player1, Deck player2)
        {
            while (player1.HasAny && player2.HasAny)
            {
                var card1 = player1.Draw();
                var card2 = player2.Draw();
                if (card1 > card2) { player1.Add(card1, card2); }
                else { player2.Add(card2, card1); }
            }
            return player2.Count == 0;
        }

        public static bool Players1WinsRecursive(Deck player1, Deck player2)
        {
            var states = new HashSet<string>();
            while (player1.HasAny && player2.HasAny)
            {
                if (!states.Add($"{player1}&{player2}")) { return true; }

                var card1 = player1.Draw();
                var card2 = player2.Draw();
                var p1Wins = player1.Count >= card1 && player2.Count >= card2
                    ? Players1WinsRecursive(player1.Copy(card1), player2.Copy(card2))
                    : card1 > card2;

                if (p1Wins) { player1.Add(card1, card2); }
                else { player2.Add(card2, card1); }
            }
            return player2.Count == 0;
        }
    }
}
