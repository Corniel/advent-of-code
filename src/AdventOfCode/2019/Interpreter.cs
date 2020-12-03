using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Interpreter
    {
        public static IList<int> Run(this Intcode code)
        {
            while (code.Running())
            {
                switch (code.Instruct())
                {
                    case Instruction.Add: code.Add(); break;
                    case Instruction.Multiply: code.Multipy(); break;
                    case Instruction.Exit: code.Exit(); break;
                    default: code.Unknown(); break;
                }
            }
            return code.Succeeded() ? code.Memory : null;
        }

        internal static Intcode Add(this Intcode code)
        {
            if (code.Read(out var p0) && 
                code.Read(out var p1) &&
                code.Read(out var target) &&
                code.Read(p0, out var l) &&
                code.Read(p1, out var r))
            {
                code.Write(target, l + r);
            }
            return code;
        }

        internal static Intcode Multipy(this Intcode code)
        {
            if (code.Read(out var p0) &&
                code.Read(out var p1) &&
                code.Read(out var target) &&
                code.Read(p0, out var l) &&
                code.Read(p1, out var r))
            {
                code.Write(target, l * r);
            }
            return code;
        }

        public static int Anwser(this IEnumerable<int> memory)
            => memory is null
            ? throw new NoAnswer()
            : memory.FirstOrDefault();
    }
}
