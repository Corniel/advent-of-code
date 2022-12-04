namespace Advent_of_Code_2021;

[Category(Category.Grid, Category.PathFinding)]
public class Day_09
{
    private const string Example = @"
2199943210
3987894921
9856789892
8767896789
9899965678";

    [Example(answer: 15, Example)]
    [Puzzle(answer: 588)]
    public int part_one(string input)
    {
        var map = input.CharPixels().Grid().SetNeighbors(Neighbors.Grid);
        return map.Positions
            .Where(point => map.Neighbors[point]
            .All(n => map[n] > map[point]))
            .Sum(p => map[p] - '0' + 1);
    }

    [Example(answer: 1134, Example)]
    [Puzzle(answer: 964712)]
    public long part_two(string input)
    {
        var map = input.CharPixels().Grid().SetNeighbors(Neighbors.Grid);
        var done = new Grid<bool>(map.Cols, map.Rows);
        var sizes = new List<long>();
        var queue = new Queue<Point>();

        foreach (var point in map.Positions.Where(p => !done[p] && map[p] != '9'))
        {
            queue.Clear();
            var size = 1;
            done[point] = true;
            queue.Enqueue(point);
            while(queue.Any())
            {
                foreach(var n in map.Neighbors[queue.Dequeue()])
                {
                    if(!done[n] && map[n] != '9')
                    {
                        size++;
                        done[n] = true;
                        queue.Enqueue(n);
                    }
                }
            }
            sizes.Add(size);
        }
        return sizes.OrderDescending().Take(3).Product();
    }
}
