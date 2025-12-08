namespace Advent_of_Code_2022;

[Category(Category._3D, Category.PathFinding)]
public class Day_18
{
    [Example(answer: 10, "1,1,1;2,1,1")]
    [Example(answer: 64, "2,2,2;1,2,2;3,2,2;2,1,2;2,3,2;2,2,1;2,2,3;2,2,4;2,2,6;1,2,5;3,2,5;2,1,5;2,3,5")]
    [Puzzle(answer: 4348, O.μs100)]
    public int part_one(Point3Ds points) => Surface([.. points]);

    [Example(answer: 58, "2,2,2;1,2,2;3,2,2;2,1,2;2,3,2;2,2,1;2,2,3;2,2,4;2,2,6;1,2,5;3,2,5;2,1,5;2,3,5")]
    [Puzzle(answer: 2546, O.ms)]
    public int part_two(Point3Ds points)
    {
        var min = 0; var max = 21; var cubes = points.ToHashSet();
        return Surface(cubes) - Surface(Todo(cubes, min, max));
    }

    static HashSet<Point3D> Todo(HashSet<Point3D> done, int min, int max)
    {
        var queue = new Queue<Point3D>();
        queue.Enqueue(new Point3D(min, min, min));

        while (queue.TryDequeue(out var point))
        {
            queue.EnqueueRange(Neigbors.Select(v => point + v).Where(p => InRange(p, min, max) && done.Add(p)));
        }
        return Points3D.Range(new Point3D(min, min, min), new Point3D(max, max, max)).Where(p => !done.Contains(p)).ToHashSet();

        static bool InRange(Point3D n, int min, int max) => n.X.InRange(min, max) && n.Y.InRange(min, max) && n.Z.InRange(min, max);
    }

    static int Surface(HashSet<Point3D> cubes) => cubes.Sum(c => 6 - Neigbors.Count(v => cubes.Contains(c + v)));

    static readonly Vector3D[] Neigbors = [new(-1, 0, 0), new(+1, 0, 0), new(0, -1, 0), new(0, +1, 0), new(0, 0, -1), new(0, 0, +1)];
}
