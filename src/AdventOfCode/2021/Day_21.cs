namespace Advent_of_Code_2021;

[Category(Category.Computation)]
public class Day_21
{
    [Example(answer: 739785, "Player 1 starting position: 4;Player 2 starting position: 8")]
    [Puzzle(answer: 597600, input: "Player 1 starting position: 8;Player 2 starting position: 5")]
    public int part_one(string input)
    {
        var numbers = input.Int32s().ToArray();
        var scores = new int[2];
        var position = new int[] { numbers[1], numbers[3] };
        var dice = 0;
        var turns = 0;
        while (scores.All(n => n < 1000))
        {
            for (var step = 0; step < 3; step++)
            {
                dice = Modulo100[dice + 1];
                position[turns & 1] = Modulo10[position[turns & 1] + dice];
            }
            scores[turns & 1] += position[turns++ & 1];
        }
        return scores.Min() * turns * 3;
    }

    [Example(answer: 444356092776315, "Player 1 starting position: 4;Player 2 starting position: 8")]
    [Puzzle(answer: 634769613696613, input: "Player 1 starting position: 8;Player 2 starting position: 5")]
    public long part_two(string input)
    {
        var numbers = input.Int32s().ToArray();
        var universe = Jagged.Array<ItemCounter<Point>>(2, 11, 11);
        var player = 0;
        long active = 1;
        var wins = new long[2];

        foreach (var setup in Setups)
        {
            universe[0][setup.X][setup.Y] = new();
            universe[1][setup.X][setup.Y] = new();
        }
        universe[player][numbers[1]][numbers[3]][default] += 1;

        while (active > 0)
        {
            foreach (var setup in Setups)
            {
                foreach (var standing in universe[player][setup.X][setup.Y])
                {
                    for (var dice = 3; dice <= 9; dice++)
                    {
                        var position = Modulo10[setup[player] + dice];
                        var score = Score(standing.Item, player, position);
                        var occurences = standing.Count * Weight[dice];
                        if (score[player] >= 21)
                        {
                            wins[player] += occurences;
                        }
                        else if (player == 0)
                        {
                            universe[1][position][setup.Y][score] += occurences;
                            active += occurences;
                        }
                        else
                        {
                            universe[0][setup.X][position][score] += occurences;
                            active += occurences;
                        }
                    }
                    active -= standing.Count;
                }
                universe[player][setup.X][setup.Y].Clear();
            }
            player = player == 0 ? 1 : 0;
        }
        return wins.Max();
    }

    static Point Score(Point points, int player, int add) => player == 0 ? new(points.X + add, points.Y) : new Point(points.X, points.Y + add);

    static readonly int[] Modulo10 = Enumerable.Range(0, 110).Select(n => ((n - 1) % 10) + 1).ToArray();
    static readonly int[] Modulo100 = Enumerable.Range(0, 200).Select(n => ((n - 1) % 100) + 1).ToArray();
    static readonly int[] Weight = new int[] { 0, 0, 0, 1, 3, 6, 7, 6, 3, 1 };
    static readonly Point[] Setups = Points.Range(new Point(1, 1), new Point(10, 10)).ToArray();
}
