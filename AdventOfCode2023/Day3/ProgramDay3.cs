using System.Text.RegularExpressions;
using AdventOfCodeShared;
using AdventOfCodeShared.Models;

namespace AdventOfCode2023.Day3
{

    public class Part : Line
    {
        public static int Id { get; private set; } = 0;
        public int PartValue { get; private init; }
        public Part(Point from, Point to, int value) : base(from, to)
        {
            PartValue = value;
            Id++;
        }
    }

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
        [InlineData("467..114..\n...*......\n..35..633.\n......#...\n617 * ......\n.....+.58.\n..592.....\n......755.\n...$.*....\n.664.598..", "467835")]
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
            return Regex.Matches(line, @"\*")
            .Select(m => new Point(m.Index, index));
        }

        private IEnumerable<Part> CreatePartCoordinates(string line, int rowIndex)
        {
            return Regex.Matches(line, @"\d+").Select(m =>
            new Part(new(m.Index, rowIndex), new(m.Index + m.Length - 1, rowIndex), int.Parse(m.Value)));
        }
        protected override string RunPartTwo()
        {
            var getGearCoordinates = Lines.SelectMany(CreateGearCoordinates);
            var partsCoordinates = Lines.SelectMany(CreatePartCoordinates);
            var pairs = getGearCoordinates.Select(g => FindPartPairs(g, partsCoordinates)).Distinct();
            return pairs.Where(p => p != null).Distinct().Sum(p => p!.Value.FirstPart * p.Value.SecondPart).ToString();
        }

        private (int FirstPart, int SecondPart)? FindPartPairs(Point gear, IEnumerable<Part> partsCoordinates)
        {
            var topLeftPoint = new Point(gear.X - 1, gear.Y - 1);
            var searchArea = new Rectangle(topLeftPoint, 2, 2);
            var partsAroundCoordinates = partsCoordinates.Where(p => searchArea.CoordinatesAreWithinRectangle(p));
            if (partsAroundCoordinates.Count() == 2)
            {
                return (partsAroundCoordinates.First().PartValue, partsAroundCoordinates.Last().PartValue);
            }
            return null;

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