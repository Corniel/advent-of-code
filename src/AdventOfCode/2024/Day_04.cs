namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public class Day_04
{
    [Example(answer: 18, Example._1)]
    [Puzzle(answer: 2500, O.ms)]
    public int part_one(CharGrid map) => map.Positions()
        .Where(map.OnEdge)
        .SelectMany(p => CompassPoints.All.Select(d => new Cursor(p, d).Move(2))
        // otherwise XMAS on edges will be count multiple times.
        .Where(c => !map.OnEdge(c.Pos) || !map.OnGrid(c.Move(-3))))
        .Sum(c => Scan(c, map));

    static int Scan(Cursor c, CharGrid map) => c.Moves()
        .TakeWhile(c => map.OnGrid(c)).Count(c => XMAS(c, map));

    static bool XMAS(Cursor c, CharGrid map)
        => map[c./*.....*/Pos] == 'X'
        && map[c.Move(-1).Pos] == 'M'
        && map[c.Move(-2).Pos] == 'A'
        && map[c.Move(-3).Pos] == 'S';

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
