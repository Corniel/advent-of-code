namespace Advent_of_Code_2016;

[Category(Category.Cryptography)]
public class Day_06
{
    [Example(answer: "easter", "eedadn;drvtee;eandsr;raavrd;atevrs;tsrnev;sdttsa;rasrtv;nssdts;ntnada;svetve;tesnvt;vntsnd;vrdear;dvrsen;enarar")]
    [Puzzle(answer: "mshjnduc", O.μs100)]
    public string part_one(Lines input) => Decrypt(input, Max);

    [Puzzle(answer: "apfeeebz", O.μs100)]
    public string part_two(Lines input) => Decrypt(input, Min);

    static string Decrypt(Lines lines, Func<IEnumerable<char>, char> selector) 
        => new(Range(0, lines[0].Length).Select(i => selector(lines.Select(line => line[i]))).ToArray());

    static char Max(IEnumerable<char> chars) => new ItemCounter<char> { chars }.Max().Item;
    static char Min(IEnumerable<char> chars) => new ItemCounter<char> { chars }.Min().Item;
}
