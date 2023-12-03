namespace System;

public static class CharExtensions
{
    public static bool IsDigit(this char c) => c >= '0' && c <= '9';

    public static int Digit(this char c) => c - '0';

    public static int? TryDigit(this char c) => c.IsDigit() ? c.Digit() : null;
}
