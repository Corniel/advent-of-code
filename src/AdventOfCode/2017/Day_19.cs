namespace Advent_of_Code_2017;

[Category(Category.Grid)]
public class Day_19
{
    [Example(answer: "ABCDEF", Example._1)]
    [Puzzle(answer: "GPALMJSOY", O.ms10)]
    public char[] part_one(CharGrid map) => [..Navigate(map).Where(char.IsLetter)];

    [Example(answer: 38, Example._1)]
    [Puzzle(answer: 16204, O.ms10)]
    public int part_two(CharGrid map) => Navigate(map).Count();

    static IEnumerable<char> Navigate(CharGrid map)
    {
        map.SetNeighbors(Neighbors.Grid);
        var cur = new Cursor(map.First(kvp => kvp.Value == '|').Key, Vector.S);

        while (map.OnGrid(cur) && map.Val(cur) != ' ')
        {
            var ch = map.Val(cur);
            yield return ch;

            cur = ch == '+'
                ? map.Neighbors(cur).Select(cur.Move).First(n => map.Val(n) != ' ' && n.Pos != cur.Reverse().Pos)
                : cur.Move();
        }
    }
}
