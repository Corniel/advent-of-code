using System.Numerics;

namespace SmartAss;

public static class SelectWithPreviousExtensions
{
    public static CurrentAndPreviouses SelectWithPrevious(this string str, int size = 2) => new(str, size);

    public static CurrentAndPreviouses<T> SelectWithPrevious<T>(this IEnumerable<T> items) => new(items);

    /// <summary>Gets an read-only collection of items, with item on the last slot being the newest.</summary>
    [Pure]
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

    public static T Delta<T>(this CurrentAndPrevious<T> pair) where T : struct, INumberBase<T>
        => pair.Current - pair.Previous;
}

public struct CurrentAndPreviouses : IEnumerator<string>, IEnumerable<string>
{
    private readonly string Str;
    private readonly int Size;
    private int Pos;

    public CurrentAndPreviouses(string str, int size)
    {
        Str = str;
        Size = size;
        Pos = -1;
    }
    public string Current { get; private set; }

    readonly object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (++Pos <= Str.Length - Size)
        {
            Current = Str[Pos..(Pos + Size)];
            return true;
        }
        else return false;
    }

    public IEnumerator<string> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;

    public void Reset() => throw new NotSupportedException();

    public void Dispose() { /* Nothing to dispose. */ }
}

public struct CurrentAndPreviouses<T> : IEnumerator<CurrentAndPrevious<T>>, IEnumerable<CurrentAndPrevious<T>>
{
    private readonly IEnumerator<T> Iterator;

    public CurrentAndPreviouses(IEnumerable<T> enumerable)
    {
        Iterator = enumerable.GetEnumerator();
        Current = Iterator.MoveNext() ? new CurrentAndPrevious<T>(default, Iterator.Current) : default;
    }

    public CurrentAndPrevious<T> Current { get; private set; }

    readonly object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (Iterator.MoveNext())
        {
            Current = new(Current.Current, Iterator.Current);
            return true;
        }
        else return false;
    }

    public IEnumerator<CurrentAndPrevious<T>> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;

    public void Reset() => throw new NotSupportedException();

    public void Dispose() { /* Nothing to dispose. */ }
}

public readonly struct CurrentAndPrevious<T>(T prev, T curr)
{
    public readonly T Previous = prev;
    public readonly T Current = curr;

    public bool Unchanged() => Current.Equals(Previous);

    public override string ToString() => $"Current: {Current}, Previous: {Previous}";
}
