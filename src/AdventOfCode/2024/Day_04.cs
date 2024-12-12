namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public class Day_04
{
    [Example(answer: 18, Example._1)]
    [Puzzle(answer: 2500, O.ms10)]
    public int part_one(CharGrid map) => map.Positions()
        .SelectMany(p => CompassPoints.All.Select(d => new Cursor(p, d)))
        .Count(c => Str(c, map) == "XMAS");
    
    static string Str(Cursor c, CharGrid map) => new([.. c.Moves().Take(4).TakeWhile(map.OnGrid).Select(p => map[p.Pos])]);

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
