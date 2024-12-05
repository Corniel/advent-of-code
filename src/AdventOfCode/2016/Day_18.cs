namespace Advent_of_Code_2016;

[Category(Category.GameOfLife)]
public class Day_18
{
    [Example(answer: 38, ".^^.^.^^^^", 10)]
    [Puzzle(answer: 1963, null, 40, O.μs)]
    public int part_one(string str, int rows)
    {
        bool[] curr = [false, .. str.Select(c => c == '^'), false];
        bool[] next = [..curr];
        var safe = 0;

        foreach(var _ in Range(0, rows))
        {
            safe += curr.Count(x => !x) - 2;
            for (var i = 1; i < curr.Length - 1; i++)  next[i] = (curr[i - 1] ^ curr[i + 1]);
            (curr, next) = (next, curr);
        }

        return safe;
    }

    [Puzzle(answer: 20009568, null, 400000, O.ms100)]
    public int part_two(string str, int rows) => part_one(str, rows);
}
