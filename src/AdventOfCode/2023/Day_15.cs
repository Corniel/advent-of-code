namespace Advent_of_Code_2023;

[Category(Category.ExpressionParsing)]
public class Day_15
{
    [Example(answer: 52, "HASH")]
    [Example(answer: 1320, "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7")]
    [Puzzle(answer: 511215, O.μs10)]
    public int part_one(string str) => str.CommaSeparated(Hash).Sum();

    [Example(answer: 145, "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7")]
    [Puzzle(answer: 236057, O.μs100)]
    public int part_two(string str)
    {
        var boxes = Range(0, 256).Fix(_ => new List<Lens>());

        foreach (var step in str.CommaSeparated(Parse))
        {
            var box = boxes[step.Index];
            var pos = box.FindIndex(b => b.Name == step.Name);

            if (step.Len != 0)
            {
                var lens = new Lens(step.Name, step.Len);
                if (pos == -1) box.Add(lens);
                else box[pos] = lens;
            }
            else if (pos != -1) { box.RemoveAt(pos); }
        }
        return boxes.Select(Score).Sum();
    }

    static Step Parse(string str)
    {
        var split = str.Split('=', '-');
        return new(split[0], Hash(split[0]), split[1].Int32N() ?? 0);
    }

    static int Hash(string str)
    {
        var val = 0;
        foreach (var ch in str) { val = (val + ch) * 17; }
        return val & 255;
    }

    static int Score(List<Lens> box, int i) => (i + 1) * box.Select(Score).Sum();

    static int Score(Lens lens, int i) => lens.Length * (i + 1);

    record Step(string Name, int Index, int Len);

    record Lens(string Name, int Length);
}
