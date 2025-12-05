namespace Advent_of_Code_2015;

[Category(Category.SequenceProgression)]
public class Day_20
{
    [Example(answer: 8, 150)]
    [Puzzle(answer: 786240, 34000000, O.ms10)]
    public int part_one(long number) => Solve(number, 10, int.MaxValue);

    [Puzzle(answer: 831600, 34000000, O.ms10)]
    public int part_two(long number) => Solve(number, 11, 50);

    static int Solve(long number, int factor, int visits)
    {
        var houses = new long[1_000_000];
        for (var f = 1; f < houses.Length; f++)
        {
            var mp = f; var visit = 0;
            while (mp < houses.Length && visit++ < visits)
            {
                houses[mp] += f * factor;
                mp += f;
            }
        }
        return houses.Index().First(h => h.Item >= number).Index;
    }
}
