using AdventOfCodeShared;

namespace AdventOfCode2023;

public record ScratchCard(int cardNumber, IEnumerable<int> WinningNumbers)
{ }

public class ProgramDay4(string? text = null) : AdventOfCodeProgram<long>(text)
{
    public override long RunPartOne()
    {
        var total = Lines
        .Select(CreateScratchCards)
        .Where(c => c.WinningNumbers.Any())
        .Select(c => Math.Pow(2, c.WinningNumbers.Count() - 1))
        .Sum();

        return (long)total;
    }

    public override long RunPartTwo()
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

        return cardDictionary.Select(d => d.Value).Sum();
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

        var winningNumbers = winningSide.ExtractNumbers<int>();
        var ownedNumbers = ownedSide.ExtractNumbers<int>();

        var matches = ownedNumbers.Intersect(winningNumbers);
        return new(cardNumber + 1, matches); // + 1 because of zero index based linq
    }
}
