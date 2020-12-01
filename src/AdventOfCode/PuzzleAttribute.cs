using System;

namespace AdventOfCode
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PuzzleAttribute : Attribute
    {
        public PuzzleAttribute(int year, int day, Part part)
        {
            Year = year;
            Day = day;
            Part = part;
        }

        public int Year { get; }
        public int Day { get; }
        public Part Part { get; }
        public override string ToString() => $"{Year} day {Day} part {Part}";
    }

    public enum Part
    {
        one = 1,
        two = 2,
    }
}
