using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2022.Day6
{
    public class ProgramDay6 : AdventOfCodeProgram
    {
        public ProgramDay6() : base(6)
        {
        }

        protected override string[] Run()
        {
            var part1 = GetResult(4);
            var part2 = GetResult(14);
            return new string[] { $"{part1.nrOfLettersToProcess} letters processed to get result: {string.Join("", part1.letters)}", $"{part2.nrOfLettersToProcess} letters processed to get result: {string.Join("", part2.letters)}" };
        }

        private (int nrOfLettersToProcess, HashSet<char> letters) GetResult(int nrOfUniqueLetters)
        {
            HashSet<char> set = new HashSet<char>();
            var firstMarkerAfter = 0;
            foreach (var l in lines)
            {
                for (int i = 0; i < l.Length; i++)
                {
                    set = l.Skip(i).Take(nrOfUniqueLetters).ToHashSet();
                    if (set.Count() == nrOfUniqueLetters)
                    {
                        firstMarkerAfter = i + nrOfUniqueLetters;
                        break;
                    }
                }
                if (firstMarkerAfter != 0) break;
            }
            return (firstMarkerAfter, set);
        }


        [Fact]
        public void PartOneShouldGiveCorrectOutput()
        {
            var program = new ProgramDay6();
            program.CreateTestInput("mjqjpqmgbljsphdztnvjfqwrcgsmlb");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().StartWith("7");

            program.CreateTestInput("bvwbjplbgvbhsrlpgdmjqwftvncz");
            result = program.Run();
            part1 = result.FirstOrDefault();
            part1.Should().StartWith("5");

            program.CreateTestInput("nppdvjthqldpwncqszvftbrmjlhg");
            result = program.Run();
            part1 = result.FirstOrDefault();
            part1.Should().StartWith("6");

            program.CreateTestInput("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg");
            result = program.Run();
            part1 = result.FirstOrDefault();
            part1.Should().StartWith("10");

            program.CreateTestInput("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw");
            result = program.Run();
            part1 = result.FirstOrDefault();
            part1.Should().StartWith("11");



        }


        [Fact]
        public void PartTwoShouldGiveCorrectOutput()
        {
            var program = new ProgramDay6();
            program.CreateTestInput("bvwbjplbgvbhsrlpgdmjqwftvncz");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().StartWith("23");
        }
    }
}
