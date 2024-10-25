using System.Diagnostics.Contracts;

namespace SmartAss.Navigation;

public static class CursorExtensions
{
    public static bool Add(this ISet<Point> set, Cursor cursor) => set.Add(cursor.Pos);

    [Pure]
    public static bool Contains(this ISet<Point> set, Cursor cursor) => set.Contains(cursor.Pos);

    [Pure]
    public static bool OnGrid<T>(this Grid<T> map, Cursor cursor) => map.OnGrid(cursor.Pos);

    [Pure]
    public static GridNeighbors Neighbors<T>(this Grid<T> map, Cursor cursor) => map.Neighbors[cursor.Pos];

    [Pure]
    public static T Val<T>(this Grid<T> map, Cursor cursor) => map[cursor.Pos];
}
