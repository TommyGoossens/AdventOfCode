using AdventOfCodeShared;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day4
{
    internal class ProgramDay4 : AdventOfCodeProgram
    {
        public ProgramDay4() : base(4)
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
    }
}
