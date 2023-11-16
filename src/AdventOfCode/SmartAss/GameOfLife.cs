namespace SmartAss;

/// <summary>A abstract implementation of Conway's Game of Life.</summary>
public abstract class GameOfLife<TCell> : HashSet<TCell>
{
    /// <summary>Returns true when a living cell should die.</summary>
    protected virtual bool Dies(TCell cell) => Dies(LivingNeighbors(cell));

    /// <summary>Returns true when a living cell should die.</summary>
    protected virtual bool Dies(int living) => living < 2 || living > 3;

    /// <summary>Returns true when a non-living cell should come into existence.</summary>
    protected virtual bool IntoExistence(TCell cell) => IntoExistence(LivingNeighbors(cell));
    
    /// <summary>Returns true when a non-living cell should come into existence.</summary>
    protected virtual bool IntoExistence(int living) => living == 3;

    /// <summary>Gets the neighbors of a cell.</summary>
    public abstract IEnumerable<TCell> Neighbors(TCell cell);

    /// <summary>Simulates multiple generations.</summary>
    public virtual void Generations(int generations)
    {
        foreach (var _ in Range(0, generations))
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
        toDie.AddRange(this.Where(Dies));
        intoExistance.AddRange(candidates.Where(IntoExistence));

        foreach (var dead in toDie) { Remove(dead); }
        foreach (var alive in intoExistance) { Add(alive); }
    }
    private int LivingNeighbors(TCell cell) => Neighbors(cell).Count(n => Contains(n));

    private readonly HashSet<TCell> candidates = [];
    private readonly HashSet<TCell> toDie = [];
    private readonly HashSet<TCell> intoExistance = [];
}
