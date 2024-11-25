namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_05
{
    [Example(answer: 5, "0,9 -> 5,9;8,0 -> 0,8;9,4 -> 3,4;2,2 -> 2,1;7,0 -> 7,4;6,4 -> 2,0;0,9 -> 2,9;3,4 -> 1,4;0,0 -> 8,8;5,5 -> 8,2")]
    [Puzzle(answer: 6666, O.ms)]
    public int part_one(Lines lines) => Run(lines, diagonal: false);
  
    [Example(answer: 12, "0,9 -> 5,9;8,0 -> 0,8;9,4 -> 3,4;2,2 -> 2,1;7,0 -> 7,4;6,4 -> 2,0;0,9 -> 2,9;3,4 -> 1,4;0,0 -> 8,8;5,5 -> 8,2")]
    [Puzzle(answer: 19081, O.ms10)]
    public int part_two(Lines lines) => Run(lines, diagonal: true);

    [Test]
    public void Parse_dx() => Select("9,7 -> 7,7").Should().BeEquivalentTo(new Point[] { new(9, 7), new(8, 7), new(7, 7) });

    [Test]
    public void Parse_dy() => Select("1,1 -> 1,3").Should().BeEquivalentTo(new Point[] { new(1, 1), new(1, 2), new(1, 3) });

    [Test]
    public void Parse_dxy() => Select("9,7 -> 7,9").Should().BeEquivalentTo(new Point[] { new(9, 7), new(8, 8), new(7, 9) });

    static int Run(Lines lines, bool diagonal)
        => ItemCounter.New(lines.As(line => Select(line, diagonal)).SelectMany(p => p)).Count(p => p.Count >= 2);

    public static IEnumerable<Point> Select(string line, bool diagonal = true)
    {
        var parts = line.Split(" -> ");
        var start = Point.Parse(parts[0]);
        var end = Point.Parse(parts[1]);
        var delta = (end - start).Sign();

        return diagonal || delta.X == 0 || delta.Y == 0
            ? start.Repeat(delta, true).TakeWhile(point => point != end + delta)
            : [];
    }
}
