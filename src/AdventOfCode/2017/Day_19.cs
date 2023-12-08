using SmartAss.Text;

namespace Advent_of_Code_2017;

[Category(Category.Grid)]
public class Day_19
{
    [Example(answer: "ABCDEF", Example._1)]
    [Puzzle(answer: "GPALMJSOY", O.ms10)]
    public char[] part_one(CharGrid map) => Navigate(map).Where(char.IsLetter).ToArray();

    [Example(answer: 38, Example._1)]
    [Puzzle(answer: 16204, O.ms10)]
    public int part_two(CharGrid map) => Navigate(map).Count();

    public IEnumerable<char> Navigate(CharGrid map)
    {
        map.SetNeighbors(Neighbors.Grid);
        var pos = map.First(kvp => kvp.Value == '|').Key;
        var dir = Vector.S;

        while (map.OnGrid(pos) && map[pos] != ' ')
        {
            var ch = map[pos];
            yield return ch;

            if (ch == '+')
            {
                var next = map.Neighbors[pos].FirstOrDefault(n => map[n] != ' ' && (n + dir) != pos);
                dir = next - pos;
            }
            pos += dir;
        }
    }
}
