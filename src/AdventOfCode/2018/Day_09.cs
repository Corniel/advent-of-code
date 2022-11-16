namespace Advent_of_Code_2018;

public class Day_09
{
    [Example(answer: 00000_32, "9 players; last marble is worth 25 points")]
    [Example(answer: 000_8317, "10 players; last marble is worth 1618 points")]
    [Example(answer: 0_146373, "13 players; last marble is worth 7999 points")]
    [Example(answer: 000_2764, "17 players; last marble is worth 1104 points")]
    [Example(answer: 00_54718, "21 players; last marble is worth 6111 points")]
    [Example(answer: 00_37305, "30 players; last marble is worth 5807 points")]
    [Puzzle(answer: 398730, "438 players; last marble is worth 71626 points")]
    public long part_one(string input) => Simulate(input);

    [Puzzle(answer: 3349635509, "438 players; last marble is worth 71626 points")]
    public long part_two(string input) => Simulate(input, 100);

    private static long Simulate(string input, int factor = 1)
    {
        var numbers = input.Int32s().ToArray();
        var players = numbers[0];
        var last = numbers[1] * factor;
        var scores = new long[players];
        var circle = Marble.Zero();

        for (var i = 1; i <= last; i++)
        {
            if (i % 23 == 0)
            {
                var remove = circle.CCW.CCW.CCW.CCW.CCW.CCW.CCW;
                scores[(i - 1) % players] += remove.Value + i;
                remove.Remove();
                circle = remove.CW;
            }
            else circle = circle.Insert(i);
        }
        return scores.Max();
    }

    record Marble(int Value)
    {
        public static Marble Zero()
        {
            var circle = new Marble(0);
            circle.CW = circle;
            circle.CCW = circle;
            return circle;
        }

        public Marble CW { get; private set; }
        public Marble CCW { get; private set; }

        public Marble Insert(int value)
        {
            var add = new Marble(value);
            var l = CW; 
            var r = CW.CW;
            l.CW = add; 
            r.CCW = add;
            add.CCW = l; 
            add.CW = r;
            return add;
        }

        public void Remove()
        {
            var ccw = CCW;
            var cw = CW;
            CCW.CW = cw;
            CW.CCW = ccw;
        }
    }
}
