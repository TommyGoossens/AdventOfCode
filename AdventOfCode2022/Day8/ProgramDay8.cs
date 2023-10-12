using AdventOfCodeShared;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AdventOfCode2022.Day8
{
    public class ProgramDay8 : AdventOfCodeProgram
    {
        private readonly List<List<double>> linesAsInt;
        public ProgramDay8(string? text = null) : base(text)
        {
            linesAsInt = Lines.Select(l => l.Select(c => char.GetNumericValue(c)).ToList()).ToList();
        }

        protected override string RunPartOne()
        {
            int part1 = HandlePartOne();
            return $"{part1} trees are visible";
        }
        protected override string RunPartTwo()
        {
            int part2 = HandlePartTwo();
            return $"Highest scenic score is: {part2}";
        }

        private int GetVisibleTreesInList(IEnumerable<double> trees, double currentTree)
        {
            var nrOfVisibleTrees = 0;
            foreach (var tree in trees)
            {
                if (tree < currentTree) nrOfVisibleTrees++;
                if (tree >= currentTree) { nrOfVisibleTrees++; break; }
            }
            return nrOfVisibleTrees;
        }
        private int HandlePartTwo()
        {
            var scenicScores = new List<int>();
            for (int r = 0; r < linesAsInt.Count; r++)
            {
                for (var c = 0; c < linesAsInt[r].Count; c++)
                {
                    var currentTree = linesAsInt[r][c];
                    var treesInColAbove = linesAsInt.Take(r).Select(l => l[c]).Reverse();
                    var treesInColBelow = linesAsInt.Skip(r + 1).Select(l => l[c]);
                    var treesToTheRight = linesAsInt[r].Skip(c + 1);
                    var treesToTheLeft = linesAsInt[r].Take(c).Reverse();

                    var nrOfVisibleTreesAbove = GetVisibleTreesInList(treesInColAbove, currentTree);
                    var nrOfVisibleTreesBelow = GetVisibleTreesInList(treesInColBelow, currentTree);
                    var nrOfVisibleTreesToTheRight = GetVisibleTreesInList(treesToTheRight, currentTree);
                    var nrOfVisibleTreesToTheLeft = GetVisibleTreesInList(treesToTheLeft, currentTree);
                    scenicScores.Add(nrOfVisibleTreesAbove * nrOfVisibleTreesBelow * nrOfVisibleTreesToTheLeft * nrOfVisibleTreesToTheRight);
                }
            }
            return scenicScores.Max();
        }

        private int HandlePartOne()
        {
            int treesVisible = 0;
            for (int r = 0; r < linesAsInt.Count; r++)
            {
                for (var c = 0; c < linesAsInt[r].Count; c++)
                {
                    var currentTree = linesAsInt[r][c];

                    var treesInColAbove = linesAsInt.Take(r).Select(l => l[c]);
                    var treesAreLowerAbove = !treesInColAbove.Any(t => t>=currentTree);

                    var treesInColBelow = linesAsInt.Skip(r + 1).Select(l => l[c]);
                    var treesAreLowerBelow = !treesInColBelow.Any(t => t >= currentTree);

                    var treesToTheLeft = linesAsInt[r].Take(c);
                    var treesAreLowerOnTheLeft = !treesToTheLeft.Any(t => t >= currentTree);

                    var treesToTheRight = linesAsInt[r].Skip(c + 1);
                    var treesAreLowerOnTheRight = !treesToTheRight.Any(t => t >= currentTree);

                    if (treesAreLowerOnTheLeft || treesAreLowerOnTheRight || treesAreLowerAbove || treesAreLowerBelow) treesVisible++;
                }
            }
            return treesVisible;
        }

        [Theory]
        [InlineData("30373\r\n25512\r\n65332\r\n33549\r\n35390", "21")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay8(input);
            var result = program.RunPartOne();
            result.Should().StartWithEquivalentOf(expectedResult);
        }

        [Theory]
        [InlineData("30373\r\n25512\r\n65332\r\n33549\r\n35390", "8")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay8(input);
            var result = program.RunPartTwo();
            result.Should().EndWithEquivalentOf(expectedResult);
        }

    }
}
