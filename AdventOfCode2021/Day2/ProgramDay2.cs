using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day2
{
    public class ProgramDay2 : AdventOfCodeProgram
    {
        public ProgramDay2(string? text = null) : base(text)
        {
        }

        protected override string RunPartOne()
        {
            var depth = CalculateDepth(false);
            return $"Horizontal * depth = {depth}";
        }

        protected override string RunPartTwo()
        {
            var depth = CalculateDepth(true);
            return $"Horizontal * depth (adjusted with aim) = {depth}";
        }

        private int CalculateDepth(bool takeAimIntoAccount)
        {
            int horizontalPos = 0;
            int depth = 0;
            int aim = 0;

            foreach ((string direction, int units) in lines.Select(l => (l.Split(' ')[0], int.Parse(l.Split(' ')[1]))))
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
        [InlineData("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", "150")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay2(input);
            var result = program.RunPartOne();
            result.Should().EndWithEquivalentOf(expectedResult);
        }

        [Theory]
        [InlineData("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", "900")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay2(input);
            var result = program.RunPartTwo();
            result.Should().EndWithEquivalentOf(expectedResult);
        }
    }
}
