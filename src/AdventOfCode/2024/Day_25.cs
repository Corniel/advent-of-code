namespace Advent_of_Code_2024;

[Category(Category.Grid, Category.BitManupilation)]
public class Day_25
{
    [Example(answer: 3, Example._1)]
    [Puzzle(answer: 2691, O.μs100)]
    public int part_one(CharGrid map)
    {
        var lcks = new List<int>();
        var keys = new List<int>();
        var off = 0;
        do
        {
            var m = 0; var f = 1;
            for (var c = 0; c < 5; c++)
            {
                for (var r = 1; r <= 5; r++)
                {
                    if (map[c, r + off] is '#') m |= f;
                    f <<= 1;
                }
            }
            (map[0, off] is '#' ? lcks : keys).Add(m);
            off += 7;
        }
        while (off < map.Rows);

        return keys.Sum(k => lcks.Count(l => (l & k) == 0));
    }

    [Puzzle(answer: "You have enough stars to deliver the Chronicle.")]
    public string part_two(string str) => "You have enough stars to deliver the Chronicle.";
}
