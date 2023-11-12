using Qowaiv.IO;

namespace Advent_of_Code.Rankings;

public record class LinesOfCode(FileInfo Location, AdventDate Date)
{
    public StreamSize Size => Process().size;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int size;

    public bool Exists => Location.Exists;

    public int LoC => Process().loc;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int loc;

    public int Lines => Process().lines;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int lines;

    private LinesOfCode Process()
    {
        if (lines != 0) return this;

        string line;
        using var reader = Location.OpenText();
        while ((line = reader.ReadLine()) != null)
        {
            line = line.Trim();
            if (line.Length == 0) continue;
            lines++;
            if (line.StartsWith("//")
                || line.StartsWith("namespace ")
                || line.StartsWith("public class Day_")
                || line.StartsWith('[')
                || line == "{"
                || line == "}"
                || line == "};") continue;
            loc++;
            size += line.Length;
        }
        return this;
    }

    public override string ToString() => Invariant($"{Date}: {LoC,3} LoC, {Size,9:0.00 kB}");
}
