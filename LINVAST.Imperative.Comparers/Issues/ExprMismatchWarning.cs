﻿using System;
using System.Diagnostics.CodeAnalysis;
using LINVAST.Imperative.Nodes;
using Serilog;

namespace LINVAST.Imperative.Comparers.Issues
{
    public sealed class ExprNodeMismatchWarning : BaseWarning
    {
        public ExprNode Expected { get; set; }
        public ExprNode Actual { get; set; }
        public string Message { get; set; }
        public int Line { get; set; }


        public ExprNodeMismatchWarning(int line, ExprNode expected, ExprNode actual, string message = "Expressions differ")
        {
            if (expected.Equals(actual))
                throw new ArgumentException("Expected different objects");
            this.Expected = expected;
            this.Actual = actual;
            this.Message = message;
            this.Line = line;
        }


        public override string ToString() 
            => $"{base.ToString()}| {this.Message} at line {this.Line} | exp: {this.Expected} | got: {this.Actual}";

        public override void LogIssue()
        {
            Log.Error("Expression mismatch found at line {Line}: expected {ExpectedValue}, got {ActualValue}",
                this.Line, this.Expected, this.Actual);
        }

        public override bool Equals(object? obj)
            => this.Equals(obj as ExprNodeMismatchWarning);

        public override bool Equals([AllowNull] BaseIssue other)
        {
            if (!base.Equals(other))
                return false;

            var o = other as ExprNodeMismatchWarning;
            return Equals(this.Expected, o?.Expected) && Equals(this.Actual, o?.Actual);
        }
    }
}
