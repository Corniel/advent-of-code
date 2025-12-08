namespace Advent_of_Code_2017;

[Category(Category.Grid, Category.GameOfLife)]
public class Day_22
{
    [Example(answer: 5587, Example._1)]
    [Puzzle(answer: 5369, O.μs100)]
    public int part_one(CharGrid map) => Run(map, 10000, s => s == State.Inf ? State.Cle : State.Inf);

    [Example(answer: 2511944, Example._1)]
    [Puzzle(answer: 2510774, O.ms100)]
    public int part_two(CharGrid map) => Run(map, 10000000, s => (State)((int)s + 1).Mod(4));

    static int Run(CharGrid map, int runs, Func<State, State> change)
    {
        var spots = map.Hashes().ToDictionary(p => p, _ => State.Inf);
        var carrier = new Cursor(new(map.Cols / 2, map.Rows / 2), Vector.N);
        var burst = 0;

        while (runs-- > 0)
        {
            spots.TryGetValue(carrier.Pos, out var state);
            var next = change(state);
            spots[carrier.Pos] = next;
            carrier = Move(carrier, state).Move();
            if (next == State.Inf) burst++;
        }
        return burst;
    }

    static Cursor Move(Cursor c, State s) => s switch
    {
        State.Cle => c.TurnLeft(),
        State.Inf => c.TurnRight(),
        State.Fla => c.UTurn(),
        _ => c,
    };

    enum State { Cle, Wea, Inf, Fla };
}

