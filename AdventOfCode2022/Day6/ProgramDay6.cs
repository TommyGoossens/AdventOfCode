using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2022.Day6
{
    public class ProgramDay6 : AdventOfCodeProgram
    {
        public ProgramDay6(string? text = null) : base(text)
        {
        }

        protected override string RunPartOne()
        {
            var part1 = GetResult(4);

            return $"{part1.nrOfLettersToProcess} letters processed to get result: {string.Join("", part1.letters)}";
        }

        protected override string RunPartTwo()
        {
            var part2 = GetResult(14);
            return $"{part2.nrOfLettersToProcess} letters processed to get result: {string.Join("", part2.letters)}";
        }

        private (int nrOfLettersToProcess, IEnumerable<char> letters) GetResult(int nrOfUniqueLetters)
        {
            var line = string.Join("", lines);
            var i = 0;
            while (line.Skip(i).Take(nrOfUniqueLetters).ToHashSet().Count != nrOfUniqueLetters) i++;
            var letters = line.Skip(i).Take(nrOfUniqueLetters);
            return (i + nrOfUniqueLetters, letters);
        }

        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "7")]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", "5")]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", "6")]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "10")]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "11")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay6(input);
            var result = program.RunPartOne();
            result.Should().StartWith(expectedResult);
        }

        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "19")]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", "23")]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", "23")]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "29")]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "26")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay6(input);
            var result = program.RunPartTwo();
            result.Should().StartWith(expectedResult);
        }
    }
}
