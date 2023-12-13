using System.Diagnostics.CodeAnalysis;

namespace System;

public static class AoCStringExtensions
{
    /// <inheritdoc cref="Regex.Replace(string, string)" />
    public static string RegReplace(this string str, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, string replacement)
        => Regex.Replace(str, pattern, replacement);
}
