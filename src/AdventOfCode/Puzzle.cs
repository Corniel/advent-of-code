using System.IO;
using System.Text;

namespace Advent_of_Code
{
    public static class Puzzle
    {
        public static string Input(int year, int day)
        {
            var path = $"Advent_of_Code._{year}.Day_{day:00}.txt";
            using var stream = typeof(PuzzleAttribute).Assembly.GetManifestResourceStream(path);
            if (stream is null) throw new FileNotFoundException(path);
            var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}
