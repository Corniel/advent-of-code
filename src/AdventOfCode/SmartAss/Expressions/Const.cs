﻿namespace SmartAss.Expressions;

public sealed class Const : Expr
{
    public Const(long value) => Val = value;

    public long Val { get; }

    public override void Solve(long value, Params pars) => throw new NotSolved();

    public override long? TryValue(Params pars) => Val;

    public override string ToString() => Val.ToString();
}