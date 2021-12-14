namespace SmartAss;

public static class LoopWithPrevious
{
    public static IEnumerable<string> SelectWithPrevious(this string str, int size = 2)
    {
        for (var i = 0; i <= str.Length - size; i++)
        {
            yield return str[i..(i + size)];
        }
    }

    public static IEnumerable<CurrentAndPrevious<T>> SelectWithPrevious<T>(this IEnumerable<T> items)
    {
        var iterator = items.GetEnumerator();
        iterator.MoveNext();
        var previous = iterator.Current;

        while (iterator.MoveNext())
        {
            yield return new(previous, iterator.Current);
            previous = iterator.Current;
        }
    }

    public static IEnumerable<IReadOnlyList<T>> SelectWithPrevious<T>(this IEnumerable<T> items, int size)
    {
        var iterator = items.GetEnumerator();
        var previous = new T[size];
        var init = 0;

        while (++init < size && iterator.MoveNext())
        {
            previous[init] = iterator.Current;
        }

        while (iterator.MoveNext())
        {
            var current = new T[size];
            Array.Copy(previous, 1, current, 0, size - 1);
            current[^1] = iterator.Current;
            yield return current;
            previous = current;
        }
    }
}

public readonly struct CurrentAndPrevious<T>
{
    public CurrentAndPrevious(T prev, T curr)
    {
        Previous = prev;
        Current = curr;
    }

    public readonly T Previous;
    public readonly T Current;

    public bool Unchanged() => Current.Equals(Previous);

    public override string ToString() => $"Current: {Current}, Previous: {Previous}";
}
