using System.Linq;

namespace AdventOfCode._2020
{
    public class Day08
    {
        public static long One(string input)
        {
            var instructions = input.Lines().Select(Instruction.Parse).ToArray();
            Execute(instructions, -1, out var accumulator);
            return accumulator;
        }

        public static long Two(string input)
        {
            var instructions = input.Lines().Select(Instruction.Parse).ToArray();
            var accumulator = 0;

            for(var fix_pointer = 0; fix_pointer < instructions.Length; fix_pointer++)
            {
                if (instructions[fix_pointer].Name == "acc") { continue; }
                if (Execute(instructions, fix_pointer, out accumulator))
                {
                    return accumulator;
                }
            }
            throw new NoAnswer();
        }

        private static bool Execute(
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

                if (executed.Any(e => e > 1))
                {
                    return false;
                }

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

        public readonly struct Instruction
        {
            public Instruction(string name, int val)
            {
                Name = name;
                Value = val;
            }

            public string Name { get; }
            public int Value { get; }

            public static Instruction Parse(string line)
            {
                var parts = line.SpaceSeperated().ToArray();
                return new Instruction(parts[0], parts[1].Int32());
            }
        }
    }
}