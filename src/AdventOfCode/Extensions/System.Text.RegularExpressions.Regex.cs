using System.Diagnostics.CodeAnalysis;

namespace System;

public static class AoRegexExtensions
{
    /// <inheritdoc cref="Regex.Replace(string, string)" />
    public static string RegReplace(this string str, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, string replacement)
        => Regex.Replace(str, pattern, replacement);

    /// <inheritdoc cref="Regex.IsMatch(string, string)" />
    public static bool IsMatch(this string str, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern) 
        => Regex.IsMatch(str, pattern);
}
