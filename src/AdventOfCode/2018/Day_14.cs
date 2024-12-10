namespace Advent_of_Code_2018;

[Category(Category.SequenceProgression)]
public class Day_14
{
    [Example(answer: 0124515891, 5)]
    [Example(answer: 1589167792, 10)]
    [Puzzle(answer: 02107929416, 556061, O.ms10)]
    public int part_one(int runs)
        => Digits.ToInt32(Run(r => r.Count < runs + 10).Skip(runs).Take(10));

    [Example(answer: 9, 51589)]
    [Example(answer: 2018, 59414)]
    [Puzzle(answer: 20307394, 556061, O.ms100)]
    public int part_two(int number)
    {
        var digits = number.Digits();
        var recipes = Run(r => r.Count <= digits.Count || !(EndsWith(r, digits, 0) || EndsWith(r, digits, 1)));
        return recipes.Count - digits.Count - (EndsWith(recipes, digits, 0) ? 0 : 1);
    }

    static List<int> Run(Predicate<List<int>> @while)
    {
        var digits = 37.Digits();
        var recipes = digits.ToList();
        var e1 = 0; var e2 = 1;

        while (@while(recipes))
        {
            var v1 = recipes[e1];
            var v2 = recipes[e2];

            recipes.AddRange((v1 + v2).Digits());

            e1 = (e1 + v1 + 1).Mod(recipes.Count);
            e2 = (e2 + v2 + 1).Mod(recipes.Count);
        }
        return recipes;
    }

    static bool EndsWith(List<int> recipes, IReadOnlyCollection<int> digits, int shorten)
    {
        var p = recipes.Count - digits.Count - shorten;
        return digits.All(d => recipes[p++] == d);
    }
}
