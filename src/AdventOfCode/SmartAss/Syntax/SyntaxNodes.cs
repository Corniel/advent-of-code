namespace SmartAss.Syntax
{
    public interface SyntaxNodes<TSyntax> : IReadOnlyList<TSyntax> where TSyntax : SyntaxNode { }
}
