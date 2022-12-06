namespace Advent_of_Code_2016;

[Category(Category.Cryptography)]
public class Day_06
{
    [Example(answer: "easter", "eedadn;drvtee;eandsr;raavrd;atevrs;tsrnev;sdttsa;rasrtv;nssdts;ntnada;svetve;tesnvt;vntsnd;vrdear;dvrsen;enarar")]
    [Puzzle(answer: "mshjnduc")]
    public string part_one(string input) => Decrypt(input.Lines().ToArray(), Max);

    [Puzzle(answer: "apfeeebz")]
    public string part_two(string input) => Decrypt(input.Lines().ToArray(), Min);

    private static string Decrypt(string[] lines, Func<IEnumerable<char>, char> selector) 
        => new(Range(0, lines[0].Length).Select(i => selector(lines.Select(line => line[i]))).ToArray());

    static char Max(IEnumerable<char> chars) => new ItemCounter<char> { chars }.Max().Item;
    static char Min(IEnumerable<char> chars) => new ItemCounter<char> { chars }.Min().Item;
}
