using System.Text.RegularExpressions;
using AdventOfCodeShared;

namespace AdventOfCode2023;

public record ScratchCard(int cardNumber, IEnumerable<int> WinningNumbers)
{ }

public class ProgramDay4 : AdventOfCodeProgram
{
    public ProgramDay4(string? text = null) : base(text)
    {
    }

    [Theory]
    [InlineData(@"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
"13")]
    public override void RunTestsPartOne(string input, string expectedResult)
    {
        var program = new ProgramDay4(input);
        var result = program.RunPartOne();
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
"30")]
    public override void RunTestsPartTwo(string input, string expectedResult)
    {
        var program = new ProgramDay4(input);
        var result = program.RunPartTwo();
        result.Should().Be(expectedResult);
    }

    protected override string RunPartOne()
    {
        var total = Lines
        .Select(CreateScratchCards)
        .Where(c => c.WinningNumbers.Any())
        .Select(c => Math.Pow(2, c.WinningNumbers.Count() - 1))
        .Sum();

        return total.ToString();
    }

    protected override string RunPartTwo()
    {
        var allScratchCards = Lines.Select(CreateScratchCards);
        var cardDictionary = allScratchCards.ToDictionary(c => c.cardNumber, c => 1);
        foreach (var card in allScratchCards)
        {
            var nrOfWins = card.WinningNumbers.Count();
            for (int i = card.cardNumber + 1; i <= card.cardNumber + nrOfWins; i++)
            {
                cardDictionary[i] += cardDictionary[card.cardNumber];
            }
            // var copies = allScratchCards.Where(c => c.Key.cardNumber > card.cardNumber && c.Key.cardNumber <= card.Key.cardNumber + card.Key.WinningNumbers.Count());

        }
        // var copies = winningCards.SelectMany(c => GetCopies(c, allScratchCards)).ToList();
        // var totalCards = 0;
        // var cardsWithNoWins = allScratchCards.Where(c => !c.WinningNumbers.Any());
        // copies.AddRange(cardsWithNoWins);
        // var grouped = copies.GroupBy(c => c.cardNumber).Select(g => new { CardNumber = g.Key, NrOfCopies = g.Count() }).OrderBy(c => c.CardNumber);

        return cardDictionary.Select(d => d.Value).Sum().ToString();
    }

    // #TODO stuk in infinite loop
    private IEnumerable<ScratchCard> GetCopies(ScratchCard card, IEnumerable<ScratchCard> allScratchCards)
    {
        var result = new List<ScratchCard>() { card };
        if (!card.WinningNumbers.Any()) return result;
        var copies = allScratchCards.Where(c => c.cardNumber > card.cardNumber && c.cardNumber <= card.cardNumber + card.WinningNumbers.Count());
        foreach (var c in copies) result.AddRange(GetCopies(c, allScratchCards));
        Console.WriteLine("Returning for card: {0}", card.cardNumber);
        return result;
    }

    private ScratchCard CreateScratchCards(string card, int cardNumber)
    {
        var trimmed = card.Split(':')[1];
        var winningSide = trimmed.Split("|")[0];
        var ownedSide = trimmed.Split("|")[1];
        var winningNumbers = Regex.Matches(winningSide, @"\d+").Select(m => int.Parse(m.Value));
        var ownedNumbers = Regex.Matches(ownedSide, @"\d+").Select(m => int.Parse(m.Value));
        var matches = ownedNumbers.Intersect(winningNumbers);
        return new(cardNumber + 1, matches); // + 1 because of zero index based linq
    }
}
