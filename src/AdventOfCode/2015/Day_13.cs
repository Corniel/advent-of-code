namespace Advent_of_Code_2015;

[Category(Category.Computation)]
public class Day_13
{
    [Example(answer: 330, @"
Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.")]
    [Puzzle(answer: 664, O.μs100)]
    public int part_one(Lines lines) => FindHappiness(lines);

    [Puzzle(answer: 640, O.ms)]
    public int part_two(Lines lines) => FindHappiness(lines, +1);

    static int FindHappiness(Lines lines, int neutral = 0)
    {
        var relations = lines.ToArray(Relation.Parse);
        var people = relations.Select(h => h.Obj).Distinct().Order().ToArray();
        var likes = new Grid<int>(people.Length + neutral, people.Length + neutral);

        foreach (var relation in relations)
        {
            var obj = people.IndexOf(relation.Obj);
            var sub = people.IndexOf(relation.Sub);
            likes[obj, sub] = relation.Val;
        }
        return Range(1, people.Length + neutral - 1)
            .Permutations().Where(p => p[0] < p[1]).Select(permuation => Happiness(permuation, likes)).Max();
    }

    static int Happiness(int[] permuation, Grid<int> likes)
        => Happiness(0, permuation[0], likes)
        + Happiness(0, permuation[^1], likes)
        + permuation.SelectWithPrevious().Sum(pair => Happiness(pair.Current, pair.Previous, likes));

    static int Happiness(int l, int r, Grid<int> likes) => likes[l, r] + likes[r, l];

    record Relation(string Obj, string Sub, int Val)
    {
        public static Relation Parse(string line)
        {
            var blocks = line[..^1].Separate(" would ", " happiness units by sitting next to ");
            return new(blocks[0], blocks[2], (blocks[1].StartsWith("lose ") ? -1 : +1) * blocks[1].SpaceSeparated().Last().Int32());
        }
    }
}
