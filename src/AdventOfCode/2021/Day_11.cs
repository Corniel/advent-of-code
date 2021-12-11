namespace Advent_of_Code_2021;

public class Day_11
{
    private const string Example = @"
5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

    [Example(answer: 1656, Example)]
    [Puzzle(answer: 1691, year: 2021, day: 11)]
    public int part_one(string input) => Simulate(input).Take(100).Sum();

    [Example(answer: 195, Example)]
    [Puzzle(answer: 216, year: 2021, day: 11)]
    public int part_two(string input) => Simulate(input).TakeWhile(f => f != 100).Count() + 1;

    private static IEnumerable<int> Simulate(string input)
    {
        var grid = input.CharPixels().Grid((ch) => ch - '0');
        var dones = new Grid<bool>(grid.Cols, grid.Rows);
        var queue = new Queue<Point>();
        return Enumerable.Range(1, int.MaxValue).Select(step => Step(grid, dones, queue));
    }

    private static int Step(Grid<int> grid, Grid<bool> dones, Queue<Point> queue)
    {
        foreach (var pos in grid.Positions)
        {
            grid[pos]++;
            dones[pos] = false;
        }
        foreach(var pos in grid.Positions.Where(p => grid[p] == 10))
        {
            queue.Enqueue(pos);
            dones[pos] = true;
        }
        while (queue.Any())
        {
            foreach (var neighbor in Neighbors.Grid(grid, queue.Dequeue(), diagonals: true))
            {
                if (++grid[neighbor] >= 10 && !dones[neighbor])
                {
                    dones[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }
        var flashes = 0;
        foreach (var ten in grid.Positions.Where(p => grid[p] >= 10))
        {
            flashes++;
            grid[ten] = 0;
        }
        return flashes;
    }
}
