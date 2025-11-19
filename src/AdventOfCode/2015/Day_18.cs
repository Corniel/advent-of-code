namespace Advent_of_Code_2015;

[Category(Category.GameOfLife)]
public class Day_18
{
    [Puzzle(answer: 814, O.ms10)]
    public int part_one(CharPixels chars)
    {
        var simulation = new GameOfLife(chars.Grid(ch => ch == '#'));
        simulation.Generations(100);
        return simulation.Count;
    }

    [Puzzle(answer: 924, O.ms10)]
    public int part_two(CharPixels chars)
    {
        var simulation = new GameOfLife(chars.Grid(ch => ch == '#'));
        simulation.AddRange([(0, 0), (0, 99), (99, 0), (99, 99)]);
        for (var step = 1; step <= 100; step++)
        {
            simulation.NextGeneration();
            simulation.AddRange([(0, 0), (0, 99), (99, 0), (99, 99)]);
        }
        return simulation.Count;
    }

    class GameOfLife : GameOfLife<Point>
    {
        public GameOfLife(Grid<bool> grid)
        {
            neighbors = grid.SetNeighbors(SmartAss.Maps.Neighbors.Grid, CompassPoints.All).Neighbors;
            this.AddRange(grid.Positions(t => t));
        }
        readonly Grid<GridNeighbors> neighbors;

        public override IEnumerable<Point> Neighbors(Point cell) => neighbors[cell];
    }
}
