using System.Text.RegularExpressions;
using AdventOfCodeShared;

namespace AdventOfCode2023.Day2
{
    public class ProgramDay2 : AdventOfCodeProgram<int>
    {
        public ProgramDay2(string? text = null) : base(text)
        {
        }

        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 8)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay2(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 2286)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay2(input).RunPartTwo().Should().Be(expectedResult);
        }

        protected override int RunPartOne()
        {
            int nrOfRed = 12, nrOfGreen = 13, nrOfBlue = 14;
            var gameIds = Lines
                .Select(ParseGameRequirements)
                .Where(g => g.Red <= nrOfRed && g.Green <= nrOfGreen && g.Blue <= nrOfBlue)
                .Select(g => g.GameId)
                .Sum();
            return gameIds;
        }

        protected override int RunPartTwo()
        {
            var parsedGames = Lines
                .Select(ParseGameRequirements)
               .Select(g => g.Red * g.Green * g.Blue)
               .Sum();
            return parsedGames;
        }

        private (int GameId, int Red, int Green, int Blue) ParseGameRequirements(string l)
        {
            var gameId = Regex.Match(l, @"Game (\d{1,})").Result("$1");
            var red = Regex.Matches(l, @"(\d{1,}) red").Select(m => m.Result("$1")).Select(int.Parse).Max();
            var green = Regex.Matches(l, @"(\d{1,}) green").Select(m => m.Result("$1")).Select(int.Parse).Max();
            var blue = Regex.Matches(l, @"(\d{1,}) blue").Select(m => m.Result("$1")).Select(int.Parse).Max();
            return (int.Parse(gameId), red, green, blue);
        }
    }
}