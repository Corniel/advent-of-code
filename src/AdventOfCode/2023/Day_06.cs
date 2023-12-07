namespace Advent_of_Code_2023;

[Category(Category.SequenceProgression)]
public class Day_06
{
    [Example(answer: 288, "Time: 7 15 30; Distance: 9 40 200")]
    [Puzzle(answer: 252000, O.ns100)]
    public int part_one(Longs numbers) => Races(numbers).Product(r => r.Further());


    [Example(answer: 71503, "Time: 7 15 30; Distance: 9 40 200")]
    [Puzzle(answer: 36992486, O.ns100)]
    public int part_two(string str) => part_one(new(str.StripChars(" ").Int64s().ToArray()));

    IEnumerable<Race> Races(IReadOnlyList<long> ns)
            => Range(0, ns.Count / 2).Select(n => new Race(ns[n], ns[n + ns.Count / 2]));

    record Race(long Time, long Distance)
    {
        /// <summary>
        /// We want to beat the distance:
        /// 
        ///  d + 1 = h * (t - h)
        ///  
        ///  0 = h² - h·t + d + 1
        /// 
        ///      h ± √ t² - 4·(d + 1)
        ///  t = --------------------
        ///               2
        ///               
        ///  2t = h ± √ t² - 4·(d + 1)
        ///  
        /// </summary>
        /// <remarks>
        /// Brute-force (used for the leaderboard):
        /// 
        /// public int Breaks()
        /// {
        ///     var holds = 0;
        ///     for (var h = 0; h < int.MaxValue; h++)
        ///     {
        ///         if (h * (Time - h) > Distance) holds++;
        ///         else if (holds != 0) break;
        ///     }
        /// 
        ///     return holds;
        /// }
        /// </remarks>
        public int Further()
        {
            var D = (Time.Sqr() - 4 * (Distance + 1)).Sqrt() / 2;
            var min = (Time / 2d - D).Ceil();
            var max = (Time / 2d + D).Floor();
            return max - min + 1;
        }
    }
}
