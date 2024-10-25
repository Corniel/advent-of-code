namespace SmartAss;

public static class AoCGridExtensions
{
    public static string AsciiText(this Grid<bool> grid, bool trim = false) => SmartAss.AsciiText.Parse(grid, trim);
}
