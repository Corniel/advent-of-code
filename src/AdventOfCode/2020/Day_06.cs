namespace Advent_of_Code_2020;

[Category(Category.Cryptography)]
public class Day_06
{
    [Example(answer: 11, Example._1)]
    [Puzzle(answer: 7110, O.μs100)]
    public int part_one(GroupedLines groups) => groups.Sum(group => Characters.a_z.Count(ch => group.Exists(member => member.Contains(ch))));

    [Example(answer: 6, Example._1)]
    [Puzzle(answer: 3628, O.μs100)]
    public int part_two(GroupedLines groups) => groups.Sum(group => Characters.a_z.Count(ch => group.TrueForAll(member => member.Contains(ch))));
}
