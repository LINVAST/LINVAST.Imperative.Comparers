﻿using System.Collections.Generic;
using System.Linq;
using LINVAST.Imperative.Comparers.Comparers.Common;
using LINVAST.Imperative.Comparers.Issues;
using LINVAST.Nodes;
using Serilog;

namespace LINVAST.Imperative.Comparers.Comparers
{
    internal abstract class ASTNodeComparerBase<T> : IASTNodeComparer where T : ASTNode
    {
        public MatchIssues Issues { get; } = new MatchIssues();

        
        public abstract MatchIssues Compare(T n1, T n2);
        
        public virtual MatchIssues Compare(ASTNode n1, ASTNode n2) => this.Compare(n1.As<T>(), n2.As<T>());


        protected void CompareSymbols(Dictionary<string, DeclaredSymbol> srcSymbols, Dictionary<string, DeclaredSymbol> dstSymbols)
        {
            Log.Debug("Testing declarations...");

            foreach ((string identifier, DeclaredSymbol srcSymbol) in srcSymbols) {
                if (identifier.StartsWith("tmp__"))
                    continue;

                if (!dstSymbols.ContainsKey(identifier)) {
                    this.Issues.AddWarning(new MissingDeclarationWarning(srcSymbol.Specifiers, srcSymbol.Declarator));
                    continue;
                }
                DeclaredSymbol dstSymbol = dstSymbols[identifier];

                if (srcSymbol.Specifiers != dstSymbol.Specifiers)
                    this.Issues.AddWarning(new DeclSpecsMismatchWarning(dstSymbol.Declarator, srcSymbol.Specifiers, dstSymbol.Specifiers));

                var declComparer = new DeclNodeComparer(srcSymbol, dstSymbol);
                this.Issues.Add(declComparer.Compare(srcSymbol.Declarator, dstSymbol.Declarator));
            }

            foreach (string identifier in dstSymbols.Keys.Except(srcSymbols.Keys)) {
                if (!identifier.StartsWith("tmp__")) {
                    DeclaredSymbol extra = dstSymbols[identifier];
                    this.Issues.AddWarning(new ExtraDeclarationWarning(extra.Specifiers, extra.Declarator));
                }
            }

            if (!this.Issues.NoSeriousIssues)
                Log.Error("Failed to match found declarations to all expected declarations.");
            else
                Log.Debug("Matched all expected top-level declarations.");
        }
    }
}
