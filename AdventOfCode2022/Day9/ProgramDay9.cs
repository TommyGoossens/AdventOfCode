using AdventOfCodeShared;
using AdventOfCodeShared.Models;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2022.Day9
{
    internal class Rope
    {
        private readonly Point head;
        private readonly Dictionary<int, List<Point>> knots;
        internal Rope(int nrOfKnots)
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



    public class ProgramDay9 : AdventOfCodeProgram
    {
        public ProgramDay9(string? text = null) : base(text)
        {
        }
        protected override string RunPartOne()
        {
            var rope = new Rope(1);
            foreach ((char direction, int steps) in lines.Select(l => (l[0], int.Parse(l.Split(" ").Last())))) rope.Move(direction, steps);        
            return $"{rope.Knots[1].Distinct().Count()} positions are visited by the tail";
        }

        protected override string RunPartTwo()
        {
            var rope = new Rope(9);
            foreach ((char direction, int steps) in lines.Select(l => (l[0], int.Parse(l.Split(" ").Last())))) rope.Move(direction, steps);
            return $"{rope.Knots[9].Distinct().Count()} positions are visited by the tail";
        }

        [Theory]
        [InlineData("R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2", "13")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay9(input);
            var result = program.RunPartOne();
            result.Should().StartWithEquivalentOf(expectedResult);
        }

        [Theory]
        [InlineData("R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20", "36")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay9(input);
            var result = program.RunPartTwo();
            result.Should().StartWithEquivalentOf(expectedResult);
        }


    }
}
