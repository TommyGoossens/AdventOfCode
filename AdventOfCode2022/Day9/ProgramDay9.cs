using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2022.Day9
{
    public class ProgramDay9 : AdventOfCodeProgram
    {
        public ProgramDay9(string? text = null) : base(text)
        {
        }
        protected override string RunPartOne()
        {
            var tailPositions = new List<(int x, int y)> { (0, 0) };
            var latestHeadPos = (0, 0);
            foreach ((char direction, int steps) in lines.Select(l => (l[0], int.Parse(l.Split(" ").Last()))))
            {
                var (tailMoves, headpos) = MovePartOne(latestHeadPos, tailPositions[tailPositions.Count - 1], direction, steps);
                latestHeadPos = headpos;
                tailPositions.AddRange(tailMoves);
            }
            return $"{tailPositions.Distinct().Count()} positions are visited by the tail";
        }

        private (List<(int x, int y)> tailMoves, (int x, int y) headPos) MovePartOne((int x, int y) headPos, (int x, int y) tailPos, char direction, int steps)
        {
            var moves = new List<(int x, int y)>();
            var (tailX, tailY) = tailPos;
            var (headX, headY) = headPos;

            for (int i = 0; i < steps; i++)
            {
                if (direction == 'U') headY++;
                else if (direction == 'D') headY--;
                else if (direction == 'R') headX++;
                else if (direction == 'L') headX--;
                var shouldMoveTail = Math.Max(Math.Abs(headY - tailY), Math.Abs(headX - tailX)) > 1;
                if (shouldMoveTail)
                {
                    if ((direction == 'U' || direction == 'D') && tailX != headX) tailX = headX;
                    else if ((direction == 'R' || direction == 'L') && tailY != headY) tailY = headY;

                    if (direction == 'U') tailY++;
                    else if (direction == 'D') tailY--;
                    else if (direction == 'R') tailX++;
                    else if (direction == 'L') tailX--;
                    moves.Add((tailX, tailY));
                }
            }

            return (moves, (headX, headY));
        }

        protected override string RunPartTwo()
        {
            var tailPositions = Enumerable.Range(0, 9).ToDictionary(i => i + 1, i => new List<(int x, int y)>() { (0, 0) });


            var latestHeadPos = (0, 0);
            foreach ((char direction, int steps) in lines.Select(l => (l[0], int.Parse(l.Split(" ").Last()))))
            {
                var (tailMoves, headpos) = MovePartTwo(latestHeadPos, tailPositions, direction, steps);
                latestHeadPos = headpos;
                tailPositions = tailMoves;
            }
            return $"{tailPositions[9].Distinct().Count()} positions are visited by the tail";
        }


        //private (Dictionary<int, List<(int x, int y)>>, (int x, int y) headPos) MovePartTwo((int x, int y) headPos, Dictionary<int, List<(int x, int y)>> tailPos, char direction, int steps)
        //{
        //    var (headX, headY) = headPos;

        //    for (int i = 0; i < steps; i++)
        //    {
        //        if (direction == 'U') headY++;
        //        else if (direction == 'D') headY--;
        //        else if (direction == 'R') headX++;
        //        else if (direction == 'L') headX--;

        //        foreach (var t in tailPos)
        //        {
        //            var (tailX, tailY) = t.Value[^1];
        //            tailPos.TryGetValue(t.Key - 1, out var prevKnot);

        //            var pointToFollow = prevKnot?[^1] ?? (headX, headY);
        //            (int pX, int pY) = pointToFollow;

        //            var shouldMoveTail = Math.Max(Math.Abs(pY - tailY), Math.Abs(pX - tailX)) > 1;
        //            if (shouldMoveTail)
        //            {
        //                if ((direction == 'U' || direction == 'D') && tailX != pX) tailX = pX;
        //                else if ((direction == 'R' || direction == 'L') && tailY != pY) tailY = pY;
        //                shouldMoveTail = Math.Max(Math.Abs(pY - tailY), Math.Abs(pX - tailX)) > 1;
        //                if (shouldMoveTail)
        //                {
        //                    if (direction == 'U') tailY++;
        //                    else if (direction == 'D') tailY--;
        //                    else if (direction == 'R') tailX++;
        //                    else if (direction == 'L') tailX--;
        //                }

        //                tailPos[t.Key].Add((tailX, tailY));
        //            }
        //        }
        //    }
        //    return (tailPos, (headX, headY));
        //}


        private (Dictionary<int, List<(int x, int y)>>, (int x, int y) headPos) MovePartTwo((int x, int y) headPos, Dictionary<int, List<(int x, int y)>> tailPos, char direction, int steps)
        {
            var (headX, headY) = headPos;

            for (int i = 0; i < steps; i++)
            {
                if (direction == 'U') headY++;
                else if (direction == 'D') headY--;
                else if (direction == 'R') headX++;
                else if (direction == 'L') headX--;

                foreach (var t in tailPos)
                {
                    var (tailX, tailY) = t.Value[^1];
                    tailPos.TryGetValue(t.Key - 1, out var prevKnot);
                    var pointToFollow = prevKnot?[^1] ?? (headX, headY);
                    (int pX, int pY) = pointToFollow;

                    if (pX == tailX && pY > tailY + 1) tailY++;
                    else if (pX == tailX && pY < tailY - 1) tailY--;
                    else if (pX > tailX + 1 && pY == tailY) tailX++;
                    else if (pX < tailX - 1 && pY == tailY) tailX--;
                    else if (Math.Abs(pX - tailX) == 1 && Math.Abs(pY - tailY) == 1) continue;
                    else if (pX < tailX && pY < tailY)
                    {
                        tailX--;
                        tailY--;
                    }
                    else if (pX > tailX && pY < tailY)
                    {
                        tailX++;
                        tailY--;
                    }
                    else if (pX > tailX && pY > tailY)
                    {
                        tailX++;
                        tailY++;
                    }
                    else if (pX < tailX && pY > tailY)
                    {
                        tailX--;
                        tailY++;
                    }
                    else continue;

                    tailPos[t.Key].Add((tailX, tailY));
                }
            }
            return (tailPos, (headX, headY));
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
