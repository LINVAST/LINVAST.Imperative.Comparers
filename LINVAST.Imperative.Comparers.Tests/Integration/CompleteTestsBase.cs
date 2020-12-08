using LINVAST.Imperative.Builders.C;
using LINVAST.Imperative.Builders.Lua;
using LINVAST.Imperative.Builders.Pseudo;
using LINVAST.Imperative.Comparers;
using LINVAST.Nodes;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Comparers.Integration
{
    internal abstract class CompleteTestsBase
    {
        [Test]
        public abstract void SemanticEquivallenceTests();

        [Test]
        public abstract void DifferenceTests();


        public virtual ASTNode FromCSource(string src) 
            => new CASTBuilder().BuildFromSource(src);
        
        public virtual ASTNode FromLuaSource(string src) 
            => new LuaASTBuilder().BuildFromSource(src);
        
        public virtual ASTNode FromPseudoSource(string src) 
            => new PseudoASTBuilder().BuildFromSource(src);


        protected void Compare(ASTNode src, ASTNode dst, MatchIssues? expectedIssues = null)
        {
            MatchIssues issues = new ASTNodeComparer(src, dst).AttemptMatch();
            expectedIssues ??= new MatchIssues();
            CollectionAssert.AreEqual(expectedIssues, issues);
        }
    }
}
