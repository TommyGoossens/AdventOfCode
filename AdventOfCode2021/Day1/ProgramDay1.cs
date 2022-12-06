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

        protected override string[] Run()
        {
            var nrOfIncreases = GetAnswerPart1();
            var nrOfLargerSums = GetAnswerPart2();
            return new string[]
            {
                $"{nrOfIncreases} measurements that are larger than the previous.",
                $"{nrOfLargerSums} sums that are larger than the previous sum"
            };
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

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay1("199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().StartWithEquivalentOf("7");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay1("199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().StartWithEquivalentOf("5");
        }
    }
}
