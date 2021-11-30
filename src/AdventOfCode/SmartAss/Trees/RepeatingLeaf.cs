namespace SmartAss.Trees;

public readonly struct RepeatingLeaf<TLeaf, TLabel> where TLeaf : Leaf<TLeaf, TLabel>
{
    public RepeatingLeaf(TLeaf leaf, int repeats = 1)
    {
        Leaf = leaf;
        Repeats = repeats;
    }
    public TLeaf Leaf { get; }

    public int Repeats { get; }

    public RepeatingLeaf<TLeaf, TLabel> Add(int repeats)
        => new(Leaf, Repeats + repeats);

    public override string ToString() => $"{Repeats}: {Leaf}";

    public static RepeatingLeaf<TLeaf, TLabel> operator +(RepeatingLeaf<TLeaf, TLabel> leaf, int repeats)
        => leaf.Add(repeats);
}
