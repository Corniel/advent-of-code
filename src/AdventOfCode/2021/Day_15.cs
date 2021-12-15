namespace Advent_of_Code_2021;

public class Day_15
{
    private const string Example = @"
1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

    [Example(answer: 40, Example)]
    [Puzzle(answer: 595, year: 2021, day: 15)]
    public int part_one(string input)=> Run(input.CharPixels().Grid(ch => ch - '0'));

    [Example(answer: 315, Example)]
    [Puzzle(answer: 2914, year: 2021, day: 15)]
    public int part_two(string input) => Run(Scale(input.CharPixels().Grid(ch => ch - '0')));

    static int Run(Grid<int> costs)
    {
        var distances = new Grid<int>(costs.Cols, costs.Rows);
        distances.Set(short.MaxValue, distances.Positions);
        distances[Point.O] = 0;
        var target = new Point(costs.Cols - 1, costs.Rows - 1);
        var tiles = new Queue<Point>();
        tiles.Enqueue(Point.O);

        while (tiles.Any())
        {
            var curr = tiles.Dequeue();
            var distance = distances[curr];

            foreach (var next in Neighbors.Grid(costs, curr).OrderBy(n => costs[n] + target.ManhattanDistance(n)))
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

        foreach (var point in grid.Positions)
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
