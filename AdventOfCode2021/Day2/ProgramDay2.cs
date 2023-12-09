using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day2
{
    public class ProgramDay2 : AdventOfCodeProgram<int>
    {
        public ProgramDay2(string? text = null) : base(text)
        {
        }

        protected override int RunPartOne() => CalculateDepth(false);

        protected override int RunPartTwo() => CalculateDepth(true);


        private int CalculateDepth(bool takeAimIntoAccount)
        {
            int horizontalPos = 0;
            int depth = 0;
            int aim = 0;

            foreach ((string direction, int units) in Lines.Select(l => (l.Split(' ')[0], int.Parse(l.Split(' ')[1]))))
            {
                switch (direction)
                {
                    case "forward":
                        horizontalPos += units;
                        if (takeAimIntoAccount) depth += aim * units;
                        break;
                    case "down":
                        if (!takeAimIntoAccount) depth += units;
                        else aim += units;
                        break;
                    case "up":
                        if (!takeAimIntoAccount) depth -= units;
                        else aim -= units;
                        break;
                }
            }
            return depth * horizontalPos;
        }

        [Theory]
        [InlineData("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", 150)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay2(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", 900)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay2(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
