using SmartAss;
using System.Collections.Generic;
using System.Linq;
using Int = System.Numerics.BigInteger;

namespace Advent_of_Code_2019.IntComputing
{
    public class RunArguments
    {
        public static RunArguments Empty() => new RunArguments();

        public RunArguments(params Int[] inputs)
            : this(false, inputs) => Do.Nothing();

        public RunArguments(bool haltOnOutput, params Int[] inputs)
        {
            HaltOnOutput = haltOnOutput;
            Inputs = inputs.ToArray();
        }

        public bool HaltOnOutput { get; }
        public IReadOnlyCollection<Int> Inputs { get; }
    }
}
