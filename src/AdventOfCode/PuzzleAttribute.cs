using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using SmartAss;
using System;
using System.Collections.Generic;

namespace Advent_of_Code
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PuzzleAttribute : NUnitAttribute, ITestBuilder, IImplyFixture
    {
        public PuzzleAttribute(object answer, string input)
        {
            Answer = answer;
            Input = input;
        }

        public PuzzleAttribute(object answer, int year, int day)
            : this(answer, Puzzle.Input(year, day)) => Do.Nothing();

        public object Answer { get; }
        public string Input { get; }

        /// <summary>
        /// Builds a single test from the specified method and context.
        /// </summary>
        /// <param name="method">The MethodInfo for which tests are to be constructed.</param>
        /// <param name="suite">The suite to which the tests will be added.</param>
        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            var parameters = new TestCaseParameters(new[] { Input })
            {
                ExpectedResult = Answer,
            };

            var test = new NUnitTestCaseBuilder().BuildTestMethod(method, suite, parameters);
            test.Name = TestName(method);
            yield return test;
        }

        protected virtual string TestName(IMethodInfo method)
            => $"answer is {Answer} for {method.Name.Replace("_", " ")}";
    }
}
