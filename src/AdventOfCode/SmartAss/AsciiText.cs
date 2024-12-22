namespace SmartAss;

public static class AsciiText
{
    public static string Parse(Grid<bool> grid, bool trim = false)
    {
        if (grid.Rows != 6) throw new ArgumentException("Should contain 6 rows", nameof(grid));
        try
        {
            char[] chs = [..new TextIterator(grid, trim).Select(bits => Lookup[bits])];
            return new string(chs).Trim();
        }
        catch (KeyNotFoundException)
        {
            throw new FormatException($"Not a valid ASCII text:\r\n{grid.ToString(b => b ? '█' : '░')}");
        }
    }

    static readonly Dictionary<ulong, char> Lookup = Init();

    static Dictionary<ulong, char> Init()
    {
        var grid = @"
░██░░███░░░██░░███░░████░████░░██░░█░░█░░███░░░██░█░░█░█░░░░█░░░██░░█░░██░░███░░░██░░███░░░███░░███░█░░█░█░░░██░░░██░░░██░░░█████░
█░░█░█░░█░█░░█░█░░█░█░░░░█░░░░█░░█░█░░█░░░█░░░░░█░█░█░░█░░░░██░████░█░█░░█░█░░█░█░░█░█░░█░█░░░░░░█░░█░░█░█░░░██░░░█░█░█░█░░░█░░░█░
█░░█░███░░█░░░░█░░█░███░░███░░█░░░░████░░░█░░░░░█░██░░░█░░░░██░████░█░█░░█░█░░█░█░░█░█░░█░█░░░░░░█░░█░░█░░█░█░█░░░█░░█░░░█░█░░░█░░
████░█░░█░█░░░░█░░█░█░░░░█░░░░█░██░█░░█░░░█░░░░░█░█░█░░█░░░░█░█░██░██░█░░█░███░░█░░█░███░░░██░░░░█░░█░░█░░█░█░█░█░█░░█░░░░█░░░█░░░
█░░█░█░░█░█░░█░█░░█░█░░░░█░░░░█░░█░█░░█░░░█░░█░░█░█░█░░█░░░░█░█░██░██░█░░█░█░░░░█░█░░█░█░░░░░█░░░█░░█░░█░░░█░░█░█░█░█░█░░░█░░█░░░░
█░░█░███░░░██░░███░░████░█░░░░░███░█░░█░░███░░██░░█░░█░████░█░░░██░░█░░██░░█░░░░░█░█░█░░█░███░░░░█░░░██░░░░█░░░█░█░█░░░█░░█░░████░".CharPixels().Grid(ch => ch == '█');

        var lookup = new Dictionary<ulong, char>();
        var ch = 'A';
        foreach (var bits in new TextIterator(grid, false))
        {
            lookup[bits] = ch++;
        }
        lookup[0] = ' ';
        return lookup;
    }

    private struct TextIterator(Grid<bool> grid, bool trim) : Iterator<ulong>
    {
        private readonly Grid<bool> Grid = grid;
        private bool Trim = trim;
        private int Col = 0;
        public ulong Current { get; private set; }

        public bool MoveNext()
        {
            TrimEmptyColumns();

            if (Grid.Cols - Col >= 4)
            {
                Current = 0;
                for (var row = 0; row < 6; row++)
                {
                    for (var d_c = 0; d_c < 5; d_c++)
                    {
                        Current <<= 1;
                        var col = Col + d_c;
                        if (col != Grid.Cols) { Current |= Grid[col, row] ? 1UL : 0UL; }
                    }
                }
                Col += 5;
                return true;
            }
            return false;
        }

        private void TrimEmptyColumns()
        {
            while (Trim)
            {
                for (var row = 0; row < 6; row++)
                {
                    if (Grid[Col, row])
                    {
                        Trim = false;
                        return;
                    }
                }
                Col++;
            }
        }

        public readonly void Dispose() => Do.Nothing();
        public void Reset() => throw new NotSupportedException();
    }
}
