using System;
using System.Linq;

namespace SmartAss.Trees
{
    public class Leaf<TLeaf, TLabel> : IEquatable<Leaf<TLeaf, TLabel>>
        where TLeaf : Leaf<TLeaf, TLabel>
    {
        public Leaf(TLabel label) => Label = label;

        public TLabel Label { get; }

        public Leaves<TLeaf, TLabel> Leaves { get; } = new();

        public int Count => Leaves.Sum(child => child.Repeats);
        
        public int NestedCount
            => Count 
            + Leaves.Sum(child => child.Repeats * child.Leaf.NestedCount);

        public TLeaf Search(TLabel label)
        {
            if (Label.Equals(label))
            {
                return (TLeaf)this;
            }
            else
            {
                foreach(var repeating in Leaves)
                {
                    var found = repeating.Leaf.Search(label);
                    if(found != null)
                    {
                        return found;
                    }
                }
                return null;
            }
        }

        public override bool Equals(object obj)
            => obj is Leaf<TLeaf, TLabel> other && Equals(other);

        public bool Equals(Leaf<TLeaf, TLabel> other)
            => other != null && Label.Equals(other.Label);

        public override int GetHashCode() => Label.GetHashCode();

        public override string ToString() => $"{Label}, Children: {Count}, Anchest";
    }

    public class Leaf<TLeaf>: Leaf<TLeaf, string>
         where TLeaf : Leaf<TLeaf, string>
    {
        public Leaf(string label) : base(label) => Do.Nothing();
    }
    public class Leaf : Leaf<Leaf, string>
    {
        public Leaf(string label) : base(label) => Do.Nothing();
    }
}
