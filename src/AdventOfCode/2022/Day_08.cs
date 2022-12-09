namespace Advent_of_Code_2022;

[Category(Category.ms, Category.Grid, Category.VectorAlgebra)]
public class Day_08
{
    [Example(answer: 21, "30373\r\n25512\r\n65332\r\n33549\r\n35390")]
    [Puzzle(answer: 1711)]
    public int part_one(string input)
    {
        var map = input.CharPixels().Grid();
        return map.Positions.Count(p => map.OnEdge(p)
            || IsVisible(p, Vector.N, map)
            || IsVisible(p, Vector.E, map)
            || IsVisible(p, Vector.S, map)
            || IsVisible(p, Vector.W, map));
    }
    static bool IsVisible(Point p, Vector dir, Grid<char> map) => p.Repeat(dir).TakeWhile(map.OnGrid).All(o => map[o] < map[p]);

    [Example(answer: 8, "30373\r\n25512\r\n65332\r\n33549\r\n35390")]
    [Puzzle(answer: 301392)]
    public long part_two(string input)
    {
        var map = input.CharPixels().Grid();
        return map.Positions.Where(p => !map.OnEdge(p)).Select(p => Scenic(p, map)).Max();
    }

    static int Scenic(Point p, Grid<char> map) => CompassPoints.Primary.Select(d => View(p, d.ToVector(), map)).Product();

    static int View(Point p, Vector dir, Grid<char> map)
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
