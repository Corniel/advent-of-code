namespace Advent_of_Code_2017;

[Category(Category.SequenceProgression)]
public class Day_13
{
    [Example(answer: 24, "0: 3;1: 2;4: 4;6: 4")]
    [Puzzle(answer: 1316, O.ns100)]
    public int part_one(Ints numbers) => numbers.ChunkBy(2).Where(l => Layer(l[0], l[1]) == 0).Sum(l => l[0] * l[1]);

    [Example(answer: 10, "0: 3;1: 2;4: 4;6: 4")]
    [Puzzle(answer: 3840052, O.ms10)]
    public int part_two(Ints numbers)
    {
        var layers = numbers.ChunkBy(2).OrderBy(l => l[1]).ToArray();
        return Range(0, int.MaxValue).First(turn => layers.All(l => Layer(l[0] + turn, l[1]) != 0));
    }

    static int Layer(int turn, int size)
    {
        var periode = (size - 1) * 2;
        var pos = turn % periode;
        return pos < size ? pos : periode - pos;
    }
}
