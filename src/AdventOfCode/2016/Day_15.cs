namespace Advent_of_Code_2016;

[Category(Category.SequenceProgression)]
public class Day_15
{
    [Example(answer: 5, "Disc #1 has 5 positions; at time=0, it is at position 4.\nDisc #2 has 2 positions; at time=0, it is at position 1.")]
    [Puzzle(answer: 121834, O.ns100)]
    public int part_one(Lines lines) => lines.As(Modulo).Sum().Value;

    [Puzzle(answer: 3208099, O.ns100)]
    public int part_two(Lines lines)
    {
        var moduli = lines.As(Modulo).ToList();
        moduli.Add(new(-moduli.Count - 1, 11));
        return moduli.Sum().Value;
    }

    static ModuloInt32 Modulo(string line)
    {
        int[] ns = [..line.Int32s()];
        return (-ns[0] - ns[2] - ns[3]).Modulo(ns[1]);
    }
}
