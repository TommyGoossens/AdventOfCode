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

        protected override string[] Run()
        {
            var splitLines = lines.Select(l => l.Split(new char[] { ',', '-' })).Select(ParseStringsToNumbers);
            var allContainedRows = splitLines.Where(IsContained);
            var allOverlappingRows = splitLines.Where(HasOverlap);
            return new string[] { $"Contained pairs: {allContainedRows.Count()}", $"Pairs with overlap {allOverlappingRows.Count()}" };
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

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay4("2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().EndWithEquivalentOf("2");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay4("2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().EndWithEquivalentOf("4");
        }
    }
}
