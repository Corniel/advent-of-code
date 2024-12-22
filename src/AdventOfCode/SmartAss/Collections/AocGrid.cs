
namespace SmartAss.Collections;

public static class AocGrid
{
    [Pure]
    public static GridNeighbors Neighbors(CharGrid grid, Point position, IReadOnlyCollection<CompassPoint> directions)
        => new NoHashNeighbors(grid, position, directions);

    /// <summary>Gets all positions with a value other than '#'.</summary>
    [Pure]
    public static IEnumerable<Point> NonHashes(this CharGrid grid) => grid.Positions(c => c is  not '#');

    /// <summary>Gets all positions with a value of '#'.</summary>
    [Pure]
    public static IEnumerable<Point> Hashes(this CharGrid grid) => grid.Positions(c => c is '#');

    private sealed class NoHashNeighbors : GridNeighbors
    {
        public NoHashNeighbors(CharGrid grid, Point position, IReadOnlyCollection<CompassPoint> directions)
        {
            Grid = grid;
            Position = position;
            this.directions = directions;
        }

        private readonly CharGrid Grid;
        private readonly Point Position;
        private readonly IReadOnlyCollection<CompassPoint> directions;
        
        public int Count => Directions.Count();

        public Point this[int index] => this.Skip(index).First();

        public Point this[CompassPoint compass] => Directions.First(dir => dir.Key == compass).Value;

        [Pure]
        public bool Contains(CompassPoint compass) => Directions.Any(dir => dir.Key == compass);

        public IEnumerable<KeyValuePair<CompassPoint, Point>> Directions
        {
            get
            {
                var pos = Position;
                var grid = Grid;
                return directions.Select(dir => KeyValuePair.Create(dir, pos + dir.ToVector()))
                    .Where(p => grid.OnGrid(p.Value) && grid[p.Value] is not '#');
            }
        }

        [Pure]
        public IEnumerator<Point> GetEnumerator()
        {
            var grid = Grid;
            return Position.Projections(directions.Select(p => p.ToVector()))
                .Where(grid.OnGrid)
                .Where(p => grid[p] is not '#')
                .GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
