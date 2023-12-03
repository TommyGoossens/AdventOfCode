using System.Text.RegularExpressions;
using AdventOfCodeShared;
using AdventOfCodeShared.Models;

namespace AdventOfCode2023.Day3
{
    public class ProgramDay3 : AdventOfCodeProgram
    {
        public ProgramDay3(string? text = null) : base(text)
        {
        }
        [Theory]
        [InlineData("467..114..\n...*......\n..35..633.\n......#...\n617*......\n.....+.58.\n..592.....\n......755.\n...$.*....\n.664.598.*\n.......100", "4461")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay3(input);
            var result = program.RunPartOne();
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("467..114..\n...*......\n..35..633.\n......#...\n617*......\n.....+.58.\n..592.....\n......755.\n...$.*....\n.664.598.*\n.......100", "467835")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay3(input);
            var result = program.RunPartTwo();
            result.Should().Be(expectedResult);
        }

        protected override string RunPartOne()
        {
            var parts = Lines
            .Select((l, i) => (l, i))
            .SelectMany(GetIndicesOfDigits)
            .Where(IsAdjacentToSymbol)
            .Select(GetPartNumber);
            return parts.Sum(p => p.PartNumber).ToString();
        }


        private IEnumerable<Point> CreateGearCoordinates(string line, int index)
        {
            return Regex.Matches(line, "/*")
            .Select(m => new Point(m.Index, index));
        }



        protected override string RunPartTwo()
        {

            IEnumerable<Point> getGearCoordinates = Lines
            .SelectMany(CreateGearCoordinates);



            var parts = Lines
            .Select((l, i) => (l, i))
            .SelectMany(GetIndicesOfDigits)
            .Where(IsAdjacentToSymbol)
            .Select(GetPartNumber);
            return parts.Sum(p => p.PartNumber).ToString();
        }



        private (int PartNumber, int Line) GetPartNumber((int LineNumber, int StartIndex, int Length) tuple, int arg2)
        {
            var (lineNumber, index, length) = tuple;
            var currentLine = Lines[lineNumber];
            var digitString = currentLine.Substring(index, length);
            return (int.Parse(digitString), lineNumber);
        }

        private bool IsAdjacentToSymbol((int LineNumber, int StartIndex, int Length) tuple)
        {
            var (lineNumber, index, length) = tuple;
            var currentLine = Lines[lineNumber];
            var minCharIndex = Math.Max(0, index - 1);
            var maxCharIndex = index + length >= currentLine.Length ? currentLine.Length - 1 : index + length;
            var currentNumber = GetPartNumber(tuple, 0);
            var maxLength = Math.Min(length + 2, currentLine.Length - minCharIndex);
            var prevLine = Lines[Math.Max(0, lineNumber - 1)];
            var prevLineLimited = prevLine.Substring(minCharIndex, maxLength);
            var prevLineContainsSymbol = prevLineLimited.Select(IsSymbol).Any(b => b == true);

            var nextLine = Lines[Math.Min(Lines.Length - 1, lineNumber + 1)];
            var nextLineLimited = nextLine.Substring(minCharIndex, maxLength);
            var nextLineContainsSymbol = nextLineLimited.Select(IsSymbol).Any(b => b == true);

            char prevChar = currentLine[minCharIndex], nextChar = currentLine[maxCharIndex];
            var currentLineContainsSybol = IsSymbol(prevChar) || IsSymbol(nextChar);
            if (prevLineContainsSymbol || currentLineContainsSybol || nextLineContainsSymbol) return true;
            return false;
        }

        private bool IsSymbol(char v)
        {
            if (Regex.Match(v.ToString(), @"\d").Success || v == '.') return false;
            return true;
        }

        private IEnumerable<(int LineNumber, int StartIndex, int Length)> GetIndicesOfDigits((string l, int i) tuple)
        {
            var (line, rowIndex) = tuple;
            var matches = Regex.Matches(line, @"\d+");
            return matches.Select(m => (rowIndex, m.Index, m.Length));
        }
    }
}