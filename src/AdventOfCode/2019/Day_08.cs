namespace Advent_of_Code_2019;

[Category(Category.Grid, Category.Cryptography, Category.ASCII)]
public class Day_08
{
    [Puzzle(answer: 2480, O.μs10)]
    public int part_one(string str)
        => Layer.Parse(25, 6, str)
            .OrderBy(layer => layer.Zeros)
            .Select(layer => layer.Ones * layer.Twos)
            .FirstOrDefault();

    [Puzzle(answer: "ZYBLH", O.μs100)]
    public string part_two(string str)
    {
        var layers = Layer.Parse(25, 6, str).ToArray();
        var merged = layers.Last();
        foreach (var layer in layers.Reverse().Skip(1))
        {
            merged = layer.Merge(merged);
        }
        return merged.AsciiText(25);
    }

    public readonly struct Layer(string pixels)
    {
        private const char Transprant = '2';

        private readonly string pixels = pixels;

        public int Size => pixels.Length;
        public int Zeros => pixels.Count(ch => ch == '0');
        public int Ones => pixels.Count(ch => ch == '1');
        public int Twos => pixels.Count(ch => ch == Transprant);

        public string AsciiText(int width)
        {
            var sb = new StringBuilder(Size).AppendLine();
            var pos = 0;
            while (pos < Size)
            {
                sb.AppendLine(pixels.Substring(pos, width));
                pos += width;
            }
            return sb.ToString().CharPixels().Grid(ch => ch == '1').AsciiText();
        }

        public Layer Merge(Layer lower)
        {
            var merged = pixels.ToCharArray();
            for (var i = 0; i < Size; i++)
            {
                if (pixels[i] == Transprant)
                {
                    merged[i] = lower.pixels[i];
                }
            }
            return new Layer(new string(merged));
        }

        public static IEnumerable<Layer> Parse(int width, int height, string str)
        {
            var pos = 0;
            var size = width * height;
            while (pos <= str.Length - size)
            {
                yield return new Layer(str.Substring(pos, size));
                pos += size;
            }
        }
    }
}
