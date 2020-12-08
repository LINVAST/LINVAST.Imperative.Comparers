using LINVAST.Nodes;

namespace LINVAST.Imperative.Comparers.Comparers
{
    public interface IASTNodeComparer
    {
        MatchIssues Issues { get; }

        MatchIssues Compare(ASTNode n1, ASTNode n2);
    }
}
