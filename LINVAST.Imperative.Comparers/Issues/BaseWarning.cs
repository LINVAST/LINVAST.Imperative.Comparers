﻿namespace LINVAST.Imperative.Comparers.Issues
{
    public abstract class BaseWarning : BaseIssue
    {
        public override string ToString() => $"WRN {base.ToString()}";
    }
}
