using Advent_of_Code;

namespace Advent_of_Code_2019.Intcoding
{
    public static class IntcodeExtensions
    {
        public static int Answer(this Intcode intcode)
            => intcode is null
            ? throw new NoAnswer()
            : intcode.Memory[0];
    }
}
