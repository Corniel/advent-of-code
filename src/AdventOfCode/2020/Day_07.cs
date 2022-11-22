using SmartAss.Trees;

namespace Advent_of_Code_2020;

[Category(Category.Graph)]
public class Day_07
{
    [Example(answer: 4, year: 2020, day: 07, example: 1)]
    [Puzzle(answer: 161, year: 2020, day: 07)]
    public int part_one(string input)
        => Bags.Parse(input).Values.Count(bag => bag.Search("shiny gold") != null) - 1;

    [Example(answer: 032, year: 2020, day: 07, example: 1)]
    [Example(answer: 126, year: 2020, day: 07, example: 2)]
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