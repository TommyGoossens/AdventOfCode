using AdventOfCodeShared;

namespace AdventOfCode2022.Day4
{
    internal class ProgramDay4 : AdventOfCodeProgram
    {
        public ProgramDay4() : base(4)
        {
        }

        protected override string[] Run()
        {
            var allContainedRows = lines.Where(IsContained);
            var allOverlappingRows = lines.Where(HasOverlap);
            return new string[] {$"Contained pairs: {allContainedRows.Count()}", $"Pairs with overlap {allOverlappingRows.Count()}" };
        }

        private bool IsContained(string l)
        {
            var first = l.Split(',')[0];
            var second = l.Split(',')[1];
            var firstStart = int.Parse(first.Split('-')[0]);
            var firstEnd = int.Parse(first.Split('-')[1]);
            var secondStart = int.Parse(second.Split('-')[0]);
            var secondEnd = int.Parse(second.Split('-')[1]);

            var firstRange = Enumerable.Range(firstStart, firstEnd - firstStart + 1);
            var secondRange = Enumerable.Range(secondStart, secondEnd - secondStart + 1);
            return firstRange.Intersect(secondRange).Count() == firstRange.Count() || secondRange.Intersect(firstRange).Count() == secondRange.Count();
        }


        private bool HasOverlap(string l)
        {
            var first = l.Split(',')[0];
            var second = l.Split(',')[1];
            var firstStart = int.Parse(first.Split('-')[0]);
            var firstEnd = int.Parse(first.Split('-')[1]);
            var secondStart = int.Parse(second.Split('-')[0]);
            var secondEnd = int.Parse(second.Split('-')[1]);

            var firstRange = Enumerable.Range(firstStart, firstEnd - firstStart + 1);
            var secondRange = Enumerable.Range(secondStart, secondEnd - secondStart + 1);
            return firstRange.Intersect(secondRange).Any();
        }
    }
}
