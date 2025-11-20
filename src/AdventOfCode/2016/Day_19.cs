using System.Numerics;

namespace Advent_of_Code_2016;

[Category(Category.SequenceProgression)]
public class Day_19
{
    // See https://youtu.be/uCsD3ZGzMgE.
    [Example(answer: 3, 5)]
    [Puzzle(answer: 1834903, 3014603, O.ns10)]
    public long part_one(int input)
        =>  1 + 2 * (input - (1 << BitOperations.Log2((uint)input)));

    // My default loop was slow, but gave the right answer
    // I put in  the most ellegant formula I could find: https://www.reddit.com/r/adventofcode/comments/5j4lp1/2016_day_19_solutions
    [Example(answer: 2, 5)]
    [Puzzle(answer: 1420280, 3014603, O.ns10)]
    public int part_two(int input)
    {
        var p = 3.Pow((int)(Math.Log(input) / Math.Log(3)));
        return p switch
        {
            _ when p == input => p,
            _ when input - p <= p => input - p,
            _ => 2 * input - 3 * p,
        };
    }
}
