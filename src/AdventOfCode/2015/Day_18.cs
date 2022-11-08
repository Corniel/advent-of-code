namespace Advent_of_Code_2015;

[Category(Category.GameOfLife)]
public class Day_18
{
    [Puzzle(answer: 814, year: 2015, day: 18)]
    public long part_one(string input)
    {
        var simulation = new GameOfLife(input.CharPixels().Grid(ch => ch == '#'));
        simulation.Generations(100);
        return simulation.Count;
    }

    [Puzzle(answer: 924, year: 2015, day: 18)]
    public long part_two(string input)
    {
        var simulation = new GameOfLife(input.CharPixels().Grid(ch => ch == '#'));
        simulation.AddRange(new[] { Point.O, new Point(0, 99), new Point(99, 0), new Point(99, 99) });
        for (var step = 1; step <= 100; step++)
        {
            simulation.NextGeneration();
            simulation.AddRange(new[] { Point.O, new Point(0, 99), new Point(99, 0), new Point(99, 99) });
        }
        return simulation.Count;
    }

    class GameOfLife : GameOfLife<Point>
    {
        public GameOfLife(Grid<bool> grid)
        {
            neighbors = grid.SetNeighbors(SmartAss.Maps.Neighbors.Grid, CompassPoints.All).Neighbors;
            this.AddRange(grid.Where(tile => tile.Value).Select(tile => tile.Key));
        }
        private readonly Grid<GridNeighbors> neighbors;

        public override IEnumerable<Point> Neighbors(Point cell) => neighbors[cell];
    }
}