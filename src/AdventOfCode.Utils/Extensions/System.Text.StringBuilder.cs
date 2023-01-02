namespace System.Text;

internal static class AoCStringBuilderExtensions
{
    public static StringBuilder AppendFormatted(this StringBuilder builder, object value, int width, string format = null)
        => format is { }
        ? builder.AppendFormat($"{{0,{width}:{format}}}", value)
        : builder.AppendFormat($"{{0,{width}}}", value);
}
