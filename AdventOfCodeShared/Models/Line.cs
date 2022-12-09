namespace AdventOfCodeShared.Models
{
    public class Line
    {
        private readonly Point from;
        private readonly Point to;

        public Line(string[] s)
        {
            this.from = new(s[0]);
            this.to = new(s[1]);
        }

        public IEnumerable<Point> GetoverlappingPoints(IEnumerable<Line> lines)
        {
            return new List<Point>();
        }
    }
}
