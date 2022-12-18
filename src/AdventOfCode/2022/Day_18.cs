namespace Advent_of_Code_2022;

[Category(Category._3D, Category.PathFinding)]
public class Day_18
{
    [Example(answer: 10, "1,1,1;2,1,1")]
    [Example(answer: 64, "2,2,2;1,2,2;3,2,2;2,1,2;2,3,2;2,2,1;2,2,3;2,2,4;2,2,6;1,2,5;3,2,5;2,1,5;2,3,5")]
    [Puzzle(answer: 4348, O.ms10)]
    public int part_one(string input) => Surface(input.Lines(Point3D.Parse).ToArray());

    [Example(answer: 58, "2,2,2;1,2,2;3,2,2;2,1,2;2,3,2;2,2,1;2,2,3;2,2,4;2,2,6;1,2,5;3,2,5;2,1,5;2,3,5")]
    [Puzzle(answer: 2546, O.ms10)]
    public int part_two(string input)
    {
        var min = 0;  var max = 21; var cubes = input.Lines(Point3D.Parse).ToArray();
        var inner = Todo(cubes, min, max);
        return Surface(cubes) - Surface(inner);
    }

    static Point3D[] Todo(Point3D[] cubes, int min, int max)
    {
        var done = new HashSet<Point3D>(cubes);
        var queue = new Queue<Point3D>();
        queue.Enqueue(new Point3D(min, min, min));

        var neigbors = new Vector3D[] { new(-1, 0, 0), new(+1, 0, 0), new(0, -1, 0), new(0, +1, 0), new(0, 0, -1), new(0, 0, +1) };

        while (queue.TryDequeue(out var point))
        {
            queue.EnqueueRange(neigbors.Select(v => point + v).Where(p => InRange(p, min, max) && done.Add(p)));
        }
        return Points3D.Range(new Point3D(min, min, min), new Point3D(max, max, max)).Where(p => !done.Contains(p)).ToArray();

        static bool InRange(Point3D n, int min, int max) => n.X >= min && n.X <= max && n.Y >= min && n.Y <= max && n.Z >= min && n.Z <= max;
    }

    static int Surface(Point3D[] cubes)
    {
        var surface = cubes.Length * 6;

        for (var i = 0; i < cubes.Length - 1; i++)
        {
            for (var o = i; o < cubes.Length; o++)
            {
                if (cubes[i].ManhattanDistance(cubes[o]) == 1) surface -= 2;
            }
        }
        return surface;
    }
}
