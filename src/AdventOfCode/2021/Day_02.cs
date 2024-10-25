namespace Advent_of_Code_2021;

[Category(Category.VectorAlgebra)]
public class Day_02
{
    [Example(answer: 150, "forward 5;down 5;forward 8;up 3;down 8;forward 2")]
    [Puzzle(answer: 2027977, O.μs10)]
    public int part_one(Lines lines) => new Submarine().Transpose(lines.As(Instruction.Parse), (s, i) => s.One(i)).Last().Produces;

    [Example(answer: 900, "forward 5;down 5;forward 8;up 3;down 8;forward 2")]
    [Puzzle(answer: 1903644897, O.μs10)]
    public int part_two(Lines lines) => new Submarine().Transpose(lines.As(Instruction.Parse), (s, i) => s.Two(i)).Last().Produces;

    record Submarine(int Depth = 0, int Height = 0, int Aim = 0) 
    {
        public int Produces => Height * Depth;
        public Submarine One(Instruction i) => i.Type switch
        {
            'd' => this with { Depth = Depth + i.Velocity },
            'u' => this with { Depth = Depth - i.Velocity },
            _ => this with { Height = Height + i.Velocity }
        };

        public Submarine Two(Instruction i) => i.Type switch
        {
            'd' => this with { Aim = Aim + i.Velocity },
            'u' => this with { Aim = Aim - i.Velocity },
            _ => this with { Height = Height + i.Velocity, Depth = Depth + Aim * i.Velocity }
        };
    }

    record Instruction(char Type, int Velocity)
    {
        public static Instruction Parse(string line) => new(line[0], line.Int32());
    }
}
