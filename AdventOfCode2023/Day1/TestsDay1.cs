using AdventOfCodeShared;

namespace AdventOfCode2023;

public class TestsDay1 : AdventOfCodeTestRunner<int>
{
    [Theory()]
    [InlineData("1abc2\npqr3stu8vwx\na1b2c3d4e5f\ntreb7uchet", 142)]
    public override void RunTestsPartOne(string input, int expectedResult)
    {
        new ProgramDay1(input).RunPartOne().Should().Be(expectedResult);

    }

    [Theory()]
    [InlineData("two1nine\neightwothree\nabcone2threexyz\nxtwone3four\n4nineeightseven2\nzoneight234\n7pqrstsixteen", 281)]
    public override void RunTestsPartTwo(string input, int expectedResult)
    {
        new ProgramDay1(input).RunPartTwo().Should().Be(expectedResult);
    }
}