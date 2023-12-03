namespace AdventOfCodeShared.Models
{
    public class Line
    {
        public Point From { get; private init; }
        public Point To { get; private init; }

        public Line(string[] s)
        {
            this.From = new(s[0]);
            this.To = new(s[1]);
        }

        public Line(Point from, Point to)
        {
            From = from;
            To = to;
        }

        public IEnumerable<Point> GetoverlappingPoints(IEnumerable<Line> lines)
        {
            return new List<Point>();
        }
    }
}
