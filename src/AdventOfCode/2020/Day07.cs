using SmartAss.Trees;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Day07
    {
        public static int One(string input)
            => Bags.Parse(input).Values.Count(bag => bag.Search("shiny gold") != null) - 1;
        
        public static int Two(string input)
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