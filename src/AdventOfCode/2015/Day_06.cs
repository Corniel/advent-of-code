namespace Advent_of_Code_2015;

[Category(Category.Grid, Category.Simulation)]
public class Day_06
{
    [Puzzle(answer: 400410, O.ms100)]
    public int part_one(Inputs<Instr> inputs)
    {
        var grid = new Grid<bool>(1000, 1000);

        foreach (var instr in inputs)
        {
            foreach (var point in Points.Range(instr.Start, instr.End))
            {
                grid[point] = instr.Type switch
                {
                    InstrType.TurnOn => true,
                    InstrType.TurnOff => false,
                    _ => !grid[point],
                };
            }
        }
        return grid.Count(kvp => kvp.Value);
    }

    [Puzzle(answer: 15343601, O.ms100)]
    public int part_two(Lines lines)
    {
        var grid = new Grid<int>(1000, 1000);

        foreach (var instruction in lines.As(Instr.Parse))
        {
            foreach (var point in Points.Range(instruction.Start, instruction.End))
            {
                grid[point] = instruction.Type switch
                {
                    InstrType.TurnOn => grid[point] + 1,
                    InstrType.TurnOff => Math.Max(0, grid[point] - 1),
                    _ => grid[point] + 2,
                };
            }
        }
        return grid.Sum(kvp => kvp.Value);
    }

    public enum InstrType { TurnOn, TurnOff, Toggle }

    public record Instr(InstrType Type, Point Start, Point End)
    {
        public static Instr Parse(string line)
        {
            var splitted = line.Split(' ');
            var type = InstrType.Toggle;
            if (splitted.Length == 5)
            {
                type = splitted[1] == "on" ? InstrType.TurnOn : InstrType.TurnOff;
            }
            return new Instr(type, Point.Parse(splitted[^3]), Point.Parse(splitted[^1]));
        }
    }
}
