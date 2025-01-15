namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_13
{
    [Puzzle(answer: 277, O.μs100)]
    public int part_one(string str) => new Arcade().Update(Computer.Parse(str).Run().Output).Blocks;

    [Puzzle(answer: 12856, O.ms100)]
    public Int part_two(string str)
    {
        var computer = Computer.Parse(str).Update(position: 0, value: 2);
        var game = new Arcade().Update(computer.Run(new RunArguments(
                haltOnInput: true,
                haltOnOutput: false)).Output);

        while (!computer.Finished)
        {
            var move = (game.Ball.X - game.Paddle.X).Sign();
            var results = computer.Run(new RunArguments(
                haltOnInput: true,
                haltOnOutput: false, move));
            game.Update(results.Output);
        }
        return game.Score;
    }

    class Arcade : Grid<Tile>
    {
        public Arcade() : base(42, 23) { }
        public Point Paddle => this.FirstOrDefault(t => t.Value == Tile.Paddle).Key;
        public Point Ball => this.FirstOrDefault(t => t.Value == Tile.Ball).Key;
        public int Blocks => Tiles.Count(Tile.Block);
        public int Score { get; private set; }
        public Arcade Update(IReadOnlyList<Int> output)
        {
            var index = 0;
            while (index < output.Count)
            {
                Point loc = ((int)output[index++], (int)output[index++]);
                var val = (int)output[index++];
                if (OnGrid(loc)) this[loc] = (Tile)val;
                else Score = val;
            }
            return this;
        }
    }
    enum Tile { Empty = 0, Wall = 1, Block = 2, Paddle = 3, Ball = 4 }
}
