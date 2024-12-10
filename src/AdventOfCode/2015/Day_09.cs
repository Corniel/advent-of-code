namespace Advent_of_Code_2015;

[Category(Category.Computation)]
public class Day_09
{
    const string Example = @"
London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

    [Example(answer: 605, Example)]
    [Puzzle(answer: 141, O.ms)]
    public int part_one(Lines lines) => Distances(lines).Min();

    [Example(answer: 982, Example)]
    [Puzzle(answer: 736, O.ms)]
    public int part_two(Lines lines) => Distances(lines).Max();

    static IEnumerable<int> Distances(Lines lines)
    {
        var routes = lines.ToArray(Route.Parse);
        var locations = routes.SelectMany(route => route.Locations).Distinct().Order().ToArray();
        var distances = new Grid<int>(locations.Length, locations.Length);

        foreach (var route in routes)
        {
            var from = locations.IndexOf(route.From);
            var to = locations.IndexOf(route.To);
            distances[from, to] = route.Distance;
            distances[to, from] = route.Distance;
        }

        return Range(0, locations.Length).Permutations().Select(permuation => GetDistance(permuation, distances));
    }

    static int GetDistance(int[] permuation, Grid<int> distances)
    {
        var total = distances[permuation[0], permuation[1]];
        for (var i = 1; i < permuation.Length - 1; i++)
        {
            total += distances[permuation[i], permuation[i + 1]];
        }
        return total;
    }

    record Route(string From, string To, int Distance)
    {
        public string[] Locations { get; } = [From, To];

        public static Route Parse(string line)
        {
            var parts = line.Separate(" to ", " = ");
            return new(parts[0], parts[1], parts[2].Int32());
        }
    }
}
