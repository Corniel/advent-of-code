namespace Advent_of_Code_2020;

[Category(Category.Graph)]
public class Day_07
{
    [Example(answer: 4, Example._1)]
    [Puzzle(answer: 161, O.ms)]
    public int part_one(string str) => Bags.Parse(str).Values.Count(bag => bag.Search("shiny gold") is { }) - 1;

    [Example(answer: 032, Example._1)]
    [Example(answer: 126, Example._2)]
    [Puzzle(answer: 30899, O.μs100)]
    public int part_two(string str) => Bags.Parse(str)["shiny gold"].NestedCount;

    class Bags : Forrest<Leaf>
    {
        public static Bags Parse(string str)
        {
            var bags = new Bags();

            foreach (var line in str.Replace("bags", " ").Replace("bag", " ").Replace(".", "").Lines())
            {
                var split = line.Separate("contain");

                var bag = new Leaf(split[0]);
                bag = bags.TryAdd(bag);

                foreach (var sub in split[1].CommaSeparated())
                {
                    var parts = sub.SpaceSeparated().Fix();

                    if (int.TryParse(parts[0], out var repeats))
                    {
                        var other = new Leaf(string.Join(" ", parts[1..]));
                        other = bags.TryAdd(other);
                        bag.Leaves.Add(other, repeats);
                    }
                }
            }
            return bags;
        }
    }
}
