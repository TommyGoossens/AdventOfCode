using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day1
{
    public class ProgramDay1 : AdventOfCodeProgram
    {
        public ProgramDay1(string? text = null) : base(text)
        {
        }

        protected override string RunPartOne()
        {
            var nrOfIncreases = GetAnswerPart1();
            return $"{nrOfIncreases} measurements that are larger than the previous.";
        }

        protected override string RunPartTwo()
        {
            var nrOfLargerSums = GetAnswerPart2();
            return $"{nrOfLargerSums} sums that are larger than the previous sum";
        }

        private int GetAnswerPart1()
        {
            int? previousDepth = null;
            int nrOfIncreases = 0;
            foreach (var line in lines)
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
            for (int i = 0; i < lines.Length; i++)
            {
                var sumOfWindow = 0;
                try
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var line = lines[i + j];
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
        [InlineData("199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", "7")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay1(input);
            var result = program.RunPartOne();
            result.Should().StartWithEquivalentOf(expectedResult);
        }

        [Theory]
        [InlineData("199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", "5")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay1(input);
            var result = program.RunPartTwo();
            result.Should().StartWithEquivalentOf(expectedResult);
        }
    }
}
