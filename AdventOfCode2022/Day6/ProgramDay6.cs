using AdventOfCodeShared;

namespace AdventOfCode2022.Day6
{
    public class ProgramDay6 : AdventOfCodeProgram<int>
    {
        public ProgramDay6(string? text = null) : base(text)
        {
        }

        public override int RunPartOne()
        {
            var (nrOfLettersToProcess, _) = GetResult(4);
            return nrOfLettersToProcess;
        }

        public override int RunPartTwo()
        {
            var (nrOfLettersToProcess, _) = GetResult(14);
            return nrOfLettersToProcess;
        }

        private (int nrOfLettersToProcess, IEnumerable<char> letters) GetResult(int nrOfUniqueLetters)
        {
            var line = string.Join("", Lines);
            var i = 0;
            while (line.Skip(i).Take(nrOfUniqueLetters).ToHashSet().Count != nrOfUniqueLetters) i++;
            var letters = line.Skip(i).Take(nrOfUniqueLetters);
            return (i + nrOfUniqueLetters, letters);
        }

        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay6(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay6(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
