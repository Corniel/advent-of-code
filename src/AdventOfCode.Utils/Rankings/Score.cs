namespace Advent_of_Code.Rankings;

public sealed record Score<TValue>(AdventDate Date, int Rank, TValue Value)
    where TValue : struct, System.Numerics.IAdditionOperators<TValue, TValue, TValue>, IFormattable;
