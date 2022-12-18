﻿namespace Advent_of_Code_2022;

[Category(Category._3D, Category.PathFinding)]
public class Day_18
{
    [Example(answer: 10, "1,1,1;2,1,1")]
    [Example(answer: 64, "2,2,2;1,2,2;3,2,2;2,1,2;2,3,2;2,2,1;2,2,3;2,2,4;2,2,6;1,2,5;3,2,5;2,1,5;2,3,5")]
    [Puzzle(answer: 4348, O.ms)]
    public int part_one(string input) => Surface(input.Lines(Point3D.Parse).ToHashSet());

    [Example(answer: 58, "2,2,2;1,2,2;3,2,2;2,1,2;2,3,2;2,2,1;2,2,3;2,2,4;2,2,6;1,2,5;3,2,5;2,1,5;2,3,5")]
    [Puzzle(answer: 2546, O.ms)]
    public int part_two(string input)
    {
        var min = 0;  var max = 21; var cubes = input.Lines(Point3D.Parse).ToHashSet();
        return Surface(cubes) - Surface(Todo(cubes, min, max));
    }

    static ISet<Point3D> Todo(ISet<Point3D> done, int min, int max)
    {
        var queue = new Queue<Point3D>();
        queue.Enqueue(new Point3D(min, min, min));

        while (queue.TryDequeue(out var point))
        {
            queue.EnqueueRange(Neigbors.Select(v => point + v).Where(p => InRange(p, min, max) && done.Add(p)));
        }
        return Points3D.Range(new Point3D(min, min, min), new Point3D(max, max, max)).Where(p => !done.Contains(p)).ToHashSet();

        static bool InRange(Point3D n, int min, int max) => n.X >= min && n.X <= max && n.Y >= min && n.Y <= max && n.Z >= min && n.Z <= max;
    }

    static int Surface(ISet<Point3D> cubes) => cubes.Sum(c => 6 - Neigbors.Count(v => cubes.Contains(c + v)));

    static readonly Vector3D[] Neigbors = new Vector3D[] { new(-1, 0, 0), new(+1, 0, 0), new(0, -1, 0), new(0, +1, 0), new(0, 0, -1), new(0, 0, +1) };
}
