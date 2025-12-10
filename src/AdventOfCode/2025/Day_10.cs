using Microsoft.Z3;

namespace Advent_of_Code_2025;

/// <summary>
/// There are machines with lights, multiple switching buttons and energy levels.
///
/// Part one: Count the number of XOR operations that result in the lights configuration.
/// Part two: Count the number of ADD opererations that result in the energly level.
/// </summary>
[Category(Category.BitManupilation)]
public class Day_10
{
    [Example(answer: 2, "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}")]
    [Example(answer: 3, "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}")]
    [Example(answer: 2, "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}")]
    [Puzzle(answer: 509, O.ms)]
    public int part_one(Inputs<Machine> input) => input.Sum(One);

    [Example(answer: 10, "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}")]
    [Example(answer: 12, "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}")]
    [Example(answer: 11, "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}")]
    [Puzzle(answer: 20083, O.ms100)]
    public int part_two(Inputs<Machine> input) => input.Sum(Two);

    private static int One(Machine m)
    {
        var max = BinaryNumber.All(m.Buttons.Length);
        var bst = int.MaxValue;

        for (var c = BinaryNumber.Empty(m.Buttons.Length); c < max; c++)
        {
            if (c.Count >= bst) continue;

            var bin = m.Lights;

            for (var p = 0; p < m.Buttons.Length; p++)
                if (c.HasFlag(p)) bin ^= m.Buttons[p];

            if (bin.IsEmpty()) bst = c.Count;
        }

        return bst;
    }

    static int Two(Machine m)
    {
        var c = new Context(); 
        var s = c.MkOptimize();
        ArithExpr[] buts = [.. Indexes().Select(Const)];

        // No negative clicks.
        foreach (var but in buts) s.Add(but >= 0);

        // For each level, buttons should add up.
        foreach (var i in Range(0, m.Levels.Length)) s.Add(Equals(i));

        s.MkMinimize(c.MkAdd(buts));
        s.Check();
        return buts.Sum(b => ((IntNum)s.Model.Evaluate(b)).Int);

        // Buttons that contribute to the level of a given index.
        ArithExpr[] Buttons(int f) => [.. Indexes().Where(i => m.Buttons[i].HasFlag(f)).Select(i => buts[i])];

        // Level = ∑ buttons.
        BoolExpr Equals(int i) => c.MkEq(Num(m.Levels[i]), c.MkAdd(Buttons(i)));

        // The indexes of the buttons.
        IEnumerable<int> Indexes() => Range(0, m.Buttons.Length);

        IntExpr Const(int i) => c.MkIntConst($"but{i}");
        IntNum Num(int n) => c.MkInt(n);
    }

    public record Machine(BinaryNumber Lights, BinaryNumber[] Buttons, int[] Levels)
    {
        public static Machine Parse(string str)
        {
            var parts = str.Split(' ');
            var light = BinaryNumber.Parse(new([.. parts[0].Reverse()]), "#", ".");
            BinaryNumber[] buttons = [.. parts[1..^1].Select(p => But(p, light.Size)).OrderByDescending(b => b.Count)];
            return new(light, buttons, [.. parts[^1].Int32s()]);
        }
        static BinaryNumber But(string part, int size)
        {
            var button = BinaryNumber.Empty(size);
            foreach (var n in part.Int32s()) button = button.Flag(n);
            return button;
        }
    }
}
