namespace Advent_of_Code_2022;

[Category(Category.PathFinding)]
public class Day_12
{
    [Example(answer: 31, Example._1)]
    [Puzzle(answer: 394, O.μs100)]
    public int part_one(string input)
    {
        var area = Area.Parse(input);
        return area.Find(area.S);
    }

    [Example(answer: 29, Example._1)]
    [Puzzle(answer: 388, O.ms100)]
    public long part_two(string input)
    {
        var area = Area.Parse(input);
        var starts = area.Map.Where(p => p.Value == 'a').Select(p => p.Key);
        return starts.Select(a => area.Find(a)).Min();
    }

    record Area(Grid<char> Map, Point S, Point E)
    {
        public int Find(Point current)
        {
            var done = new Grid<bool>(Map.Cols, Map.Rows);
            done[current] = true;

            var distance = 0;
            var queue = new Queue<Point>();
            queue.Enqueue(current);

            while (queue.NotEmpty())
            {
                foreach (var point in queue.DequeueCurrent())
                {
                    if (point == E) return distance;
                    var height = Map[point];
                    foreach (var n in Neighbors.Grid(Map, point, CompassPoints.Primary).Where(n => !done[n] && Map[n] - height <= 1))
                    {
                        done[n] = true;
                        queue.Enqueue(n);
                    }
                }
                distance++;
            }
            return int.MaxValue;
        }

        public static Area Parse(string input)
        {
            var map = input.CharPixels().Grid();
            var s = map.First(p => p.Value == 'S').Key;
            var e = map.First(p => p.Value == 'E').Key;
            map[s] = 'a';
            map[e] = 'z';
            return new(map, s, e);
        }
    }
}
