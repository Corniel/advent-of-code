namespace Advent_of_Code_2020;

[Category(Category.Simulation)]
public class Day_08
{
    [Example(answer: 5, "nop +0; acc +1; jmp +4; acc +3; jmp -3; acc -99; acc +1; jmp -4; acc +6")]
    [Puzzle(answer: 1584, O.μs100)]
    public int part_one(string input)
    {
        var instructions = input.Lines(Instruction.Parse).ToArray();
        Execute(instructions, -1, out var accumulator);
        return accumulator;
    }

    [Example(answer: 8, "nop +0; acc +1; jmp +4; acc +3; jmp -3; acc -99;acc +1; jmp -4; acc +6")]
    [Puzzle(answer: 920, O.ms)]
    public int part_two(string input)
    {
        var instructions = input.Lines(Instruction.Parse).ToArray();

        for (var fix_pointer = 0; fix_pointer < instructions.Length; fix_pointer++)
        {
            if (instructions[fix_pointer].Name == "acc") continue;
            if (Execute(instructions, fix_pointer, out var accumulator))
            {
                return accumulator;
            }
        }
        throw new NoAnswer();
    }

    static bool Execute(
        Instruction[] instructions,
        int fix_pointer,
        out int accumulator)
    {
        var executed = new int[instructions.Length];

        var pointer = 0;
        accumulator = 0;

        while (pointer >= 0 && pointer < instructions.Length)
        {
            executed[pointer]++;

            if (executed.Exists(e => e > 1)) { return false; }

            var instruction = instructions[pointer];

            if (fix_pointer == pointer)
            {
                instruction = instruction.Name == "jmp"
                    ? new Instruction("nop", instruction.Value)
                    : new Instruction("jum", instruction.Value);
            }

            switch (instruction.Name)
            {
                case "acc":
                    accumulator += instruction.Value;
                    pointer++;
                    break;
                case "jmp":
                    pointer += instruction.Value;
                    break;
                case "nop":
                    pointer++;
                    break;
            }
        }
        return true;
    }

    private record Instruction(string Name, int Value)
    {
        public static Instruction Parse(string line)
            => new(line[0..3], line[4..].Int32());
    }
}
