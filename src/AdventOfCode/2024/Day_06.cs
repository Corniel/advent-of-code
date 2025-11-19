namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public class Day_06
{
    [Example(answer: 41, Example._1)]
    [Puzzle(answer: 4988, O.μs100)]
    public int part_one(CharGrid map) => Walk(Cursor(map), map, []).Count;

    [Example(answer: 6, Example._1)]
    [Puzzle(answer: 1697, O.ms10)]
    public int part_two(CharGrid map)
    {
        var cursor = Cursor(map);
        return Walk(cursor, map, []).Count(e => Loop(cursor, map, [], e));
    }

    static Cursor Cursor(CharGrid map) => new(map.Position(c => c == '^'), Vector.N);

    static bool Loop(Cursor cur, CharGrid map, HashSet<Cursor> visited, Point hash)
    {
        do
        {
            if (cur.Pos == hash || map[cur.Pos] == '#')
            {
                if (!visited.Add(cur)) return true;
                cur = cur.Reverse().TurnRight();
            }
            cur = cur.Move();
        }
        while (map.OnGrid(cur));
        return false;
    }

    static HashSet<Point> Walk(Cursor cur, CharGrid map, HashSet<Point> path)
    {
        do
        {
            if (map[cur.Pos] == '#') cur = cur.Reverse().TurnRight();
            path.Add(cur.Pos);
            cur = cur.Move();
        }
        while (map.OnGrid(cur));
        return path;
    }
}
