using System;
using System.Linq;

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

        public Intcode Run(params int[] inputs)
        {
            Inputs.Clear();
            foreach(var input in inputs)
            {
                Inputs.Enqueue(input);
            }

            state = State.Running;

            while (Running())
            {
                Step(Opcode());
            }
            return this;
        }
        
        private Intcode Step(Opcode opcode)
            => opcode.Instruction switch
            {
                Instruction.Add => Add(opcode),
                Instruction.Multiply => Multiply(opcode),
                Instruction.Input => Input(),
                Instruction.Output => Output(),
                Instruction.JumpIfFalse => JumpIf(false, opcode),
                Instruction.JumpIfTrue => JumpIf(true, opcode),
                Instruction.LessThen => LessThen(opcode),
                Instruction.Equals => Equals(opcode),
                Instruction.Exit => Exit(),
                _ => Unknown(),
            };

        public Intcode Add(Opcode opcode)
        => Execute(opcode, (p1, p2) => p1 + p2);

        public Intcode Multiply(Opcode opcode)
            => Execute(opcode, (p1, p2) => p1 * p2);

        public Intcode LessThen(Opcode opcode)
            => Execute(opcode, (p1, p2) => p1 < p2 ? 1 : 0);

        public Intcode Equals(Opcode opcode)
            => Execute(opcode, (p1, p2) => p1 == p2 ? 1 : 0);

        public Intcode JumpIf(bool condition, Opcode opcode)
        {
            if (Read(out var p1) &&
                Read(out var target))
            {
                if (opcode.P1 == Mode.Position)
                {
                    Read(p1, out p1);
                }
                if (opcode.P2 == Mode.Position)
                {
                    Read(target, out target);
                }
                if ((p1 != 0) == condition)
                {
                    if (InMemory(target))
                    {
                        Pointer = target;
                    }
                    else state = State.OutOfMemory;
                }
            }
            return this;
        }

        public Intcode Input()
        {
            if (Read(out int p1))
            {
                var input = Inputs.Dequeue();
                Write(p1, input);
            }
            return this;
        }
        
        public Intcode Output()
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

        private Opcode Opcode() => new Opcode(Memory[Pointer++]);

        private Intcode Execute(Opcode opcode, Func<int, int, int> instruction)
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

                Write(target, instruction(p1, p2));
            }
            return this;
        }
    }
}
