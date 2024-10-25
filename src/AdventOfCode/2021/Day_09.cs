namespace Advent_of_Code_2021;

[Category(Category.Grid, Category.PathFinding)]
public class Day_09
{
    [Example(answer: 15, Example._1)]
    [Puzzle(answer: 588, O.ms)]
    public int part_one(CharGrid map) 
        => map.SetNeighbors(Neighbors.Grid).Positions()
            .Where(point => map.Neighbors[point]
            .All(n => map[n] > map[point]))
            .Sum(p => map[p].Digit() + 1);

    [Example(answer: 1134, Example._1)]
    [Puzzle(answer: 964712, O.ms)]
    public int part_two(CharGrid map)
    {
        map.SetNeighbors(Neighbors.Grid);
        var done = new Grid<bool>(map.Cols, map.Rows);
        var sizes = new List<int>();
        var queue = new Queue<Point>();

        foreach (var point in map.Positions(p => !done[p] && map[p] != '9'))
        {
            queue.Clear();
            var size = 1;
            done[point] = true;
            queue.Enqueue(point);
            while (queue.NotEmpty())
            {
                foreach (var n in map.Neighbors[queue.Dequeue()])
                {
                    if (!done[n] && map[n] != '9')
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
