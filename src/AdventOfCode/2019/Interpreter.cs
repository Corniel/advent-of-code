using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Interpreter
    {
        public static IList<int> Run(this IList<int> memory)
        {
            var pointer = 0;
            var state = StateType.Running;

            while (state == StateType.Running)
            {
                var instruction = memory[pointer++].GetInstruction();
                state = instruction(
                    memory,
                    p0: memory[pointer++],
                    p1: memory[pointer++],
                    p2: memory[pointer++]);

                if (pointer + 4 >= memory.Count) state = StateType.Exit;
            }
            return state == StateType.Exit
                ? memory
                : null;
        }

        public static int Anwser(this IEnumerable<int> memory)
            => memory is null
            ? throw new NoAnswer()
            : memory.FirstOrDefault();

        internal readonly struct State
        {
            public static readonly State OutOfMemory = new State(-2, default, default);
            public static readonly State UnknownInstruction = new State(-1, default, default);
            public static readonly State Exit = new State(-1, default, default);

            public State(int type, int pointer, int output)
            {
                this.Type = type;
                this.Pointer = pointer;
                this.Output = output;
            }

            public int Type { get; }
            public int Pointer { get; }
            public int Output { get; }
        }

        internal static State Add(IList<int> memory, State state)
        {
            if (memory.OutOf(p0, p1, p2)) return StateType.OutOfMemory;
            var l = memory[p0];
            var r = memory[p1];
            memory[p2] = l + r;
            return StateType.Running;
        }

        internal static State Multiply(IList<int> memory, State state)
        {
            if (memory.OutOf(p0, p1, p2)) return StateType.OutOfMemory;
            var l = memory[p0];
            var r = memory[p1];
            memory[p2] = l * r;
            return StateType.Running;
        }

        internal static State Exit(IList<int> memory, State state) => State.Exit;

        internal static State Unknown(IList<int> memory, State state) => State.UnknownInstruction;

        internal delegate State Instruction(IList<int> memory, State state);

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
