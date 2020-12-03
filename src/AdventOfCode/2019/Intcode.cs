using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    
    public class Intcode
    {
        private enum State
        {
            OutOfMemory = -2,
            Unknown = -1,
            Exit = 0,
            Running = 1,
        }

        private State state = State.Running;

        public Intcode(IList<int> memory)
        {
            Memory = memory;
        }

        public int Pointer { get; internal set; }

        public IList<int> Memory { get; }
        public Queue<int> Inputs { get; }
        public ICollection<int> Outputs { get; } = new List<int>();
        public int Size => Memory.Count;

        public Intcode Copy() => new Intcode(Memory.ToArray());

        public Intcode Update(int pointer, int value)
        {
            Memory[pointer] = value;
            return this;
        }

        public Instruction Instruct()
            => Memory[Pointer++] switch
            {
                1 => Instruction.Add,
                2 => Instruction.Multiply,
                99 => Instruction.Exit,
                _ => Instruction.Unknown,
            };

        public bool Read(out int value) => Read(Pointer++, out value);

        public bool Read(int pointer, out int value)
        {
            if (InMemory(pointer))
            {
                value = Memory[pointer];
                return true;
            }
            else
            {
                value = default;
                state = State.OutOfMemory;
                return false;
            }
        }

        public bool Write(int pointer, int value)
        {
            if(InMemory(pointer))
            {
                Memory[pointer] = value;
                return true;
            }
            else
            {
                state = State.OutOfMemory;
                return false;
            }
        }

        public bool InMemory(int pointer) => pointer >= 0 && pointer < Size;

        public bool Running() => state == State.Running;
        public bool Succeeded() => state == State.Exit;

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

        public Intcode Add()
        {
            if (Read(out var p0) &&
                Read(out var p1) &&
                Read(out var target) &&
                Read(p0, out var l) &&
                Read(p1, out var r))
            {
                Write(target, l + r);
            }
            return this;
        }

        public Intcode Multipy()
        {
            if (Read(out var p0) &&
                Read(out var p1) &&
                Read(out var target) &&
                Read(p0, out var l) &&
                Read(p1, out var r))
            {
                Write(target, l * r);
            }
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

        public override string ToString() => $"Pointer: {Pointer}, {string.Join(',', Memory)}";

        public static Intcode Parse(string input)
        {
            var memory = input.CommaSeperated().SelectMany(Parser.Ints).ToArray();
            return new Intcode(memory);
        }
    }
}