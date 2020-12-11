using SmartAss;
using System;

namespace Advent_of_Code_2019.IntComputing
{
    public class UnknownInstruction : InvalidOperationException
    {
        public UnknownInstruction(string message) : base(message) => Do.Nothing();
        public static UnknownInstruction For(int code) => new UnknownInstruction($"Instruction {code:00} is unknown.");
    }
}
