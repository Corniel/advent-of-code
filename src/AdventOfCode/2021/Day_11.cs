namespace Advent_of_Code_2021;

[Category(Category.Grid, Category.Simulation)]
public class Day_11
{
    [Example(answer: 1656, Example._1)]
    [Puzzle(answer: 1691, O.ms)]
    public int part_one(string input) => Simulate(input).Take(100).Sum();

    [Example(answer: 195, Example._1)]
    [Puzzle(answer: 216, O.ms)]
    public int part_two(string input) => Simulate(input).TakeWhile(f => f != 100).Count() + 1;

    private static IEnumerable<int> Simulate(string input)
    {
        var grid = input.CharPixels().Grid((ch) => ch - '0').SetNeighbors(Neighbors.Grid, CompassPoints.All);
        var dones = new Grid<bool>(grid.Cols, grid.Rows);
        var stack = new Stack<Point>();
        return Range(1, int.MaxValue).Select(step => Step(grid, dones, stack));
    }

    private static int Step(Grid<int> grid, Grid<bool> dones, Stack<Point> stack)
    {
        foreach (var pos in grid.Positions)
        {
            grid[pos]++;
            dones[pos] = false;
        }
        foreach(var pos in grid.Positions.Where(p => grid[p] == 10))
        {
            stack.Push(pos);
            dones[pos] = true;
        }
        while (stack.Any())
        {
            foreach (var neighbor in grid.Neighbors[stack.Pop()])
            {
                if (++grid[neighbor] >= 10 && !dones[neighbor])
                {
                    dones[neighbor] = true;
                    stack.Push(neighbor);
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
