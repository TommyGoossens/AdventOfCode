using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day1
{
    public class ProgramDay1 : AdventOfCodeProgram<int>
    {
        public ProgramDay1(string? text = null) : base(text)
        {
        }

        protected override int RunPartOne() => GetAnswerPart1();

        protected override int RunPartTwo() => GetAnswerPart2();

        private int GetAnswerPart1()
        {
            int? previousDepth = null;
            int nrOfIncreases = 0;
            foreach (var line in Lines)
            {
                int.TryParse(line, out var currDepth);
                if (previousDepth == null) { previousDepth = currDepth; continue; }
                if (currDepth > previousDepth) nrOfIncreases++;
                previousDepth = currDepth;
            }

            return nrOfIncreases;
        }

        private int GetAnswerPart2()
        {
            int nrOfLargerSums = 0;
            int? previousDepth = null;
            for (int i = 0; i < Lines.Count(); i++)
            {
                var sumOfWindow = 0;
                try
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var line = Lines.ElementAt(i + j);
                        int.TryParse(line, out var currDepth);
                        sumOfWindow += currDepth;

                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }

                if (previousDepth == null) { previousDepth = sumOfWindow; continue; }
                if (sumOfWindow > previousDepth) nrOfLargerSums++;
                previousDepth = sumOfWindow;
            }

            return nrOfLargerSums;
        }

        [Theory]
        [InlineData("199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", 7)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay1(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", 5)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay1(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
