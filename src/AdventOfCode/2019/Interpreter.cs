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

        public static int Anwser(this IEnumerable<int> memory)
            => memory is null
            ? throw new NoAnswer()
            : memory.FirstOrDefault();
    }
}
