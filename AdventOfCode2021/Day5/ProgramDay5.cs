using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day5
{

    public class Point
    {
        public int X { get; private init; }
        public int Y { get; private init; }
        public Point(string s)
        {
            var integers = s.Split(",").Select(int.Parse);
            X = integers.First();
            Y = integers.Last();
        }

    }
    public class VentLines
    {
        private readonly Point from;
        private readonly Point to;

        public VentLines(string[] s)
        {
            this.from = new(s[0]);
            this.to = new(s[1]);
        }

        public IEnumerable<Point> GetoverlappingPoints(IEnumerable<VentLines> line)
        {
            return new List<Point>();
        }
    }

    public class ProgramDay5 : AdventOfCodeProgram
    {
        public ProgramDay5(string? text = null) : base(text)
        {
        }

        protected override string RunPartOne()
        {
            var ventLines = lines.Select(l => new VentLines(l.Split(" -> ")));
            var overlappingPoints = ventLines.Select(l => l.GetoverlappingPoints(ventLines));
            return "";
        }
        protected override string RunPartTwo()
        {
            return "";
        }

        [Theory]
        [InlineData("0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", "5")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay5(input);
            var result = program.RunPartOne();
            result.Should().EndWithEquivalentOf(expectedResult);
        }

        [Theory]
        [InlineData("0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", "1924")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay5(input);
            var result = program.RunPartTwo();
            result.Should().EndWithEquivalentOf(expectedResult);
        }
    }
}
