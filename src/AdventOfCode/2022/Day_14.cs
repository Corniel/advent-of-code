namespace Advent_of_Code_2022;

[Category(Category.Grid)]
public class Day_14
{
    [Example(answer: 24, "498,4 -> 498,6 -> 496,6;503,4 -> 502,4 -> 502,9 -> 494,9")]
    [Puzzle(answer: 1016, O.ms)]
    public int part_one(Lines lines) => Area.Parse(lines, false).Drop();

    [Example(answer: 93, "498,4 -> 498,6 -> 496,6;503,4 -> 502,4 -> 502,9 -> 494,9")]
    [Puzzle(answer: 25402, O.ms10)]
    public int part_two(Lines lines) => Area.Parse(lines, true).Drop();

    record Area(HashSet<Point> Map, int Rock, int Bottom)
    {
        public static readonly Point Start = new(500, 0);

        public int Drop()
        {
            while (true)
            {
                var sand = Start;

                while (true)
                {
                    sand += Vector.S;

                    if (Block(sand))
                    {
                        if (!Block(sand + Vector.W)) sand += Vector.W;
                        else if (!Block(sand + Vector.E)) sand += Vector.E;
                        else
                        {
                            Map.Add(sand - Vector.S);
                            if (sand - Vector.S == Start) return Map.Count - Rock;
                            else break;
                        }
                    }
                    if (sand.Y == Bottom) return Map.Count - Rock;
                }
            }
            throw new InfiniteLoop();
        }

        bool Block(Point test) => Map.Contains(test);

        public static Area Parse(Lines lines, bool withBottom)
        {
            var map = new HashSet<Point>();
            foreach (var pair in lines.As(l => l.Separate(" -> ").Select(Point.Parse)).SelectMany(p => p.SelectWithPrevious()))
            {
                map.Add(pair.Current);
                map.AddRange(pair.Previous.Repeat((pair.Current - pair.Previous).Sign(), true).TakeWhile(c => c != pair.Current));
            }

            var max = map.Max().Y + (withBottom ? 3 : 0);
            map.AddRange(new Point(Start.X - max, max - 1).Repeat(Vector.E).Take(withBottom ? max * 2 : 0));

            return new(map, map.Count, max);
        }
    }
}
