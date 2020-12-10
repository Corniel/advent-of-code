using Advent_of_Code;
using SmartAss.Trees;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_07
    {
        [Example(answer: 4, @"
            light red bags contain 1 bright white bag, 2 muted yellow bags.
            dark orange bags contain 3 bright white bags, 4 muted yellow bags.
            bright white bags contain 1 shiny gold bag.
            muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
            shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
            dark olive bags contain 3 faded blue bags, 4 dotted black bags.
            vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
            faded blue bags contain no other bags.
            dotted black bags contain no other bags.")]
        [Puzzle(answer: 161, year: 2020, day: 07)]
        public int part_one(string input)
            => Bags.Parse(input).Values.Count(bag => bag.Search("shiny gold") != null) - 1;
            
        [Example(answer: 32, @"
            light red bags contain 1 bright white bag, 2 muted yellow bags.
            dark orange bags contain 3 bright white bags, 4 muted yellow bags.
            bright white bags contain 1 shiny gold bag.
            muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
            shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
            dark olive bags contain 3 faded blue bags, 4 dotted black bags.
            vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
            faded blue bags contain no other bags.
            dotted black bags contain no other bags.")]
        [Example(answer: 126, @"
            shiny gold bags contain 2 dark red bags.
            dark red bags contain 2 dark orange bags.
            dark orange bags contain 2 dark yellow bags.
            dark yellow bags contain 2 dark green bags.
            dark green bags contain 2 dark blue bags.
            dark blue bags contain 2 dark violet bags.
            dark violet bags contain no other bags.")]
        [Puzzle(answer: 30899, year: 2020, day: 07)]
        public int part_two(string input)
            => Bags.Parse(input)["shiny gold"].NestedCount;
            
        public class Bags : Forrest<Leaf>
        {
            public static Bags Parse(string input)
            {
                var bags = new Bags();

                foreach (var line in input
                    .Replace("bags", " ")
                    .Replace("bag", " ")
                    .Replace(".", "")
                    .Lines())
                {
                    var split = line.Seperate("contain");

                    var bag = new Leaf(split[0]);
                    bag = bags.TryAdd(bag);

                    foreach (var sub in split[1].CommaSeperated())
                    {
                        var parts = sub.SpaceSeperated().ToArray();

                        if (int.TryParse(parts[0], out var repeats))
                        {
                            var other = new Leaf(string.Join(" ", parts.Skip(1)));
                            other = bags.TryAdd(other);
                            bag.Leaves.Add(other, repeats);
                        }
                    }
                }
                return bags;
            }
        }
    }
}