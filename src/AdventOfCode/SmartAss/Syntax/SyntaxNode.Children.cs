namespace SmartAss.Syntax;

public partial class SyntaxNode
{
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    class NodeChildren(SyntaxNode parent) : SyntaxNodes<SyntaxNode>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<SyntaxNode> Nodes = [];
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public readonly SyntaxNode Parent = parent;

        public int Count => Nodes.Count;

        public SyntaxNode this[int index]
        {
            get => Nodes[index];
            set => Replace(Nodes[index], value);
        }

        public void Add(SyntaxNode item) => Nodes.Add(ChangeParent(item));
        public void Replace(SyntaxNode oldNode, SyntaxNode newNode)
        {
            var index = Nodes.IndexOf(oldNode);
            NoParent(oldNode);
            Nodes[index] = ChangeParent(newNode);
        }

        public IEnumerator<SyntaxNode> GetEnumerator() => Nodes.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal SyntaxNodes<TSyntax> Cast<TSyntax>() where TSyntax : SyntaxNode => new Typed<TSyntax>(this);

        private SyntaxNode ChangeParent(SyntaxNode node)
        {
            // remove from other children.
            if (node.Parent is { })
            {
                node.Parent.children.Nodes.Remove(node);
            }
            node.Parent = Parent;
            return node;
        }

        static void NoParent(SyntaxNode node) => node.Parent = null;
    }

    private readonly struct Typed<TSyntax>(SyntaxNodes<SyntaxNode> nodes) : SyntaxNodes<TSyntax> where TSyntax : SyntaxNode
    {
        private readonly SyntaxNodes<SyntaxNode> Nodes = nodes;
        public int Count => Nodes.Count;
        public TSyntax this[int index] => (TSyntax)Nodes[index];
        public IEnumerator<TSyntax> GetEnumerator() => Nodes.Cast<TSyntax>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
