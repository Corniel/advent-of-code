namespace Advent_of_Code_2021;

[Category(Category.Simulation, Category.VectorAlgebra)]
public class Day_25
{
    [Example(answer:58, @"
v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>")]
    [Puzzle(answer: 598, O.ms100)]
    public int part_one(string input)
    {
        var grid = input.CharPixels().Grid().SetNeighbors(Neighbors.Sphere);
        var east = new Stack<Point>(grid.Where(p => p.Value == '>').Select(p => p.Key));
        var south = new Stack<Point>(grid.Where(p => p.Value == 'v').Select(p => p.Key));
        var steps = 1;
        while (Move(grid, east, CompassPoint.E) | Move(grid, south, CompassPoint.S)) { steps++; }
        return steps;
    }

    bool Move(Grid<char> grid, Stack<Point> candidates, CompassPoint dir)
    {
        var ch = grid[candidates.Peek()];
        while (candidates.Any() && candidates.Pop() is var tile)
        {
            if (grid.Neighbors[tile][dir] is var target && grid[target] == '.')
            {
                Clear.Push(tile);
                Step.Push(target);
            }
            else { Stay.Push(tile); }
        }
        var moved = Clear.Any();
        while (Clear.Count > 0)
        {
            var move = Step.Pop();
            candidates.Push(move);
            grid[Clear.Pop()] = '.';
            grid[move] = ch;
        }
        while(Stay.Count > 0) { candidates.Push(Stay.Pop()); }
        return moved;
    }
    readonly Stack<Point> Stay = new();
    readonly Stack<Point> Step = new();
    readonly Stack<Point> Clear = new();

    [Puzzle(answer: "You only need 49 stars to boost it", input: "You only need 49 stars to boost it")]
    public string part_two(string input) => input;
}
