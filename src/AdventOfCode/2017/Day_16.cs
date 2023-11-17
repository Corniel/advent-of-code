namespace Advent_of_Code_2017;

[Category(Category.Simulation)]
public class Day_16
{
    [Example(answer: "cdeab", "s3", 5)]
    [Example(answer: "cdeab", "s3", 5)]
    [Example(answer: "baedc", "s1,x3/4,pe/b", 5)]
    [Puzzle(answer: "hmefajngplkidocb", null, 16, O.μs100)]
    public string part_one(string input, int size) => Simulate(input, size, 1);

    [Puzzle(answer: "fbidepghmjklcnoa", null, 16, O.ms)]
    public string part_two(string input, int size) => Simulate(input, size, 1_000_000_000);

    private string Simulate(string input, int size, int turns)
    {
        var moves = input.CommaSeparated().Select(Parse).ToArray();
        var period = Simulation.WithPeriod(
            initial: Characters.a_z[0..size].ToCharArray(),
            simulate: (d, _) => Dance(d, moves),
            getHash: d => new string(d),
            simulations: turns,
            out var dance);

        for (var i = 0; i < period.Remaining; i++) dance = Dance(dance, moves);

        return new(dance);
    }

    private static char[] Dance(char[] dance, object[] moves)
    {
        foreach (var move in moves)
        {
            switch (move)
            {
                case Spin s: dance = s.Move(dance); break;
                case Exchange x: x.Move(dance); break;
                case Partner p: p.Move(dance); break;
            }
        }
        return dance;
    }

    object Parse(string str) => str[0] switch
    {
        's' => new Spin(str.Int32()),
        'x' => new Exchange(str.Int32(), str.Int32s().Skip(1).First()),
        _ /* 'p' */ => new Partner(str[1], str[3]),
    };

    readonly struct Spin(int P)
    {
        public char[] Move(char[] dance)
        {
            var copy = new char[dance.Length];
            Array.Copy(dance, 0, copy, P, dance.Length - P);
            Array.Copy(dance, dance.Length - P, copy, 0, P);
            return copy;
        }
    }
    readonly struct Exchange(int F, int S)
    {
        public void Move(char[] dance)
        {
            var f_ = dance[S]; var s_ = dance[F];
            dance[F] = f_; dance[S] = s_;
        }
    }
    readonly struct Partner(char F, char S)
    {
        public void Move(char[] dance)
        {
            var f = dance.IndexOf(F); var s = dance.IndexOf(S);
            var f_ = dance[s]; var s_ = dance[f];
            dance[f] = f_; dance[s] = s_;
        }
    }
}
