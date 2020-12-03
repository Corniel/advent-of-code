using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Intcoding
{
    
    public partial class Intcode
    {
        public Intcode(IList<int> memory)
        {
            Memory = memory;
        }

        public IList<int> Memory { get; }
        public Queue<int> Inputs { get; }
        public ICollection<int> Outputs { get; } = new List<int>();
        public int Size => Memory.Count;
        public int Pointer { get; internal set; }

        public override string ToString() => $"Pointer: {Pointer}, {string.Join(',', Memory)}";

        private bool InMemory(int pointer) => pointer >= 0 && pointer < Size;
        private bool Running() => state == State.Running;
        private bool Succeeded() => state == State.Exit;
        private bool Read(out int value) => Read(Pointer++, out value);
        private bool Read(int pointer, out int value)
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
        private bool Write(int pointer, int value)
        {
            if (InMemory(pointer))
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

        private enum State
        {
            OutOfMemory = -2,
            Unknown = -1,
            Exit = 0,
            Running = 1,
        }

        private State state = State.Running;
    }
}