namespace Advent_of_Code_2016;

[Category(Category.ExpressionParsing)]
public class Day_21
{
    [Puzzle(answer: "hcdefbag", O.μs10)]
    public char[] part_one(Lines lines) => Execute([.. "abcdefgh"], lines, false);
    
    [Puzzle(answer: "fbhaegdc", O.μs10)]
    public char[] part_two(Lines lines) => Execute([.. "fbgdceah"], lines.Reversed(), true);

    static char[] Execute(char[] pass, IEnumerable<string> lines, bool reverse)
    {
        foreach (var line in lines)
            pass = Execute(pass, line, reverse);
        return pass;
    }

    static char[] Execute(char[] pass, string inst, bool reverse)
    {
        var parts = inst.SpaceSeparated();
        pass = parts[0] switch
        {
            "move" => Move(pass, Int(parts[2]), Int(parts[^1]), reverse),
            "reverse" => Reverse(pass, Int(parts[2]), Int(parts[^1])),
            "rotate" => parts[1] switch
            {
                "based" => RotateLetter(pass, Letter(parts[^1]), reverse),
                "right" => Rotate(pass, Int(parts[2]), reverse ? 1 : -1),
                /*left*/_ => Rotate(pass, Int(parts[2]), reverse ? -1 : +1),
            },
            /*swap*/_ => parts[1] switch
            {
                "position" => Swap(pass, Int(parts[2]), Int(parts[5])),
                /*letter*/_ => Swap(pass, Letter(parts[2]), Letter(parts[5])),
            },
        };
        return pass;
    }
   
    static char[] Move(char[] pass, Selector a, Selector b, bool reverse)
    {
        var (i, j) = (a(pass), b(pass));
        if (reverse) (i, j) = (j, i);
        var rem = pass[i];

        if (i < j)
            while (i < j) pass[i] = pass[++i];
        else
            while (j < i) pass[i] = pass[--i];

        pass[j] = rem;
        return pass;
    }

    static char[] Reverse(char[] pass, Selector a, Selector b)
    {
        var (i, j) = (a(pass), b(pass));

        while (i < j)
            (pass[i], pass[j]) = (pass[j--], pass[i++]);
        
        return pass;
    }

    static char[] RotateLetter(char[] pass, Selector a, bool reverse)
    => reverse
        ? Rotate(pass, ch => { var r = a(ch); return reverses[r]; }, +1)
        : Rotate(pass, ch => { var r = a(ch); return r >= 4 ? r + 2 : r + 1; }, -1);
    static readonly int[] reverses = [1, 1, 6, 2, 7, 3, 0, 4];

    static char[] Rotate(char[] pass, Selector a, int dir)
    {
        var step = a(pass) * dir;
        var next = new char[pass.Length];
        for (var i = 0; i < pass.Length; i++)
            next[i] = pass[(i + step).Mod(pass.Length)];
        return next;
    }

    static char[] Swap(char[] pass, Selector a, Selector b)
    {
        var (i, j) = (a(pass), b(pass));
        (pass[i], pass[j]) = (pass[j], pass[i]);
        return pass;
    }

    static Selector Letter(string c) => pass => pass.IndexOf(c[0]);

    static Selector Int(string i) => _ => i[0] - '0';

    delegate int Selector(char[] pass);

    [TestCase("abcde", "ebcda", "swap position 4 with position 0")]
    [TestCase("ebcda", "edcba", "swap letter d with letter b")]
    [TestCase("edcba", "abcde", "reverse positions 0 through 4")]
    [TestCase("abcde", "bcdea", "rotate left 1 step")]
    [TestCase("bcdea", "abcde", "rotate right 1 step")]
    [TestCase("bcdea", "bdeac", "move position 1 to position 4")]
    [TestCase("bdeac", "abdec", "move position 3 to position 0")]
    [TestCase("abdec", "ecabd", "rotate based on position of letter b")]
    [TestCase("ecabd", "decab", "rotate based on position of letter d")]
    public void Executes(string curr, string next, string cmd)
    {
        new string(Execute(curr.ToCharArray(), cmd, false)).Should().Be(next);
        new string(Execute(next.ToCharArray(), cmd, true)).Should().Be(curr);
    }
}
