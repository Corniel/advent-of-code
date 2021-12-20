namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_05
{
    private const string Example = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

    [Test]
    public void Parse_dx()
        => Select("9,7 -> 7,7").Should().BeEquivalentTo(new[] { new Point(9, 7), new Point(8, 7), new Point(7, 7) });
    
    [Test]
    public void Parse_dy()
        => Select("1,1 -> 1,3").Should().BeEquivalentTo(new[] { new Point(1, 1), new Point(1, 2), new Point(1, 3) });
    
    [Test]
    public void Parse_dxy()
        => Select("9,7 -> 7,9").Should().BeEquivalentTo(new[] { new Point(9, 7), new Point(8, 8), new Point(7, 9) });

    [Example(answer: 5, Example)]
    [Puzzle(answer: 6666, year: 2021, day: 05)]
    public int part_one(string input) => Run(input, diagonal: false);
  
    [Example(answer: 12, Example)]
    [Puzzle(answer: 19081, year: 2021, day: 05)]
    public int part_two(string input) => Run(input, diagonal: true);

    private static int Run(string input, bool diagonal)
    {
        var area = new Dictionary<Point, int>();
        foreach (var point in input.Lines(line => Select(line, diagonal)).SelectMany(p => p))
        {
            if (area.ContainsKey(point))
            { area[point]++; }
            else { area[point] = 1; }
        }
        return area.Count(kvp => kvp.Value >= 2);
    }

    public static IEnumerable<Point> Select(string line, bool diagonal = true)
    {
        var parts = line.Split(" -> ");
        var start = Point.Parse(parts[0]);
        var end = Point.Parse(parts[1]);
        var delta = (end - start).Sign();

        return diagonal || delta.X == 0 || delta.Y == 0
            ? start.Repeat(delta, true).TakeWhile(point => point != end + delta)
            : Array.Empty<Point>();
    }
}
