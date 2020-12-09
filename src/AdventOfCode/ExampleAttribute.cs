using NUnit.Framework.Interfaces;
using SmartAss;
using System;

namespace Advent_of_Code
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExampleAttribute : PuzzleAttribute
    {
        public ExampleAttribute(object answer, string input)
            : base(answer, input) => Do.Nothing();

        protected override string TestName(IMethodInfo method)
            => $"answer is {Answer} for {method.Name.Replace("_", " ")} example:{Trimmed()}";

        private string Trimmed() => string.Join(Environment.NewLine,
            Input.Lines(StringSplitOptions.TrimEntries));
    }
}
