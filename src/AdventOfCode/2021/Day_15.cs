namespace Advent_of_Code_2021;

[Category(Category.Grid, Category.PathFinding)]
public class Day_15
{
    [Example(answer: 40, Example._1)]
    [Puzzle(answer: 595, O.ms10)]
    public int part_one(CharPixels chars) => Run(chars.Grid(c => c.Digit()));

    [Example(answer: 315, Example._1)]
    [Puzzle(answer: 2914, O.ms100)]
    public int part_two(CharPixels chars) => Run(Scale(chars.Grid(c => c.Digit())));

    static int Run(Grid<int> costs)
    {
        costs.SetNeighbors(Neighbors.Grid);
        var distances = new Grid<int>(costs.Cols, costs.Rows);
        distances.Set(short.MaxValue, distances.Positions());
        distances[Point.O] = 0;
        Point target = (costs.Cols - 1, costs.Rows - 1);
        var tiles = new Queue<Point>();
        tiles.Enqueue(Point.O);

        while (tiles.NotEmpty())
        {
            var curr = tiles.Dequeue();
            var distance = distances[curr];

            foreach (var next in costs.Neighbors[curr].OrderBy(n => costs[n] + target.ManhattanDistance(n)))
            {
                if ((distance + costs[next]) is var test && test < distances[next])
                {
                    distances[next] = test;
                    if (next != target) { tiles.Enqueue(next); }
                }
            }
        }
        return distances[target];
    }

    static Grid<int> Scale(Grid<int> grid)
    {
        var larger = new Grid<int>(grid.Cols * 5, grid.Rows * 5);

        foreach (var point in grid.Positions())
        {
            for (var col_factor = 0; col_factor < 5; col_factor++)
            {
                for (var row_factor = 0; row_factor < 5; row_factor++)
                {
                    var row = point.X + grid.Rows * row_factor;
                    var col = point.Y + grid.Cols * col_factor;
                    larger[row, col] = (grid[point] + col_factor + row_factor + -1).Mod(9) + 1;
                }
            }
        }
        return larger;
    }
}
