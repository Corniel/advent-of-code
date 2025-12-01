namespace System;

public static class CharSpans
{
    extension(ReadOnlySpan<char> chars)
    {
        [Pure]
        public bool Is(ReadOnlySpan<char> other)
            => chars.Equals(other, StringComparison.Ordinal);
    }
}
