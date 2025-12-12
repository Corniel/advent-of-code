namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_05
{
    [Example(answer: 5, "0,9 -> 5,9;8,0 -> 0,8;9,4 -> 3,4;2,2 -> 2,1;7,0 -> 7,4;6,4 -> 2,0;0,9 -> 2,9;3,4 -> 1,4;0,0 -> 8,8;5,5 -> 8,2")]
    [Puzzle(answer: 6666, O.ms)]
    public int part_one(Point2Ds points) => Run(points, diagonal: false);

    [Example(answer: 12, "0,9 -> 5,9;8,0 -> 0,8;9,4 -> 3,4;2,2 -> 2,1;7,0 -> 7,4;6,4 -> 2,0;0,9 -> 2,9;3,4 -> 1,4;0,0 -> 8,8;5,5 -> 8,2")]
    [Puzzle(answer: 19081, O.ms10)]
    public int part_two(Point2Ds points) => Run(points, diagonal: true);

      static int Run(Point2Ds points, bool diagonal) => ItemCounter.New(points.ChunkBy(2).Select(pair => Select(pair, diagonal)).SelectMany(p => p)).Count(p => p.Count >= 2);

    static IEnumerable<Point> Select(ImmutableArray<Point> pair , bool diagonal = true)
    {
        var (start, end) = (pair[0], pair[1]);
        var delta = (end - start).Sign();
        return diagonal || delta.X == 0 || delta.Y == 0
            ? start.Repeat(delta, true).TakeWhile(point => point != end + delta)
            : [];
    }
}
