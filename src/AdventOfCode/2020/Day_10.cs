namespace Advent_of_Code_2020;

[Category(Category.SequenceProgression)]
public class Day_10
{
    [Example(answer: 5 * 7, "16,10,15,5,1,11,7,19,6,12,4")]
    [Puzzle(answer: 2100, O.μs)]
    public int part_one(Ints numbers)
    {
        var ns = new UniqueNumbers(numbers) { 0 };
        var d1 = 0; var d3 = 1;

        foreach (var p in ns.SelectWithPrevious())
        {
            switch (p.Current - p.Previous)
            {
                case 1: d1++; break;
                case 3: d3++; break;
            }
        }
        return d1 * d3;
    }

    [Example(answer: 8, "16,10,15,5,1,11,7,19,6,12,4")]
    [Example(answer: 19208, "28,33,18,42,31,14,46,20,48,47,24,23,49,45,19,38,39,11,1,32,25,35,8,17,7,9,4,2,34,10,3")]
    [Puzzle(answer: 16198260678656L, O.μs)]
    public long part_two(Ints numbers)
    {
        var ns = new UniqueNumbers(numbers) { 0 };
        ns.Add(ns.Maximum + 3);

        var size = 0; var combo = 1L;

        foreach (var p in ns.SelectWithPrevious())
        {
            size++;
            if (p.Current - p.Previous == 3)
            {
                combo *= combos[size];
                size = 0;
            }
        }
        return combo;
    }

    static readonly int[] combos = [1, 1, 1, 2, 4, 7];
}
