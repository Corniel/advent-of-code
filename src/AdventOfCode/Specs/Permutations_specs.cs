using NUnit.Framework;
using System;

namespace Permutations_specs
{
    public class For
    {
        [Test]
        public void _2_has_2_unique_permuations()
        {
            var permuations = new[] { 0, 1, }.Permutations();
            var expected = new[]
            {
                new[]{ 0, 1 },
                new[]{ 1, 0 },
            };
            Assert.AreEqual(expected, permuations);
        }

        [Test]
        public void _3_has_6_unique_permuations()
        {
            var permuations = new[] { 0, 1, 2, }.Permutations();
            var expected = new[] 
            {
                new[]{ 0, 1, 2 },
                new[]{ 1, 0, 2 },
                new[]{ 2, 0, 1 },
                new[]{ 0, 2, 1 },
                new[]{ 1, 2, 0 },
                new[]{ 2, 1, 0 },
            };
            Assert.AreEqual(expected, permuations);
        }

        [Test]
        public void _4_has_24_unique_permuations()
        {
            var permuations = new[] { 0, 1, 2, 3, }.Permutations();
            var expected = new[]
            {
                new[]{ 0, 1, 2, 3 },
                new[]{ 1, 0, 2, 3 },
                new[]{ 2, 0, 1, 3 },
                new[]{ 0, 2, 1, 3 },
                new[]{ 1, 2, 0, 3 },
                new[]{ 2, 1, 0, 3 },
                new[]{ 3, 1, 2, 0 },
                new[]{ 1, 3, 2, 0 },
                new[]{ 2, 3, 1, 0 },
                new[]{ 3, 2, 1, 0 },
                new[]{ 1, 2, 3, 0 },
                new[]{ 2, 1, 3, 0 },
                new[]{ 3, 0, 2, 1 },
                new[]{ 0, 3, 2, 1 },
                new[]{ 2, 3, 0, 1 },
                new[]{ 3, 2, 0, 1 },
                new[]{ 0, 2, 3, 1 },
                new[]{ 2, 0, 3, 1 },
                new[]{ 3, 0, 1, 2 },
                new[]{ 0, 3, 1, 2 },
                new[]{ 1, 3, 0, 2 },
                new[]{ 3, 1, 0, 2 },
                new[]{ 0, 1, 3, 2 },
                new[]{ 1, 0, 3, 2 },
            };
            CollectionAssert.AreEqual(expected, permuations);
        }
    }
}
