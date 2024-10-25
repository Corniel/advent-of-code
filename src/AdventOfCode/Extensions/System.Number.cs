using System.Numerics;
using System.Runtime.CompilerServices;

namespace System;

public static class AocNumberExentsions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InRange<T>(this T value, T lowerBound, T upperBound) where T : struct, IComparisonOperators<T, T, bool>
        => value >= lowerBound && value <= upperBound;
}
