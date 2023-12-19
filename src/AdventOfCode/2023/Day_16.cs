namespace Advent_of_Code_2023;

[Category(Category.Grid)]
public class Day_16
{
    [Example(answer: 46, Example._1)]
    [Puzzle(answer: 7996, O.ms)]
    public int part_one(CharGrid map) => Navigate(new(Point.O, Vector.E), map);
    
    [Example(answer: 51, Example._1)]
    [Puzzle(answer: 8239, O.ms100)]
    public int part_two(CharGrid map) => Starts(map).Select(c => Navigate(c, map)).Max();

    static IEnumerable<Cursor> Starts(CharGrid map)
    {
        for (var col = 0; col < map.Cols; col++)
        {
            yield return new(new(col, 0), Vector.S);
            yield return new(new(col, map.Rows - 1), Vector.N);
        }
        for (var row = 0; row < map.Rows; row++)
        {
            yield return new(new(0, row), Vector.E);
            yield return new(new(map.Cols - 1, row), Vector.W);
        }
    }

    static int Navigate(Cursor start, CharGrid map)
    {
        var energized = new HashSet<Point>();
        var done = new HashSet<Cursor>();
        var q = new Stack<Cursor>().PushRange([start.Reverse()]);

        foreach (var cur in q.PopAll().Select(c => c.Move()).Where(c => map.OnGrid(c) && done.Add(c)))
        {
            energized.Add(cur);
            var m = map.Val(cur);
            var horizon = cur.Dir.IsHorizontal;

            if ((m == '|' && horizon) || m == '-' && !horizon)
            {
                q.Push(cur.TurnLeft());
                q.Push(cur.TurnRight());
            }
            else if (m == '/') q.Push(horizon ? cur.TurnLeft() : cur.TurnRight());
            else if (m == '\\') q.Push(horizon ? cur.TurnRight() : cur.TurnLeft());
            else q.Push(cur);
        }
        return energized.Count;
    }
}
