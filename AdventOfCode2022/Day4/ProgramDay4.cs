using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2022.Day4
{
    public class ProgramDay4 : AdventOfCodeProgram
    {
        public ProgramDay4(string? text = null) : base(text)
        {
        }

        protected override string RunPartOne()
        {
            var splitLines = Lines.Select(l => l.Split(new char[] { ',', '-' })).Select(ParseStringsToNumbers);
            var allContainedRows = splitLines.Where(IsContained);
            return $"Contained pairs: {allContainedRows.Count()}";
        }

        protected override string RunPartTwo()
        {
            var splitLines = Lines.Select(l => l.Split(new char[] { ',', '-' })).Select(ParseStringsToNumbers);
            var allOverlappingRows = splitLines.Where(HasOverlap);
            return $"Pairs with overlap {allOverlappingRows.Count()}";
        }

        private bool IsContained((IEnumerable<int> first, IEnumerable<int> second) arg)
        {
            var (first, second) = arg;
            return first.Intersect(second).Count() == first.Count() || second.Intersect(first).Count() == second.Count();
        }

        private (IEnumerable<int> first, IEnumerable<int> second) ParseStringsToNumbers(string[] args)
        {
            var firstRange = Enumerable.Range(int.Parse(args[0]), int.Parse(args[1]) - int.Parse(args[0]) + 1);
            var secondRange = Enumerable.Range(int.Parse(args[2]), int.Parse(args[3]) - int.Parse(args[2]) + 1);
            return (firstRange, secondRange);
        }

        private bool HasOverlap((IEnumerable<int> first, IEnumerable<int> second) arg)
        {
            var (first, second) = arg;
            return first.Intersect(second).Any();
        }

        [Theory]
        [InlineData("2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8", "2")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay4(input);
            var result = program.RunPartOne();
            result.Should().EndWithEquivalentOf(expectedResult);
        }

        [Theory]
        [InlineData("2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8", "4")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay4(input);
            var result = program.RunPartTwo();
            result.Should().EndWithEquivalentOf(expectedResult);
        }
    }
}
