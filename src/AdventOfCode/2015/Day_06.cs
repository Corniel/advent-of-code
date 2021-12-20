namespace Advent_of_Code_2015;

[Category(Category.Grid, Category.Simulation)]
public class Day_06
{
    [Puzzle(answer: 400410, year: 2015, day: 06)]
    public long part_one(string input)
    {
        var grid = new Grid<bool>(1000, 1000);
        
        foreach (var instruction in input.Lines(Instruction.Parse))
        {
            foreach(var point in Points.Range(instruction.Start, instruction.End))
            {
                grid[point] = instruction.Type switch
                {
                    InstructionType.TurnOn => true,
                    InstructionType.TurnOff => false,
                    _ => !grid[point],
                };
            }
        }
        return grid.Count(kvp => kvp.Value);
    }

    [Puzzle(answer: 15343601, year: 2015, day: 06)]
    public int part_two(string input)
    {
        var grid = new Grid<int>(1000, 1000);

        foreach (var instruction in input.Lines(Instruction.Parse))
        {
            foreach (var point in Points.Range(instruction.Start, instruction.End))
            {
                grid[point] = instruction.Type switch
                {
                    InstructionType.TurnOn => grid[point]+1,
                    InstructionType.TurnOff => Math.Max(0, grid[point] - 1),
                    _ => grid[point] + 2,
                };
            }
        }
        return grid.Sum(kvp => kvp.Value);
    }

    enum InstructionType { TurnOn, TurnOff, Toggle }

    record Instruction(InstructionType Type, Point Start, Point End)
    {
        public static Instruction Parse(string line)
        {
            var splitted = line.Split(' ');
            var type = InstructionType.Toggle;
            if (splitted.Length == 5)
            {
                type = splitted[1] == "on" ? InstructionType.TurnOn : InstructionType.TurnOff;
            }
            return new Instruction(type, Point.Parse(splitted[^3]), Point.Parse(splitted[^1]));
        }
    }
}
