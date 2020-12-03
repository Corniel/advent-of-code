using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Intcoding
{
    public readonly struct Opcode
    {
        public Opcode(int val) => value = val;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int value;

        public Instruction Instruction => (Instruction)(value % 100);
        public Mode P1 => Mode(100);
        public Mode P2 => Mode(1_000);
        public Mode P3 => Mode(10_000);

        public override string ToString() => $"{value / 100:000} {Instruction}";

        private Mode Mode(int devide) => (Mode)((value / 100) % 10);
    }
}
