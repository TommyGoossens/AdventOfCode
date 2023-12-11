using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day3
{
    internal static class Helper
    {
        internal static string To8BitString(this string original)
        {
            var len = original.Length;
            var missing = 8 - len;
            for (int i = 0; i < missing; i++) original = "0" + original;
            return original;
        }

        internal static string FlipBits(this string original)
        {
            var result = "";
            foreach (var c in original) result += c == '0' ? "1" : "0";
            return result.To8BitString();
        }

    }
    public class ProgramDay3 : AdventOfCodeProgram<int>
    {
        public ProgramDay3(string? text = null) : base(text)
        {
        }

        public override int RunPartOne()
        {
            var (gamma, epsilon) = SolvePartOne();
            return gamma * epsilon;
        }

        public override int RunPartTwo()
        {
            var (oxygen, scrubber) = SolvePartTwo();
            return oxygen * scrubber;
        }

        private (int gamma, int epsilon) SolvePartOne()
        {
            var binaryNumber = "";
            for (int i = 0; i < Lines.First().Length; i++)
            {
                var col = Lines.Select(l => l[i]).GroupBy(i => i);
                var mostCommon = col.OrderByDescending(g => g.Count()).First().Key;
                binaryNumber += $"{mostCommon}";
            }
            var gamma = Convert.ToInt32(binaryNumber.To8BitString(), 2);
            var epsilon = Convert.ToInt32(binaryNumber.FlipBits(), 2);

            return (gamma, epsilon);
        }


        private (int oxygen, int scrubber) SolvePartTwo()
        {
            var oxygenStr = "";
            var scrubberStr = "";
            for (int i = 0; i < Lines.First().Length; i++)
            {
                var colWithMostCommon = Lines.Where(l => l.StartsWith(oxygenStr)).Select(l => l[i]).GroupBy(i => i).Select(g => new { nr = g.Key, count = g.Count() });
                var colWithleastCommon = Lines.Where(l => l.StartsWith(scrubberStr)).Select(l => l[i]).GroupBy(i => i).Select(g => new { nr = g.Key, count = g.Count() });
                var mostCommon = colWithMostCommon.OrderByDescending(g => g.count).ThenByDescending(g => g.nr).First().nr;
                var leastCommon = colWithleastCommon.OrderBy(g => g.count).ThenBy(g => g.nr).First().nr;
                oxygenStr += $"{mostCommon}";
                scrubberStr += $"{leastCommon}";
            }
            var oxygen = Convert.ToInt32(oxygenStr.To8BitString(), 2);
            var scrubber = Convert.ToInt32(scrubberStr.To8BitString(), 2);
            return (oxygen, scrubber);
        }

        [Theory]
        [InlineData("00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", 198)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay3(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", 230)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay3(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
