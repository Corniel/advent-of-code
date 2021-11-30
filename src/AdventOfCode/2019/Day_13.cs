using Advent_of_Code_2019.IntComputing;
using Int = System.Numerics.BigInteger;

namespace Advent_of_Code_2019;

public class Day_13
{
    [Puzzle(answer: 277, year: 2019, day: 13)]
    public int part_one(string input)
        => new Arcade().Update(Computer.Parse(input).Run().Output).Blocks;

    [Puzzle(answer: 12856, year: 2019, day: 13)]
    public Int part_two(string input)
    {
        var computer = Computer.Parse(input).Update(position: 0, value: 2);
        var game = new Arcade().Update(computer.Run(new RunArguments(
                haltOnInput: true,
                haltOnOutput: false)).Output);

        while (!computer.Finished)
        {
            var move = Math.Sign(game.Ball.X - game.Paddle.X);
            var results = computer.Run(new RunArguments(
                haltOnInput: true,
                haltOnOutput: false, move));
            game.Update(results.Output);
        }
        return game.Score;
    }

    private class Arcade : Grid<Tile>
    {
        public Arcade() : base(42, 23) { }
        public Point Paddle => this.FirstOrDefault(t => t.Value == Tile.Paddle).Key;
        public Point Ball => this.FirstOrDefault(t => t.Value == Tile.Ball).Key;
        public int Blocks => Tiles.Count(t => t == Tile.Block);
        public int Score { get; private set; }
        public Arcade Update(IReadOnlyList<Int> output)
        {
            var index = 0;
            while (index < output.Count)
            {
                var loc = new Point((int)output[index++], (int)output[index++]);
                var val = (int)output[index++];
                if (OnGrid(loc)) this[loc] = (Tile)val;
                else Score = val;
            }
            return this;
        }
    }
    private enum Tile
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4,
    }
}
