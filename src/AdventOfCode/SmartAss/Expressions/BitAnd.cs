﻿namespace SmartAss.Expressions;

internal class BitAnd : Binary
{
    public BitAnd(Expr left, Expr right) : base(left, right) { }

    protected override string Operator => "&";

    protected override long Solve(long value, long? left, long? right) => throw new NotImplementedException();

    protected override long TryValue(long left, long right) => left & right;
}