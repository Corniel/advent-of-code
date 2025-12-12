namespace Advent_of_Code_2018;

[Category(Category._4D)]
public class Day_25
{
    [Example(answer: 2, "0,0,0,0;3,0,0,0;0,3,0,0;0,0,3,0;0,0,0,3;0,0,0,6;9,0,0,0;12,0,0,0")]
    [Example(answer: 4, "-1,2,2,0;0,0,2,-2;0,0,0,-2;-1,2,0,0;-2,-2,-2,2;3,0,2,-1;-1,3,2,2;-1,0,-1,0;0,2,1,-2;3,0,0,0")]
    [Example(answer: 3, "1,-1,0,1;2,0,-1,0;3,2,-1,0;0,0,3,1;0,0,-1,-1;2,3,-2,0;-2,2,0,0;2,-2,0,-1;1,-1,0,-1;3,2,0,2")]
    [Example(answer: 8, "1,-1,-1,-2;-2,-2,0,1;0,2,1,3;-2,3,-2,1;0,2,3,-2;-1,-1,1,-2;0,-2,-1,0;-2,2,3,-1;1,2,2,0;-1,-2,0,-2")]
    [Puzzle(answer: 318, O.ms10)]
    public int part_one(Point4Ds points)
    {
        var constellations = new List<Constellatetion>();

        foreach (var point in points)
        {
            var extra = new Constellatetion(point, points);

            if (constellations.Where(c => c.Contains(point)).Fix() is { Length: > 0 } existing)
            {
                existing[0].AddRange(extra.Concat(existing[1..].SelectMany(c => c)));

                foreach (var c in existing[1..]) constellations.Remove(c);
            }
            else constellations.Add(extra);
        }
        return constellations.Count;
    }

    [Puzzle(answer: 50, "You only need 49 stars to boost it")]
    public int part_two(string _) => 50;

    sealed class Constellatetion(Point4D point, IEnumerable<Point4D> points)
        : HashSet<Point4D>(points.Where(p => p.ManhattanDistance(point) <= 3)) { }
}
