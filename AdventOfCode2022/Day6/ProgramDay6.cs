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

        protected override string[] Run()
        {
            var part1 = GetResult(4);
            var part2 = GetResult(14);
            return new string[] { $"{part1.nrOfLettersToProcess} letters processed to get result: {string.Join("", part1.letters)}", $"{part2.nrOfLettersToProcess} letters processed to get result: {string.Join("", part2.letters)}" };
        }

        private (int nrOfLettersToProcess, IEnumerable<char> letters) GetResult(int nrOfUniqueLetters)
        {        
            var line = string.Join("", lines);
            var i = 0;
            while(line.Skip(i).Take(nrOfUniqueLetters).ToHashSet().Count != nrOfUniqueLetters) i++;
            var letters = line.Skip(i).Take(nrOfUniqueLetters);
            return (i + nrOfUniqueLetters, letters);
        }

        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "7")]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", "5")]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", "6")]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "10")]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "11")]
        public void PartOneShouldGiveCorrectOutput(string input, string nrOfChars)
        {
            var program = new ProgramDay6(input);
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().StartWith(nrOfChars);
        }

        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "19")]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", "23")]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", "23")]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "29")]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "26")]
        public void PartTwoShouldGiveCorrectOutput(string input, string nrOfChars)
        {
            var program = new ProgramDay6(input);
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().StartWith(nrOfChars);
        }
    }
}
