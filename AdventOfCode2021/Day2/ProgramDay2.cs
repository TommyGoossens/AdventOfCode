using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day2
{
    public class ProgramDay2 : AdventOfCodeProgram
    {
        public ProgramDay2(string? text = null) : base(text)
        {
        }
        protected override string[] Run()
        {
            int horizontalPos = 0;
            int part1Depth = 0;
            int part2Depth = 0;
            int aim = 0;

            foreach ((string direction, int units) in lines.Select(l => (l.Split(' ')[0], int.Parse(l.Split(' ')[1]))))
            {
                switch (direction)
                {
                    case "forward":
                        horizontalPos += units;
                        part2Depth += aim * units;
                        break;
                    case "down":
                        part1Depth += units;
                        aim += units;
                        break;
                    case "up":
                        part1Depth -= units;
                        aim -= units;
                        break;
                }
            }

            return new string[] {
                $"Horizontal * depth = {horizontalPos * part1Depth}",
                $"Horizontal * depth (adjusted with aim) = {horizontalPos * part2Depth}"
            };
        }

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay2("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().EndWithEquivalentOf("150");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay2("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().EndWithEquivalentOf("900");
        }
    }
}
