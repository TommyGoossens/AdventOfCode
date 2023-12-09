using System.Text.RegularExpressions;
using AdventOfCodeShared;
using AdventOfCodeShared.Models;

namespace AdventOfCode2023.Day3
{
    public record Part(Point From, Point To, int PartValue) : Line(From, To)
    {

    }

    public class ProgramDay3 : AdventOfCodeProgram<int>
    {
        public ProgramDay3(string? text = null) : base(text)
        {
        }

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

        protected override int RunPartOne()
        {
            var partsCoordinates = Lines.SelectMany(CreatePartCoordinates);
            var symbols = Lines.SelectMany(CreateSymbolCoordinates);
            var sum = partsCoordinates.Where(p => IsAdjacentToSymbol(p, symbols)).Sum(p => p.PartValue);
            return sum;
        }

        protected override int RunPartTwo()
        {
            var getGearCoordinates = Lines.SelectMany(CreateGearCoordinates);
            var partsCoordinates = Lines.SelectMany(CreatePartCoordinates);
            var pairs = getGearCoordinates.Select(g => FindPartPairs(g, partsCoordinates)).Distinct();
            return pairs.Where(p => p != null).Distinct().Sum(p => p!.Value.FirstPart * p.Value.SecondPart);
        }

        private IEnumerable<Point> CreateGearCoordinates(string line, int index)
        {
            return Regex.Matches(line, @"\*").Select(m => new Point(m.Index, index));
        }

        private IEnumerable<Part> CreatePartCoordinates(string line, int rowIndex)
        {
            return Regex.Matches(line, @"\d+").Select(m =>
            new Part(new(m.Index, rowIndex), new(m.Index + m.Length - 1, rowIndex), int.Parse(m.Value)));
        }

        private IEnumerable<Point> CreateSymbolCoordinates(string line, int rowIndex)
        {
            return Regex.Matches(line, @"[^.\d]").Select(m => new Point(m.Index, rowIndex));
        }

        private bool IsAdjacentToSymbol(Part part, IEnumerable<Point> symbolCoordinates)
        {
            var searchArea = new Rectangle(new(part.From.X - 1, part.From.Y - 1), new(part.To.X + 1, part.To.Y + 1));
            return symbolCoordinates.Any(searchArea.PointIsWithinBorder);
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
    }
}