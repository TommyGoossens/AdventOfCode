using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCodeShared;
using AdventOfCodeShared.Models;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2023.Day1
{
    public class ProgramDay1 : AdventOfCodeProgram
    {
        public ProgramDay1(string? text = null) : base(text)
        {
        }

        [Theory()]
        [InlineData("1abc2\npqr3stu8vwx\na1b2c3d4e5f\ntreb7uchet", "142")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay1(input);
            var result = program.RunPartOne();
            result.Should().EndWithEquivalentOf(expectedResult);
        }

        [Theory()]
        [InlineData("two1nine\neightwothree\nabcone2threexyz\nxtwone3four\n4nineeightseven2\nzoneight234\n7pqrstsixteen", "281")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay1(input);
            var result = program.RunPartTwo();
            result.Should().EndWithEquivalentOf(expectedResult);
        }

        protected override string RunPartOne()
        {
            var sum = Lines
            .Select(line => Regex.Replace(line, "[a-z]", ""))
            .Select(line => $"{line.First()}{line.Last()}")
            .Select(int.Parse)
            .Sum();
            return $"Total is: {sum}";
        }

        protected override string RunPartTwo()
        {
            var sum = Lines
            .Select(ReplaceOverlappingDigitsAndRemoveUnwantedChars)
            .Select(line => $"{line.First()}{line.Last()}")
            .Select(int.Parse)
            .Sum();

            return $"Total is: {sum}";
        }

        private string ReplaceOverlappingDigitsAndRemoveUnwantedChars(string digit)
        {
            var result = replaces.Aggregate(digit, (current, replace) => current.Replace(replace.Key, replace.Value));
            return Regex.Replace(result, "[a-z]", "");
        }

        Dictionary<string, string> replaces = new()
        {
            {"one", "o1e"},
            {"two", "t2o"},
            {"three", "t3e"},
            {"four", "4"},
            {"five", "5e"},
            {"six", "6"},
            {"seven", "7n"},
            {"eight", "e8t"},
            {"nine", "n9e"},
        };
    }
}

