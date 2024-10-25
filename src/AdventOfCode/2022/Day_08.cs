namespace Advent_of_Code_2022;

[Category(Category.Grid, Category.VectorAlgebra)]
public class Day_08
{
    [Example(answer: 21, "30373\n25512\n65332\n33549\n35390")]
    [Puzzle(answer: 1711, O.ms)]
    public int part_one(CharGrid map)
        => map.Positions().Count(p => map.OnEdge(p)
        || IsVisible(p, Vector.N, map)
        || IsVisible(p, Vector.E, map)
        || IsVisible(p, Vector.S, map)
        || IsVisible(p, Vector.W, map));

    static bool IsVisible(Point p, Vector dir, CharGrid map) => p.Repeat(dir).TakeWhile(map.OnGrid).All(o => map[o] < map[p]);

    [Example(answer: 8, "30373\n25512\n65332\n33549\n35390")]
    [Puzzle(answer: 301392, O.ms)]
    public int part_two(CharGrid map) => map.Positions(p => !map.OnEdge(p)).Select(p => Scenic(p, map)).Max();

    static int Scenic(Point p, CharGrid map) => CompassPoints.Primary.Select(d => View(p, d.ToVector(), map)).Product();

    static int View(Point p, Vector dir, CharGrid map)
    {
        var score = 0;
        foreach (var h in p.Repeat(dir).TakeWhile(map.OnGrid).Select(n => map[n]))
        {
            score++;
            if (h >= map[p]) return score;
        }
        return score;
    }
}
