namespace Advent_of_Code_2021;

[Category(Category.Grid, Category.Cryptography, Category.ASCII)]
public class Day_13
{
    [Example(answer: 17, "6,10;0,14;9,10;0,3;10,4;4,11;6,0;6,12;4,1;0,13;10,12;3,4;3,0;8,4;1,10;2,14;8,10;9,0;;fold along y=7;fold along x=5")]
    [Puzzle(answer: 710, O.μs10)]
    public int part_one(GroupedLines groups) => Fold(groups, 1).Count;
       
    [Puzzle(answer: "EPLGRULR", O.μs100)]
    public string part_two(GroupedLines groups)
        => Grid<bool>.FromPoints(Fold(groups), true).AsciiText();

    static HashSet<Point> Fold(GroupedLines groups, int foldings = int.MaxValue)
    {
        var points = new HashSet<Point>(groups[0].Select(Point.Parse));
        foreach (var instruction in groups[1].Select(line => line[11..]).Take(foldings))
        {
            var flipY = instruction[0] == 'x';
            var fold = instruction[2..].Int32();
            points = new HashSet<Point>(points.Select(p => Fold(p, flipY, fold)));
        }
        return points;
    }
    static Point Fold(Point p, bool flipY, int fold)
    {
        if (flipY ? p.X < fold : p.Y < fold) return p;
        else return flipY ? p.FlipY(fold) : p.FlipX(fold);
    }
}
