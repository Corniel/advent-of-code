namespace SmartAss.Trees;

public class Forrest<TLeaf, TLabel> : Dictionary<TLabel, TLeaf>
    where TLeaf : Leaf<TLeaf, TLabel>
{
    public TLeaf TryAdd(TLeaf leaf)
    {
        if (TryGetValue(leaf.Label, out var existing))
        {
            return existing;
        }
        else
        {
            Add(leaf.Label, leaf);
            return leaf;
        }
    }
}

public class Forrest<TLeaf> : Forrest<TLeaf, string>
    where TLeaf : Leaf<TLeaf, string>
{ }
