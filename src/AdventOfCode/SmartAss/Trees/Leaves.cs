namespace SmartAss.Trees;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("{DebuggerDisplay}")]
public class Leaves<TLeaf, TLabel> : IEnumerable<RepeatingLeaf<TLeaf, TLabel>>
    where TLeaf : Leaf<TLeaf, TLabel>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Dictionary<TLabel, RepeatingLeaf<TLeaf, TLabel>> leaves = new();

    public void Add(TLeaf leaf, int repeats)
    {
        if (leaves.ContainsKey(leaf.Label))
        {
            leaves[leaf.Label] += repeats;
        }
        else
        {
            leaves[leaf.Label] = new RepeatingLeaf<TLeaf, TLabel>(leaf, repeats);
        }
    }

    public void Add(TLeaf leaf) => Add(leaf, 1);

    public IEnumerator<RepeatingLeaf<TLeaf, TLabel>> GetEnumerator()
        => leaves.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"Count: {leaves.Sum(l => l.Value.Repeats)}, Unique: {leaves.Count}";
}
