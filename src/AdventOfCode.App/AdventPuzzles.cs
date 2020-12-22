using SmartAss.Diagnostics;
using SmartAss.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    [DebuggerDisplay("Count = {Count}")]
    public class AdventPuzzles : IEnumerable<AdventPuzzle>
    {
        private readonly List<AdventPuzzle> items = new();

        public int Count => items.Count;

        public bool Contains(AdventDate date) => items.Any(puzzle => puzzle.Matches(date));

        public IEnumerable<AdventPuzzle> Matching(AdventDate date)
            => items
            .Where(puzzle => puzzle.Matches(date))
            .OrderBy(puzzle => puzzle.Date);

        public static AdventPuzzles Load() => Load(typeof(Puzzle).Assembly.GetExportedTypes());

        public static AdventPuzzles Load(IEnumerable<Type> types)
        {
            var puzzles = new AdventPuzzles();
            foreach(var type in types)
            {
                var match = Pattern.Match(type.FullName);
                if(match.Success)
                {
                    var year = match.Groups["year"].Value.Int32();
                    var day = match.Groups["day"].Value.Int32();
                    var part_one = type.GetMethod("part_one");
                    var part_two = type.GetMethod("part_two");
                    puzzles.items.Add(new AdventPuzzle(new AdventDate(year, day, 1), part_one));
                    puzzles.items.Add(new AdventPuzzle(new AdventDate(year, day, 2), part_two));
                }
            }
            return puzzles;
        }

        public IEnumerator<AdventPuzzle> GetEnumerator() => items.OrderBy(item => item.Date).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private static readonly Regex Pattern = new("^Advent_of_Code_(?<year>[0-9]{4}).Day_(?<day>[012][0-9])$", RegexOptions.Compiled);
    }
}
