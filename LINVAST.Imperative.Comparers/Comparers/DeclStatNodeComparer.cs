﻿using System.Collections.Generic;
using LINVAST.Exceptions;
using LINVAST.Imperative.Comparers.Comparers.Common;
using LINVAST.Imperative.Nodes;
using Serilog;

namespace LINVAST.Imperative.Comparers.Comparers
{
    internal sealed class DeclStatNodeComparer : ASTNodeComparerBase<DeclStatNode>
    {
        public override MatchIssues Compare(DeclStatNode n1, DeclStatNode n2)
        {
            Log.Debug("Comparing declarations: `{SrcDecl}` with: `{DstDecl}`", n1, n2);
            Dictionary<string, DeclaredSymbol> srcSymbols = this.GetDeclaredSymbols(n1);
            Dictionary<string, DeclaredSymbol> dstSymbols = this.GetDeclaredSymbols(n2);
            this.CompareSymbols(srcSymbols, dstSymbols);
            return this.Issues;
        }


        private Dictionary<string, DeclaredSymbol> GetDeclaredSymbols(DeclStatNode node)
        {
            var symbols = new Dictionary<string, DeclaredSymbol>();

            foreach (DeclNode decl in node.DeclaratorList.Declarators) {
                var symbol = DeclaredSymbol.From(node.Specifiers, decl);
                if (symbol is DeclaredFunctionSymbol df && symbols.ContainsKey(df.Identifier)) {
                    if (!df.AddOverload(df.FunctionDeclarator))
                        throw new SemanticErrorException($"Multiple overloads with same parameters found for function: {df.Identifier}", decl.Line);
                }
                symbols.Add(decl.Identifier, symbol);
            }

            return symbols;
        }
    }
}
