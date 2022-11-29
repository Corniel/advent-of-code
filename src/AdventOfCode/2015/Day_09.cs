namespace Advent_of_Code_2015;

[Category(Category.Computation)]
public class Day_09
{
    private const string Example = @"
London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

    [Example(answer: 605, Example)]
    [Puzzle(answer: 141)]
    public int part_one(string input) => Distances(input).Min();

    [Example(answer: 982, Example)]
    [Puzzle(answer: 736)]
    public int part_two(string input) => Distances(input).Max();

    private IEnumerable<int> Distances(string input)
    {
        var routes = input.Lines(Route.Parse).ToArray();
        var locations = routes.SelectMany(route => route.Locations).Distinct().OrderBy(l => l).ToArray();
        var distances = new Grid<int>(locations.Length, locations.Length);

        foreach (var route in routes)
        {
            var from = Array.IndexOf(locations, route.From);
            var to = Array.IndexOf(locations, route.To);
            distances[from, to] = route.Distance;
            distances[to, from] = route.Distance;
        }

        return Enumerable.Range(0, locations.Length).ToArray()
            .Permutations().Select(permuation => Distance(permuation, distances));
    }

    private int Distance(int[] permuation, Grid<int> distances)
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
        public string[] Locations { get; } = new[] { From, To };

        public static Route Parse(string line)
        {
            var parts = line.Seperate(" to ", " = ");
            return new(parts[0], parts[1], parts[2].Int32());
        }
    }
}
