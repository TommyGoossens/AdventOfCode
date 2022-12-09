namespace AdventOfCodeShared.Models
{
    public class Rope
    {
        private readonly Point head;
        private readonly Dictionary<int, List<Point>> knots;

        public Rope(int nrOfKnots)
        {
            head = new Point(0, 0);
            knots = Enumerable.Range(0, nrOfKnots).ToDictionary(i => i + 1, i => new List<Point>() { new Point(0, 0) });
        }

        public Dictionary<int, List<Point>> Knots => knots;

        public Rope Move(char direction, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                if (direction == 'U') head.UpdateY(1);
                else if (direction == 'D') head.UpdateY(-1);
                else if (direction == 'R') head.UpdateX(1);
                else if (direction == 'L') head.UpdateX(-1);

                foreach (var t in knots)
                {
                    var tailX = t.Value[^1].X;
                    var tailY = t.Value[^1].Y;
                    knots.TryGetValue(t.Key - 1, out var prevKnot);
                    var pointToFollow = prevKnot?[^1] ?? head;

                    if (pointToFollow.X == tailX && pointToFollow.Y > tailY + 1) tailY++;
                    else if (pointToFollow.X == tailX && pointToFollow.Y < tailY - 1) tailY--;
                    else if (pointToFollow.X > tailX + 1 && pointToFollow.Y == tailY) tailX++;
                    else if (pointToFollow.X < tailX - 1 && pointToFollow.Y == tailY) tailX--;
                    else if (Math.Abs(pointToFollow.X - tailX) == 1 && Math.Abs(pointToFollow.Y - tailY) == 1) continue;
                    else if (pointToFollow.X < tailX && pointToFollow.Y < tailY)
                    {
                        tailX--;
                        tailY--;
                    }
                    else if (pointToFollow.X > tailX && pointToFollow.Y < tailY)
                    {
                        tailX++;
                        tailY--;
                    }
                    else if (pointToFollow.X > tailX && pointToFollow.Y > tailY)
                    {
                        tailX++;
                        tailY++;
                    }
                    else if (pointToFollow.X < tailX && pointToFollow.Y > tailY)
                    {
                        tailX--;
                        tailY++;
                    }
                    else continue;
                    knots[t.Key].Add(new Point(tailX, tailY));
                }
            }
            return this;
        }
    }
}
