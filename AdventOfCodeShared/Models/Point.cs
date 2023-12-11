using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCodeShared.Models;

[DebuggerDisplay("X {X} | Y {Y}")]
public class Point : IEquatable<Point>
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Point(string s)
    {
        var integers = s.Split(",").Select(int.Parse);
        X = integers.First();
        Y = integers.Last();
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point UpdateX(int increment)
    {
        X += increment;
        return this;
    }

    public Point UpdateY(int increment)
    {
        Y += increment;
        return this;
    }

    public Point UpdateXAndY(int xIncrement, int yIncrement) => UpdateX(xIncrement).UpdateY(yIncrement);


    public bool Equals(Point? other)
    {
        if (this is null || other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (this is null || obj is null) return false;
        if (obj is not Point otherPoint) return false;
        if (ReferenceEquals(this, otherPoint)) return true;
        return X == otherPoint.X && this.Y == otherPoint.Y;
    }
    public int GetHashCode([DisallowNull] Point obj) => obj.GetHashCode() ^ obj.GetHashCode();

    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

    public void Deconstruct(out int x, out int y)
    {
        x = this.X;
        y = this.Y;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

