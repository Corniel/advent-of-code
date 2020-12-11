using System.Diagnostics;

namespace Advent_of_Code_2019.IntComputing
{
    public readonly struct Opcode
    {
        public Opcode(int val) => value = val;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int value;
        public int Instruction => value % 100;
        public Mode P1 => Mode(100);
        public Mode P2 => Mode(1_000);
        public Mode P3 => Mode(10_000);

        public override string ToString() => $"{value / 100:000}-{Instruction:00} {Name()}";

        private Mode Mode(int divide) => (Mode)((value / divide) % 10);

        private string Name()
            => Instruction switch
            {
                01 => "ADD",
                02 => "MUL",
                03 => "IN ",
                04 => "OUT",
                05 => "JP1",
                06 => "JP0",
                07 => "LT ",
                08 => "EQ ",
                09 => "REL",
                99 => "EXIT",
                _ => "???",
            };
    }
}
