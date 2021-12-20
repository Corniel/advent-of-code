namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_02
{
    [Example(answer: 150, "forward 5;down 5;forward 8;up 3;down 8;forward 2")]
    [Puzzle(answer: 2027977, year: 2021, day: 02)]
    public long part_one(string input)
    {
        var d = 0;
        var h = 0;

        foreach(var instruction in input.Lines(Instruction.Parse))
        {
            switch (instruction.Command)
            {
                case Command.down: d += instruction.Velocity; break;
                case Command.up: d -= instruction.Velocity; break;
                case Command.forward: h += instruction.Velocity; break;
            }
        }
        return h * d;
    }

    [Example(answer: 900, "forward 5;down 5;forward 8;up 3;down 8;forward 2")]
    [Puzzle(answer: 1903644897, year: 2021, day: 02)]
    public long part_two(string input)
    {
        var d = 0L;
        var h = 0L;
        var a = 0L;

        foreach (var instruction in input.Lines(Instruction.Parse))
        {
            switch (instruction.Command)
            {
                case Command.down: a += instruction.Velocity; break;
                case Command.up: a -= instruction.Velocity; break;
                case Command.forward:
                    h += instruction.Velocity;
                    d += a * instruction.Velocity;
                    break;
            }
        }
        return h * d;
    }

    private sealed record Instruction(Command Command, int Velocity)
    {
        public static Instruction Parse(string line)
        {
            var split = line.IndexOf(' ');
            return new(
                Command: (Command)Enum.Parse(typeof(Command), line[..split], true),
                Velocity: int.Parse(line[(split + 1)..]));
        }
    }
    private enum Command
    {
        forward,
        down,
        up,
    }
}
