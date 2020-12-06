using System.Linq;

namespace AdventOfCode._2020
{
    public class Day06
    {
        public static int One(string input)
            => input
            .GroupedLines()
            .Sum(group => Characters.a_z
                .Count(ch => group.Any(member => member.Contains(ch))));
            
        public static int Two(string input)
              => input
            .GroupedLines()
            .Sum(group => Characters.a_z
                .Count(ch => group.All(member => member.Contains(ch))));
    }
}