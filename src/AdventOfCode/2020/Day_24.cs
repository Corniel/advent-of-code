namespace Advent_of_Code_2020;

[Category(Category.VectorAlgebra, Category.GameOfLife)]
public class Day_24
{
    [Example(answer: 10, Example._1)]
    [Puzzle(answer: 523, O.μs100)]
    public int part_one(Lines lines) => Cells.Parse(lines).Count;

    [Example(answer: 2208, Example._1)]
    [Puzzle(answer: 4225, O.ms100)]
    public int part_two(Lines lines)
    {
        var cells = Cells.Parse(lines);
        cells.Generations(100);
        return cells.Count;
    }

    [Test]
    public void nwwswee_makes_roundtrip()
    {
        var location = Point.O;
        foreach (var step in Cells.Steps("nwwswee")) { location += step; }
        location.Should().Be(Point.O);
    }

    public class Cells : GameOfLife<Point>
    {
        protected override bool Dies(int living) => living == 0 || living > 2;
        protected override bool IntoExistence(int living) => living == 2;
        public override IEnumerable<Point> Neighbors(Point cell) => Directions.Select(dir => cell + dir);
        public void Toggle(Point cell) { if (!Add(cell)) Remove(cell);}

        public static Cells Parse(Lines str)
        {
            var cells = new Cells();
            foreach (var path in str.As(Steps))
            {
                cells.Toggle(Point.O + path.Sum());
            }
            return cells;
        }
        internal static IEnumerable<Vector> Steps(string line)
        {
            var prev = ' ';
            foreach (var ch in line)
            {
                if (ch == 'e')
                {
                    if (prev == 's') yield return Vector.SE;
                    else if (prev == 'n') yield return Vector.NE;
                    else yield return Vector.E * 2;
                }
                else if (ch == 'w')
                {
                    if (prev == 's') yield return Vector.SW;
                    else if (prev == 'n') yield return Vector.NW;
                    else yield return Vector.W * 2;
                }
                prev = ch;
            }
        }
        static readonly Vector[] Directions = [Vector.E * 2, Vector.SE, Vector.NE, Vector.W * 2, Vector.SW, Vector.NW];
    }
}
