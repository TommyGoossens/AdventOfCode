using System.Text.RegularExpressions;
using AdventOfCodeShared;

namespace AdventOfCode2023;

public class ProgramDay2(string? text = null) : AdventOfCodeProgram<int>(text)
{
    public override int RunPartOne()
    {
        int nrOfRed = 12, nrOfGreen = 13, nrOfBlue = 14;
        var gameIds = Lines
            .Select(ParseGameRequirements)
            .Where(g => g.Red <= nrOfRed && g.Green <= nrOfGreen && g.Blue <= nrOfBlue)
            .Select(g => g.GameId)
            .Sum();
        return gameIds;
    }

    public override int RunPartTwo()
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
