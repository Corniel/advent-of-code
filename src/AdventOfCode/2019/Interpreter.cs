using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Interpreter
    {
        public static IList<int> Run(this IList<int> memory)
        {
            var pointer = 0;
            var state = State.Running;

            while (state == State.Running)
            {
                var instruction = memory[pointer++].GetInstruction();
                state = instruction(
                    memory,
                    p0: memory[pointer++],
                    p1: memory[pointer++],
                    p2: memory[pointer++]);

                if (pointer + 4 >= memory.Count) state = State.Exit;
            }
            return state == State.Exit
                ? memory
                : null;
        }

        public static int Anwser(this IEnumerable<int> memory)
            => memory is null
            ? throw new NoAnswer()
            : memory.FirstOrDefault();

        internal enum State
        {
            OutOfMemory = -2,
            UnknownInstruction = -1,
            Exit = 0,
            Running = 1,
        }

        internal static bool OutOf(this IList<int> memory, int p0, int p1, int p2)
            => p0 < 0 || p0 >= memory.Count
            || p1 < 0 || p1 >= memory.Count
            || p2 < 0 || p2 >= memory.Count;

        internal static State Add(IList<int> memory, int p0, int p1, int p2)
        {
            if (memory.OutOf(p0, p1, p2)) return State.OutOfMemory;
            var l = memory[p0];
            var r = memory[p1];
            memory[p2] = l + r;
            return State.Running;
        }

        internal static State Multiply(IList<int> memory, int p0, int p1, int p2)
        {
            if (memory.OutOf(p0, p1, p2)) return State.OutOfMemory;
            var l = memory[p0];
            var r = memory[p1];
            memory[p2] = l * r;
            return State.Running;
        }

        internal static State Exit(IList<int> memory, int p0, int p1, int p2) => State.Exit;

        internal static State Unknown(IList<int> memory, int p0, int p1, int p2) => State.UnknownInstruction;

        internal delegate State Instruction(IList<int> memory, int p0, int p1, int p2);

        internal static Instruction GetInstruction(this int value)
            => value switch
            {
                1 => Add,
                2 => Multiply,
                99 => Exit,
                _ => Unknown,
            };
    }
}
