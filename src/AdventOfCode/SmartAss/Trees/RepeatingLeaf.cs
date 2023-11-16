namespace SmartAss.Trees;

public readonly struct RepeatingLeaf<TLeaf, TLabel>(TLeaf leaf, int repeats = 1) where TLeaf : Leaf<TLeaf, TLabel>
{
    public TLeaf Leaf { get; } = leaf;

    public int Repeats { get; } = repeats;

    public RepeatingLeaf<TLeaf, TLabel> Add(int repeats)
        => new(Leaf, Repeats + repeats);

    public override string ToString() => $"{Repeats}: {Leaf}";

    public static RepeatingLeaf<TLeaf, TLabel> operator +(RepeatingLeaf<TLeaf, TLabel> leaf, int repeats)
        => leaf.Add(repeats);
}
