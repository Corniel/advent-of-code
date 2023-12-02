namespace Advent_of_Code_2022;

[Category(Category.ASCII)]
public class Day_10
{
    [Example(answer: 13140, Example._1)]
    [Puzzle(answer: 12980, O.μs10)]
    public int part_one(Lines lines) => Data.Parse(lines).Skip(19).WithStep(40).Select(x => x.Product).Sum();

    [Puzzle(answer: "BRJLFULP", O.μs10)]
    public string part_two(Lines lines)
    {
        var grid = new Grid<bool>(40, 6);
        grid.Set(true, Data.Parse(lines).Where(d => d.Draw).Select(s => s.Point));
        return grid.AsciiText();
    }

    record Data(int Cycle, int Strength)
    {
        public int Product => Cycle * Strength;
        public Point Point => new((Cycle - 1).Mod(40), Cycle / 40);
        public bool Draw => (Point.X - Strength).Abs() <= 1;

        public static IEnumerable<Data> Parse(Lines lines)
        {
            var strength = 1; var cycle = 1;
            foreach (var line in lines)
            {
                if (line == "noop") yield return new(cycle++, strength);
                else
                {
                    yield return new(cycle++, strength);
                    yield return new(cycle++, strength);
                    strength += line[5..].Int32();
                }
            }
        }
    }
}
