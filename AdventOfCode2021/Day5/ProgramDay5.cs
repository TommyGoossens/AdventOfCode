using AdventOfCodeShared;
using AdventOfCodeShared.Models;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day5
{
    public class ProgramDay5 : AdventOfCodeProgram<int>
    {
        public ProgramDay5(string? text = null) : base(text)
        {
        }

        public override int RunPartOne()
        {
            var ventLines = Lines.Select(l => new Line(l.Split(" -> ")));
            var overlappingPoints = ventLines.Select(l => l.GetoverlappingPoints(ventLines));
            return 0;
        }
        public override int RunPartTwo()
        {
            return 0;
        }

        [Theory]
        [InlineData("0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", 5)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay5(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", 1924)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay5(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
