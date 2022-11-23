namespace Advent_of_Code_2018;

[Category(Category._4D)]
public class Day_25
{
    [Example(answer: 2, "0,0,0,0;3,0,0,0;0,3,0,0;0,0,3,0;0,0,0,3;0,0,0,6;9,0,0,0;12,0,0,0")]
    [Example(answer: 4, "-1,2,2,0;0,0,2,-2;0,0,0,-2;-1,2,0,0;-2,-2,-2,2;3,0,2,-1;-1,3,2,2;-1,0,-1,0;0,2,1,-2;3,0,0,0")]
    [Example(answer: 3, "1,-1,0,1;2,0,-1,0;3,2,-1,0;0,0,3,1;0,0,-1,-1;2,3,-2,0;-2,2,0,0;2,-2,0,-1;1,-1,0,-1;3,2,0,2")]
    [Example(answer: 8, "1,-1,-1,-2;-2,-2,0,1;0,2,1,3;-2,3,-2,1;0,2,3,-2;-1,-1,1,-2;0,-2,-1,0;-2,2,3,-1;1,2,2,0;-1,-2,0,-2")]
    [Puzzle(answer: 318, year: 2018, day: 25)]
    public long part_one(string input)
    {
        var points = input.Lines(Point4D.Parse).ToArray();
        var constellations = new List<Constellatetion>();

        foreach (var point in points)
        {
            var extra = new Constellatetion(point, points);

            if (constellations.Where(c => c.Contains(point)).ToArray() is { } existing && existing.Any())
            {
                existing.First().AddRange(extra.Concat(existing.Skip(1).SelectMany(c => c)));

                foreach (var c in existing.Skip(1)) { constellations.Remove(c); }
            }
            else { constellations.Add(extra); }
        }
        return constellations.Count;
    }

    [Puzzle(answer: "You only need 49 stars to boost it", input: "You only need 49 stars to boost it")]
    public string part_two(string input) => input;

    sealed class Constellatetion : HashSet<Point4D>
    {
        public Constellatetion(Point4D point, IEnumerable<Point4D> points)
            : base(points.Where(p => p.ManhattanDistance(point) <= 3)) { }
    }
}
