using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Intcoding
{
    public partial class Intcode
    {
        public static Intcode Parse(string input)
        {
            var memory = input.CommaSeperated().SelectMany(Parser.Ints).ToArray();
            return new Intcode(memory);
        }

        public Intcode Copy() => new Intcode(Memory.ToArray());

        public Intcode Update(int pointer, int value)
        {
            Memory[pointer] = value;
            return this;
        }

        public Intcode Run()
        {
            while (Running())
            {
                var opcode = Opcode();

                switch (opcode.Instruction)
                {
                    case Instruction.Add: Add(opcode); break;
                    case Instruction.Multiply: Multiply(opcode); break;
                    case Instruction.Input: Input(opcode); break;
                    case Instruction.Output: Output(opcode); break;
                    case Instruction.Exit: Exit(); break;
                    default: Unknown(); break;
                }
            }
            return Succeeded() ?this : null;
        }

        public Intcode Add(Opcode opcode)
        {
            if (Read(out var p1) &&
                Read(out var p2) &&
                Read(out var target))
            {
                if (opcode.P1 == Mode.Position)
                {
                    Read(p1, out p1);
                }
                if (opcode.P2 == Mode.Position)
                {
                    Read(p2, out p2);
                }

                Write(target, p1 + p2);
            }
            return this;
        }

        public Intcode Multiply(Opcode opcode)
        {
            if (Read(out var p1) &&
                Read(out var p2) &&
                Read(out var target))
            {
                if (opcode.P1 == Mode.Position)
                {
                    Read(p1, out p1);
                }
                if (opcode.P2 == Mode.Position)
                {
                    Read(p2, out p2);
                }

                Write(target, p1 * p2);
            }
            return this;
        }

        public Intcode Input(Opcode opcode)
        {
            if (Read(out int p1))
            {
                var input = Inputs.Dequeue();
                Write(p1, input);
            }
            return this;
        }
        public Intcode Output(Opcode opcode)
        {
            if (Read(out int p1) && Read(p1, out int value))
            {
                Outputs.Add(value);
            }
            return this;
        }

        public Intcode Unknown()
        {
            Pointer = Size;
            state = State.Unknown;
            return this;
        }
        public Intcode Exit()
        {
            Pointer = Size;
            state = State.Exit;
            return this;
        }

        public Intcode Input()
        {
            if (Read(out var target))
            {
                Write(target, Inputs.Dequeue());
            }
            return this;
        }

        public Intcode Outout()
        {
            if (Read(out var output))
            {
                Outputs.Add(output);
            }
            return this;
        }

        private Opcode Opcode() => new Opcode(Memory[Pointer++]);
    }
}
