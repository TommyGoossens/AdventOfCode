using AdventOfCodeShared;

namespace AdventOfCode2023;

public class TestsDay3 : AdventOfCodeTestRunner<int>
{

    [Theory]
    [InlineData("467..114..\n...*......\n..35..633.\n......#...\n617*......\n.....+.58.\n..592.....\n......755.\n...$.*....\n.664.598.*\n.......100", 4461)]
    public override void RunTestsPartOne(string input, int expectedResult)
    {
        new ProgramDay3(input).RunPartOne().Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("467..114..\n...*......\n..35..633.\n......#...\n617 * ......\n.....+.58.\n..592.....\n......755.\n...$.*....\n.664.598..", 467835)]
    public override void RunTestsPartTwo(string input, int expectedResult)
    {
        new ProgramDay3(input).RunPartTwo().Should().Be(expectedResult);
    }
}