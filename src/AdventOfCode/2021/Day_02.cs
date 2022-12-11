namespace Advent_of_Code_2021;

[Category(Category.μs, Category.VectorAlgebra)]
public class Day_02
{
    [Example(answer: 150, "forward 5;down 5;forward 8;up 3;down 8;forward 2")]
    [Puzzle(answer: 2027977)]
    public long part_one(string input) => new Submarine().Transpose(input.Lines(Instruction.Parse), (s, i) => s.One(i)).Last().Produces;

    [Example(answer: 900, "forward 5;down 5;forward 8;up 3;down 8;forward 2")]
    [Puzzle(answer: 1903644897)]
    public long part_two(string input) => new Submarine().Transpose(input.Lines(Instruction.Parse), (s, i) => s.Two(i)).Last().Produces;

    record Submarine(long Depth = 0, long Height = 0, long Aim = 0 ) 
    {
        public long Produces => Height * Depth;
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
