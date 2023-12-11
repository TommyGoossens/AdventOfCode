using AdventOfCodeShared;
using AdventOfCodeShared.Models;

namespace AdventOfCode2022.Day9
{
    public class ProgramDay9 : AdventOfCodeProgram<int>
    {
        public ProgramDay9(string? text = null) : base(text)
        { }
        public override int RunPartOne()
        {
            var rope = new Rope(1);
            foreach ((char direction, int steps) in Lines.Select(l => (l[0], int.Parse(l.Split(" ").Last())))) rope.Move(direction, steps);
            return rope.Knots[1].Distinct().Count();
        }

        public override int RunPartTwo()
        {
            var rope = new Rope(9);
            foreach ((char direction, int steps) in Lines.Select(l => (l[0], int.Parse(l.Split(" ").Last())))) rope.Move(direction, steps);
            return rope.Knots[9].Distinct().Count();
        }

        [Theory]
        [InlineData("R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2", 13)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay9(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20", 36)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay9(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
