namespace Advent_of_Code_2018;

[Category(Category.Simulation, Category.GameOfLife)]
public class Day_12
{
    [Example(answer: 325, Example._1)]
    [Puzzle(answer: 3605L, O.μs100)]
    public long part_one(Lines lines)
    {
        var game = new GameOfLife(lines);
        game.Generations(20);
        return game.Sum();
    }

    [Puzzle(answer: 4050000000798, O.ms10)]
    public long part_two(Lines lines)
    {
        var game = new GameOfLife(lines);
        game.Generations(1000);
        return game.Sum() + (50_000_000_000 - 1000) * game.Count;
    }

    class GameOfLife : GameOfLife<long>
    {
        public GameOfLife(Lines lines)
        {
            foreach (var mask in lines[1..].Select(line => Bits.UInt32.Parse(line, "#", ".")))
            {
                nexts[mask >> 1] = (mask & 1) == 1;
            }
            var index = 0;
            foreach (var ch in lines[0].Where("#.".Contains))
            {
                if (ch == '#') Add(index);
                index++;
            }
        }

        readonly bool[] nexts = new bool[0b10_00_00];

        protected override bool Dies(long cell) => !IntoExistence(cell);

        protected override bool IntoExistence(long cell)
        {
            var pattern = 0;
            pattern |= Contains(cell + 2) ? 01 : 0;
            pattern |= Contains(cell + 1) ? 02 : 0;
            pattern |= Contains(cell + 0) ? 04 : 0;
            pattern |= Contains(cell - 1) ? 08 : 0;
            pattern |= Contains(cell - 2) ? 16 : 0;
            return nexts[pattern];
        }

        public override IEnumerable<long> Neighbors(long cell) => Range((int)cell - 2, 5).Where(c => c != cell).Select(c => (long)c);
    }
}
