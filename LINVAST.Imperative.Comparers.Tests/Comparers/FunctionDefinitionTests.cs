using LINVAST.Imperative.Comparers;
using LINVAST.Imperative.Comparers.Issues;
using LINVAST.Imperative.Nodes;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Comparers.Comparer
{
    internal sealed class FunctionDefinitionTests : ComparerTestsBase
    {
        [Test]
        public void EmptyFunctionNoParamsTests()
        {
            this.Compare(
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "static", "void"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1)
                    )
                ),
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "static", "void"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1)
                    )
                )
            );

            this.Compare(
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "void"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1)
                    )
                ),
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "void"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1, new EmptyStatNode(1))
                    )
                )
            );

            this.Compare(
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "static", "void"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1)
                    )
                ),
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "void"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1)
                    )
                ),
                new MatchIssues()
                    .AddWarning(
                        new DeclSpecsMismatchWarning(
                            new FuncDeclNode(1, new IdNode(1, "f")),
                            new DeclSpecsNode(1, "static", "void"),
                            new DeclSpecsNode(1, "void")
                        )
                    )
            );

            this.Compare(
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "void"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1)
                    )
                ),
                new SourceNode(
                    new FuncDefNode(1,
                        new DeclSpecsNode(1, "int"),
                        new FuncDeclNode(1, new IdNode(1, "f")),
                        new BlockStatNode(1)
                    )
                ),
                new MatchIssues()
                    .AddWarning(
                        new DeclSpecsMismatchWarning(
                            new FuncDeclNode(1, new IdNode(1, "f")),
                            new DeclSpecsNode(1, "void"),
                            new DeclSpecsNode(1, "int")
                        )
                    )
            );
        }
    }
}
