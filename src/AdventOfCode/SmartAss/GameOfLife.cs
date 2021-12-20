namespace SmartAss;

/// <summary>A abstract implementation of Conway's Game of Life.</summary>
public abstract class GameOfLife<TCell> : HashSet<TCell>
{
    /// <summary>Returns true when a living cell should die.</summary>
    public abstract bool Dies(int living);

    /// <summary>Returns true when a non-living cell should come into existence.</summary>
    public abstract bool IntoExistence(int living);

    /// <summary>Gets the neighbors of a cell.</summary>
    public abstract IEnumerable<TCell> Neighbors(TCell cell);

    /// <summary>Simulates multiple generations.</summary>
    public void Generations(int generations)
    {
        foreach (var _ in Enumerable.Range(0, generations))
        {
            NextGeneration();
        }
    }

    /// <summary>Simulates one generation.</summary>
    public void NextGeneration()
    {
        candidates.Clear();
        toDie.Clear();
        intoExistance.Clear();

        candidates.AddRange(this.SelectMany(alive => Neighbors(alive)));
        toDie.AddRange(this.Where(cell => Dies(LivingNeighbors(cell))));
        intoExistance.AddRange(candidates.Where(cell => IntoExistence(LivingNeighbors(cell))));

        foreach (var dead in toDie) { Remove(dead); }
        foreach (var alive in intoExistance) { Add(alive); }
    }
    private int LivingNeighbors(TCell cell) => Neighbors(cell).Count(n => Contains(n));

    private readonly HashSet<TCell> candidates = new();
    private readonly HashSet<TCell> toDie = new();
    private readonly HashSet<TCell> intoExistance = new();
}
