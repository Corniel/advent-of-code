namespace Advent_of_Code_2017;

[Category(Category.Simulation)]
public class Day_25
{
    [Example(answer: 3, Example._1)]
    [Puzzle(answer: 4225, O.ms10)]
    public int part_one(GroupedLines groups)
    {
        var all = groups.Skip(1).Select(Instruction.Parse).ToArray();
        var inst = all[0];
        var curr = 0;
        var tape = new Dictionary<int, bool>();

        foreach (var _ in Range(0, groups[0][1].Int32()))
        {
            var sub = tape.GetValueOrDefault(curr) ? inst.True : inst.False;
            tape[curr] = sub.Value;
            curr += sub.Move;
            inst = all[sub.State];
        }
        return tape.Values.Count(true);
    }

    [Puzzle(answer: "You only need 49 stars to boost it", "You only need 49 stars to boost it")]
    public string part_two(string str) => str;

    record Instruction(Sub False, Sub True)
    {
        public static Instruction Parse(string[] lines) => new(
            False: new Sub(lines[2].Int32() == 1, lines[3].Contains("left") ? -1 : 1, lines[4][^2] - 'A'),
            True: new Sub(lines[6].Int32() == 1, lines[7].Contains("left") ? -1 : 1, lines[8][^2] - 'A'));
    }

    record Sub(bool Value, int Move, int State);
}
