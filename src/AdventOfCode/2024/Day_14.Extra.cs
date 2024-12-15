using System.Drawing;

namespace Advent_of_Code_2024;

partial class Day_14
{
    [Explicit("Renders the first 10,000 images, to manually spot the tree, we do not automatically want to run this.")]
    [Puzzle(answer: 0, null, 101, 103, O.s)]
    public int Render(Ints numbers, int wide, int tall)
    {
        const int Space = 10;

        var bots = Bots(numbers).ToArray();
        var map = new Grid<bool>(wide, tall);

        for (var hundred = 0; hundred < 100; hundred++)
        {
            using var bmp = new Bitmap(wide * 10 + 9 * Space, tall * 10 + 9 * Space);
            var gfx = Graphics.FromImage(bmp);
            using var bg = new SolidBrush(Color.FromArgb(127, 127, 127));
            using var black = new SolidBrush(Color.Black);
            gfx.FillRectangle(bg, 0, 0, bmp.Width, bmp.Height);

            for (var i = 0; i < 100; i++)
            {
                map.Clear();

                var step = hundred * 100 + i;
                var row = (tall + Space) * (i / 10);
                var col = (wide + Space) * (i % 10);

                gfx.FillRectangle(black, col, row, wide, tall);


                foreach (var p in Points(bots, step, wide, tall)) map[p] = true;

                var tree = Tree(map, wide, tall);

                foreach (var p in map.Positions(p => p))
                {
                    var color = map[p] switch
                    {
                        _ when tree is { } t && p.X.InRange(t.X, t.X + 31) && p.Y.InRange(t.Y, t.Y + 33) => Color.Green,
                        _ => Color.FromArgb(red: Random.Shared.Next(190, 255), green: 0, blue: 0),
                    };
                    bmp.SetPixel(p.X + col, p.Y + row, color);
                }
            }
            bmp.Save($"C:/TEMP/AOC/tree.{hundred:00}.png");
        }
        return 0;
    }

    private static SmartAss.Numerics.Point? Tree(Grid<bool> map, int wide, int tall)
    {
        for (var r = 0; r < tall - 33; r++)
        {
            var length = 0; var c = -1;
            while (++c + length < wide)
            {
                var on = map[c, r];
                if (on && ++length > 20) return new(c -20, r);
                else if (!on) length = 0;
            }
        }
        return null;
    }
}
