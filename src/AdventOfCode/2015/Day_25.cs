namespace Advent_of_Code_2015;

[Category(Category.SequenceProgression)]
public class Day_25
{
    [Example(answer: 20151125, 1, 1)]
    [Example(answer: 24659492, 6, 4)]
    [Puzzle(answer: 9132360L, 2981, 3075, O.ms10)]
    public long part_one(int row, int col) => Sequence.AdHoc(20151125L, n=> n * 252533 % 33554393).Skip(Skip(row, col)).First();

    static int Skip(int row, int col) => (col + row - 1) * (col + row) / 2 - row;

    [Puzzle(answer: "You only need 49 stars to boost it", "You only need 49 stars to boost it")]
    public string part_two(string str) => str;
}
