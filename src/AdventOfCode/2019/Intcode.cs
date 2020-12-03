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
        public Queue<int> Input { get; }
        public ICollection<int> Output { get; } = new List<int>();
        public int Size => Memory.Count;

        public Intcode Copy() => new Intcode(Memory.ToArray());

        public Intcode Update(int pointer, int value)
        {
            Memory[pointer] = value;
            return this;
        }

        public bool Read(out int value)
        {
            if(InMemory(Pointer))
            {
                value = Memory[Pointer++];
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

        public void Unknown()
        {
            Pointer = Size;
            state = State.Unknown;
        }
        public void Exit()
        {
            Pointer = Size;
            state = State.Exit;
        }

        public Instruction Instruct()
            => Memory[Pointer++] switch
            {
                1 => Instruction.Add,
                2 => Instruction.Multiply,
                99 => Instruction.Exit,
                _ => Instruction.Unknown,
            };

        public static Intcode Parse(string input)
        {
            var memory = input.CommaSeperated().SelectMany(Parser.Ints).ToArray();
            return new Intcode(memory);
        }
    }
}