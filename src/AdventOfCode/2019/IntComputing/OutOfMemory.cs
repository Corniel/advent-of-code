using SmartAss;
using System;

namespace Advent_of_Code_2019.IntComputing
{
    public class OutOfMemory : InvalidOperationException
    {
        public OutOfMemory() : base("Out of Int Computer memory.") => Do.Nothing();
    }
}
