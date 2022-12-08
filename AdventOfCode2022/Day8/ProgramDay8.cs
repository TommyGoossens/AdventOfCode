using AdventOfCodeShared;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AdventOfCode2022.Day8
{
    public class ProgramDay8 : AdventOfCodeProgram
    {
        public ProgramDay8(string? text = null) : base(text)
        {
        }

        protected override string[] Run()
        {
            var linesAsInt = lines.Select(l => l.Select(c => char.GetNumericValue(c)));
            int part1 = HandlePartOne(linesAsInt);
            int part2 = HandlePartTwo(linesAsInt);


            return new string[] { $"{part1} trees are visible", $"Highest scenic score is: {part2}" };
        }

        private int HandlePartTwo(IEnumerable<IEnumerable<double>> linesAsInt)
        {
            var scenicScores = new List<int>();
            for (int r = 0; r < linesAsInt.Count(); r++)
            {
                var row = linesAsInt.ElementAt(r);
                for (var c = 0; c < row.Count(); c++)
                {
                    var currentTree = row.ElementAt(c);
                    var treesInColAbove = linesAsInt.Take(r).Select(l => l.ElementAt(c)).ToList();
                    treesInColAbove.Reverse();
                    var treesInColBelow = linesAsInt.Skip(r + 1).Select(l => l.ElementAt(c)).ToList();
                    var treesToTheRight = row.Skip(c + 1).ToList();                    
                    var treesToTheLeft = row.Take(c).ToList();
                    treesToTheLeft.Reverse();

                    var nrOfVisibleTreesAbove = 0;
                    var nrOfVisibleTreesBelow = 0;
                    var nrOfVisibleTreesToTheRight = 0;
                    var nrOfVisibleTreesToTheLeft = 0;

                    foreach (var tree in treesInColAbove)
                    {
                        if (tree < currentTree) nrOfVisibleTreesAbove++;
                        if (tree >= currentTree) { nrOfVisibleTreesAbove++; break; }
                    }

                    foreach (var tree in treesInColBelow)
                    {
                        if (tree < currentTree) nrOfVisibleTreesBelow++;
                        if (tree >= currentTree) { nrOfVisibleTreesBelow++; break; }
                    }

                    foreach (var tree in treesToTheRight)
                    {
                        if (tree < currentTree) nrOfVisibleTreesToTheRight++;
                        if (tree >= currentTree) { nrOfVisibleTreesToTheRight++; break; }
                    }

                    foreach (var tree in treesToTheLeft)
                    {
                        if (tree < currentTree) nrOfVisibleTreesToTheLeft++;
                        if (tree >= currentTree) { nrOfVisibleTreesToTheLeft++; break; }
                    }

                    scenicScores.Add(nrOfVisibleTreesAbove * nrOfVisibleTreesBelow * nrOfVisibleTreesToTheLeft * nrOfVisibleTreesToTheRight);
                }
            }
            return scenicScores.Max();
        }

        private int HandlePartOne(IEnumerable<IEnumerable<double>> linesAsInt)
        {
            int treesVisible = 0;
            for (int r = 0; r < linesAsInt.Count(); r++)
            {
                var row = linesAsInt.ElementAt(r);
                for (var c = 0; c < row.Count(); c++)
                {
                    var currentTree = row.ElementAt(c);
                    var treesInColAbove = linesAsInt.Take(r).Select(l => l.ElementAt(c)).DefaultIfEmpty(-1).ToList();
                    var treesAreLowerAbove = treesInColAbove.Max() < currentTree;
                    var treesInColBelow = linesAsInt.Skip(r + 1).Select(l => l.ElementAt(c)).DefaultIfEmpty(-1).ToList();
                    var treesAreLowerBelow = treesInColBelow.Max() < currentTree;

                    var treesToTheLeft = row.Take(c).DefaultIfEmpty(-1).ToList();
                    var treesAreLowerOnTheLeft = treesToTheLeft.Max() < currentTree;
                    var treesToTheRight = row.Skip(c + 1).DefaultIfEmpty(-1).ToList();
                    var treesAreLowerOnTheRight = treesToTheRight.Max() < currentTree;
                    if (treesAreLowerOnTheLeft || treesAreLowerOnTheRight || treesAreLowerAbove || treesAreLowerBelow) treesVisible++;
                }
            }
            return treesVisible;
        }

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay8("30373\r\n25512\r\n65332\r\n33549\r\n35390");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().StartWithEquivalentOf("21");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay8("30373\r\n25512\r\n65332\r\n33549\r\n35390");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().EndWithEquivalentOf("8");
        }

    }
}
