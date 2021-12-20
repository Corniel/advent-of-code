namespace SmartAss.Syntax
{
    public partial class SyntaxNode
    {
        protected SyntaxNode() => children = new NodeChildren(this);

        public int Depth => (Parent?.Depth + 1) ?? 1;
        public SyntaxNode Root => AncestorsAndSelf().LastOrDefault();
        public SyntaxNode Parent { get; private set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly NodeChildren children;

        public void AddChild(SyntaxNode child) => children.Add(child);
        public void AddChildren(params SyntaxNode[] children)
        {
            foreach (var child in children)
            {
                this.children.Add(child);
            }
        }
        public void ReplaceChild(SyntaxNode oldNode, SyntaxNode newNode) => children.Replace(oldNode, newNode);
        public void ReplaceChild(int index, SyntaxNode newNode) => children[index] = newNode;

        public SyntaxNodes<SyntaxNode> Children() => children;
        public SyntaxNodes<TSyntax> Children<TSyntax>() where TSyntax : SyntaxNode => children.Cast<TSyntax>();

        public IEnumerable<SyntaxNode> AncestorsAndSelf()
        {
            var parent = this;
            while (parent is { })
            {
                yield return parent;
                parent = parent.Parent;
            }
        }
        public IEnumerable<SyntaxNode> Ancestors() => AncestorsAndSelf().Skip(1);
    }
}
