namespace Advent_of_Code;

internal static class Static
{
    public static T[] array<T>(params IEnumerable<T> items) => [.. items];
    public static Queue<T> queue<T>(params IEnumerable<T> items) => new(items);
}
