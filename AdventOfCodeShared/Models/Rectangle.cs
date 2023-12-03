using System.Diagnostics.CodeAnalysis;

namespace AdventOfCodeShared.Models
{
    public class Rectangle : IEquatable<Rectangle>
    {
        public Point Origin { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle(Point origin, int width, int height)
        {
            Origin = origin;
            Width = width;
            Height = height;
        }

        public Rectangle(Point origin, Point bottomRight)
        {
            Origin = origin;
            Width = bottomRight.X - origin.X;
            Height = bottomRight.Y - origin.Y;
        }

        public Rectangle(int x, int y, int width, int height) : this(new(x, y), width, height)
        { }

        public bool CoordinatesAreWithinRectangle(Line line, bool shouldEntireLineBeWithinFrame = false)
        {
            return shouldEntireLineBeWithinFrame ?
            PointIsWithinBorder(line.From) && PointIsWithinBorder(line.To) :
            PointIsWithinBorder(line.From) || PointIsWithinBorder(line.To);
        }
        public bool PointIsWithinBorder(Point point)
        {
            return
                point.X >= Origin.X &&
                point.X <= Origin.X + Width &&
                point.Y >= Origin.Y &&
                point.Y <= Origin.Y + Height;
        }

        public bool Equals(Rectangle? other)
        {
            if (this is null || other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Origin == other.Origin && this.Width == other.Width && this.Height == other.Height;
        }

        public override bool Equals(object? obj)
        {
            if (this is null || obj is null) return false;
            if (obj is not Rectangle otherRectangle) return false;
            if (ReferenceEquals(this, otherRectangle)) return true;
            return Origin == otherRectangle.Origin && this.Width == otherRectangle.Width && this.Height == otherRectangle.Height;
        }
        public int GetHashCode([DisallowNull] Rectangle obj) => obj.GetHashCode() ^ obj.GetHashCode();

        public override int GetHashCode() => Origin.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
    }
}
