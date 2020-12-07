using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Day07
    {
        public static int One(string input)
            => Bags.Parse(input).Values.Count(bag => bag.CanHold("shiny gold")) - 1;

        public static int Two(string input)
            => Bags.Parse(input)["shiny gold"].ContainCount;
        
        public class Bags : Dictionary<string, Bag>
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
                    var c = split[0];
                    if (!bags.TryGetValue(c, out var bag))
                    {
                        bag = new Bag(c);
                        bags[c] = bag;
                    }
                    foreach (var other in split[1].CommaSeperated())
                    {
                        var parts = other.SpaceSeperated().ToArray();
                        if (int.TryParse(parts[0], out var q))
                        {
                            var other_c = string.Join(" ", parts.Skip(1));
                            if (!bags.TryGetValue(other_c, out var other_bag))
                            {
                                other_bag = new Bag(other_c);
                                bags[other_c] = other_bag;
                            }
                            bag.Contains[other_bag] = q;
                        }
                    }
                }
                return bags;
            }
        }

        public class Bag
        {
            public Bag(string color) => Color = color;

            public string Color { get; }

            public override string ToString() => Color;

            public Dictionary<Bag, int> Contains { get; } = new();

            public bool CanHold(string color)
                => Contains.Any(kvp => kvp.Key.CanHold(color))
                || Color == color;

            public int ContainCount
                => Contains.Sum(bag => bag.Value)
                + Contains.Sum(bag => bag.Key.ContainCount * bag.Value);
        }
    }
}