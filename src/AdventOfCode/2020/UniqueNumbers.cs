namespace Advent_of_Code_2020;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count: {Count}, Min: {Minimum}, Max: {Maximum}")]
public class UniqueNumbers : IEnumerable<int>
{
    private readonly byte[] contains = new byte[short.MaxValue];
    private const byte True = 255;

    public static UniqueNumbers Empty => new();

    private UniqueNumbers() => Do.Nothing();

    public UniqueNumbers(IEnumerable<int> numbers)
    {
        var minimum = int.MaxValue;
        var maximum = int.MinValue;
        var count = 0;

        foreach (var item in numbers)
        {
            contains[item] = True;
            count++;
            if (item > maximum)
            {
                maximum = item;
            }
            if (item < minimum)
            {
                minimum = item;
            }
        }
        Minimum = minimum;
        Maximum = maximum;
        Count = count;
    }

    public int Minimum { get; private set; }
    public int Maximum { get; private set; }
    public int Count { get; private set; }

    public bool Contains(int number) => contains[number] == True;

    public bool Add(int number)
    {
        var added = !Contains(number);
        if (added)
        {
            contains[number] = True;
            if (Count++ == 0)
            {
                Minimum = number;
                Maximum = number;
            }
            else
            {
                if (number < Minimum)
                {
                    Minimum = number;
                }
                else if (number > Maximum)
                {
                    Maximum = number;
                }
            }
        }
        return added;
    }

    public void Clear()
    {
        Array.Clear(contains, Minimum, Maximum - Minimum + 1);
        Count = 0;
        Minimum = 0;
        Maximum = 0;
    }

    public IEnumerable<int> Range(int min = 0, int max = 0)
        => new Enumerator(
            contains: contains,
            min: min == default ? Minimum : min,
            max: max == default ? Maximum : max);

    public IEnumerator<int> GetEnumerator() => new Enumerator(contains, Minimum, Maximum);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static UniqueNumbers Parse(string str) => new(str.Int32s());

    private sealed class Enumerator(byte[] contains, int min, int max) : IEnumerator<int>, IEnumerable<int>
    {
        private readonly byte[] contains = contains;
        private readonly int max = max;
        private int index = min - 1;

        public int Current => index;

        object IEnumerator.Current => Current;

        public void Dispose() => Do.Nothing();

        public bool MoveNext()
        {
            do { index++; }
            while (contains[index] == 0 && index <= max);
            return index <= max;
        }

        public void Reset() => throw new NotSupportedException();

        public IEnumerator<int> GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
