
using AdventOfCodeShared;
using AdventOfCodeShared.Models;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2022.Day9
{
    public class ProgramDay9 : AdventOfCodeProgram
    {
        public ProgramDay9(string? text = null) : base(text)
        { }
        protected override string RunPartOne()
        {
            var rope = new Rope(1);
            foreach ((char direction, int steps) in Lines.Select(l => (l[0], int.Parse(l.Split(" ").Last())))) rope.Move(direction, steps);
            return $"{rope.Knots[1].Distinct().Count()} positions are visited by the tail";
        }

        protected override string RunPartTwo()
        {
            var rope = new Rope(9);
            foreach ((char direction, int steps) in Lines.Select(l => (l[0], int.Parse(l.Split(" ").Last())))) rope.Move(direction, steps);
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
