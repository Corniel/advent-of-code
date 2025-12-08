namespace Advent_of_Code_2015;

[Category(Category.Computation)]
public class Day_13
{
    [Example(answer: 330, Example._1)]
    [Puzzle(answer: 664, O.μs100)]
    public int part_one(Inputs<Relation> relations) => FindHappiness(relations, 0);

    [Puzzle(answer: 640, O.ms)]
    public int part_two(Inputs<Relation> relations) => FindHappiness(relations, +1);

    static int FindHappiness(Inputs<Relation> relations, int neutral)
    {
        var people = relations.As(h => h.Obj).Distinct().Order().ToArray();
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

    public record Relation(string Obj, string Sub, int Val)
    {
        public static Relation Parse(string line)
        {
            var blocks = line[..^1].Separate(" would ", " happiness units by sitting next to ");
            return new(blocks[0], blocks[2], (blocks[1].StartsWith("lose ") ? -1 : +1) * blocks[1].SpaceSeparated().Last().Int32());
        }
    }
}
