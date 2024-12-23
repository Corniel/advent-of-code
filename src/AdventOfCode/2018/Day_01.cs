namespace Advent_of_Code_2018;

[Category(Category.SequenceProgression)]
public class Day_01
{
    [Example(answer: 3, "+1, +1, +1")]
    [Example(answer: 0, "+1, +1, -2")]
    [Puzzle(answer: 538, O.ns100)]
    public int part_one(Ints numbers) => numbers.Sum();

    [Example(answer: 0, "+1, -1")]
    [Example(answer: 10, "+3, +3, +4, -2, -4")]
    [Example(answer: 14, "+7, +7, -2, -7, -4")]
    [Puzzle(answer: 77271, O.ms)]
    public int part_two(Ints numbers)
    {
        HashSet<int> frequencies = [0];
        var current = 0;
        while (true)
        {
            foreach (var f in numbers)
            {
                current += f;
                if (!frequencies.Add(current)) return current;
            }
        }
        throw new InfiniteLoop();
    }
}
