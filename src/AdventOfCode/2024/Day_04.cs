namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public class Day_04
{
    [Example(answer: 18, Example._1)]
    [Puzzle(answer: 2500, O.ms)]
    public int part_one(CharGrid map) => map.Positions()
        .SelectMany(p => CompassPoints.All.Select(d => new Cursor(p, d)))
        .Count(c => XMAS(c, map, 0));

    static bool XMAS(Cursor c, CharGrid map, int i)
    {
        while (i < 4 && map.OnGrid(c) && map[c.Pos] == "XMAS"[i])
        {
            c = c.Move(); i++;
        }
        return i is 4;
    }

    [Example(answer: 9, Example._1)]
    [Puzzle(answer: 1933, O.μs100)]
    public int part_two(CharGrid map) => map.Positions().Count(p => MAS(p, map));

    static bool MAS(Point p, CharGrid map)
        => !map.OnEdge(p)
        && map[p] == 'A'
        && MS(map[p + Vector.NW], map[p + Vector.SE])
        && MS(map[p + Vector.NE], map[p + Vector.SW]);

    static bool MS(char m, char s) => (m == 'M' && s == 'S') || (s == 'M' && m == 'S');
}
