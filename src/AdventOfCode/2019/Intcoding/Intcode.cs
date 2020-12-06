using System.Collections.Generic;

namespace AdventOfCode._2019.Intcoding
{

    public partial class Intcode
    {
        public Intcode(IList<int> memory)
        {
            Memory = memory;
        }

        public IList<int> Memory { get; }
        public Queue<int> Inputs { get; } = new Queue<int>();
        public ICollection<int> Outputs { get; } = new List<int>();
        public int Size => Memory.Count;
        public int Pointer { get; internal set; }

        public override string ToString() => $"Pointer: {Pointer}, {state}, {string.Join(',', Memory)}";

        public int Answer()
            => state switch
            {
                State.Exit => Memory[0],
                _ => throw new NoAnswer(),
            };

        public bool Halted() => state == State.Exit;

        private bool InMemory(int pointer) => pointer >= 0 && pointer < Size;
        private bool Running() => state == State.Running && Pointer < Size;

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
        private void Write(int pointer, int value)
        {
            if (InMemory(pointer))
            {
                Memory[pointer] = value;
            }
            else
            {
                state = State.OutOfMemory;
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