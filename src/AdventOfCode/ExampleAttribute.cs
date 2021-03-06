﻿using NUnit.Framework.Interfaces;
using SmartAss;
using System;

namespace Advent_of_Code
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExampleAttribute : PuzzleAttribute
    {
        public ExampleAttribute(object answer, string input)
            : base(answer, input) => Do.Nothing();
        public ExampleAttribute(object answer, int year, int day, int example)
           : base(answer, Puzzle.Input(year, day, example)) => Do.Nothing();

        protected override string TestName(IMethodInfo method)
           => $"answer is {Answer} for {method.Name.Replace("_", " ")} example with length {Input.Length}";
    }
}
