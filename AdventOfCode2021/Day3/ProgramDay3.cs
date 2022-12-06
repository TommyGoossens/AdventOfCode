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
    public class ProgramDay3 : AdventOfCodeProgram
    {
        public ProgramDay3(string? text = null) : base(text)
        {
        }

        protected override string[] Run()
        {
            var (gamma, epsilon) = SolvePartOne();
            var (oxygen, scrubber) = SolvePartTwo();
            return new string[] { $"Gamma ({gamma}) * Epsilon ({epsilon}) = {gamma * epsilon}", $"Oxygen ({oxygen}) * Scrubber ({scrubber}) = {oxygen * scrubber}" };
        }

        private (int gamma, int epsilon) SolvePartOne()
        {
            var binaryNumber = "";
            for (int i = 0; i < lines.First().Length; i++)
            {
                var col = lines.Select(l => l[i]).GroupBy(i => i);
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
            for (int i = 0; i < lines.First().Length; i++)
            {
                var colWithMostCommon = lines.Where(l => l.StartsWith(oxygenStr)).Select(l => l[i]).GroupBy(i => i).Select(g => new { nr = g.Key, count = g.Count() });
                var colWithleastCommon = lines.Where(l => l.StartsWith(scrubberStr)).Select(l => l[i]).GroupBy(i => i).Select(g => new { nr = g.Key, count = g.Count() });
                var mostCommon = colWithMostCommon.OrderByDescending(g => g.count).ThenByDescending(g => g.nr).First().nr;
                var leastCommon = colWithleastCommon.OrderBy(g => g.count).ThenBy(g => g.nr).First().nr;
                oxygenStr += $"{mostCommon}";
                scrubberStr += $"{leastCommon}";
            }
            var oxygen = Convert.ToInt32(oxygenStr.To8BitString(), 2);
            var scrubber = Convert.ToInt32(scrubberStr.To8BitString(), 2);
            return (oxygen, scrubber);
        }

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay3("00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().EndWithEquivalentOf("198");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay3("" +
                "00100\r\n" +
                "11110\r\n" +
                "10110\r\n" +
                "10111\r\n" +
                "10101\r\n" +
                "01111\r\n" +
                "00111\r\n" +
                "11100\r\n" +
                "10000\r\n" +
                "11001\r\n" +
                "00010\r\n" +
                "01010");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().EndWithEquivalentOf("230");
        }
    }
}
