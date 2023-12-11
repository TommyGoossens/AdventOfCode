using AdventOfCodeShared;

namespace AdventOfCode2023;

public enum HandType
{
    FiveOfAKind = 6,
    FourOfAKind = 5,
    FullHouse = 4,
    ThreeOfAKind = 3,
    TwoPairs = 2,
    OnePair = 1,
    HighCards = 0
}

public class Hand
{
    private readonly Dictionary<char, int> cardValDict = new()
    {
        ['A'] = 14,
        ['K'] = 13,
        ['Q'] = 12,
        ['J'] = 11,
        ['T'] = 10,
        ['9'] = 9,
        ['8'] = 8,
        ['7'] = 7,
        ['6'] = 6,
        ['5'] = 5,
        ['4'] = 4,
        ['3'] = 3,
        ['2'] = 2
    };
    public Hand(string line, bool jokersInGame = false)
    {
        if (jokersInGame) cardValDict['J'] = -1;
        Cards = line.Split(' ')[0].Select(c => cardValDict[c]).Select(c => int.Parse(c.ToString()));
        Bid = int.Parse(line.Split(' ')[1]);
        HandType = jokersInGame ? GetJokerHandType() : GetHandType();
    }

    public IEnumerable<int> Cards { get; private init; }
    public int Bid { get; private init; }
    public HandType HandType { get; private init; }

    private HandType GetJokerHandType()
    {
        var nrOfJokers = Cards.Where(c => c == -1).Count();
        if (nrOfJokers == 0) return GetHandType();
        if (nrOfJokers == 5) return HandType.FiveOfAKind;

        var grouped = Cards.Where(c => c != -1).GroupBy(c => c).OrderByDescending(g => g.Count());
        var maxOfAKind = grouped.Max(g => g.Count());
        if (grouped.Count() == 1) return HandType.FiveOfAKind; ;
        if (grouped.Count() == 2)
        {
            if (maxOfAKind == 3 || maxOfAKind == 2 && nrOfJokers == 2 || nrOfJokers == 3) return HandType.FourOfAKind;
            return HandType.FullHouse;
        }
        if (grouped.Count() == 3) return HandType.ThreeOfAKind;
        return HandType.OnePair; // Must be one pair then
    }

    private HandType GetHandType()
    {
        var distinctCards = Cards.Distinct().Count();
        if (distinctCards == 1) return HandType.FiveOfAKind;
        if (distinctCards == Cards.Count()) return HandType.HighCards;
        var grouped = Cards.GroupBy(c => c).OrderByDescending(g => g.Count());

        if (grouped.Count() == 2 && grouped.First().Count() == 3 && grouped.Last().Count() == 2) return HandType.FullHouse;

        var maxOfAKind = grouped.Max(g => g.Count());
        if (maxOfAKind == 4) return HandType.FourOfAKind;
        if (maxOfAKind == 3) return HandType.ThreeOfAKind;
        if (grouped.Count() == 3 && grouped.Where(g => g.Count() == 2).Count() == 2) return HandType.TwoPairs;
        return HandType.OnePair; // Must be one pair then
    }
}

public class ProgramDay7(string? text = null) : AdventOfCodeProgram<long>(text)
{
    public override long RunPartOne()
    {
        var result = PlayGame();
        return result.Select((c, i) => c.Bid * (i + 1)).Sum();

    }

    public override long RunPartTwo()
    {
        var result = PlayGame(true);
        return result.Select((c, i) => c.Bid * (i + 1)).Sum();
    }

    private List<Hand> PlayGame(bool includeJokers = false)
    {
        var hands = Lines.Select(l => new Hand(l, includeJokers));
        var grouped = hands.OrderByDescending(h => h.HandType).GroupBy(h => h.HandType);
        var winningOrder = new List<Hand>();
        foreach (var group in grouped)
        {
            if (group.Count() == 1) { winningOrder.Add(group.First()); continue; }
            var ordered = group
            .OrderByDescending(g => g.Cards.ElementAt(0))
            .ThenByDescending(g => g.Cards.ElementAt(1))
            .ThenByDescending(g => g.Cards.ElementAt(2))
            .ThenByDescending(g => g.Cards.ElementAt(3))
            .ThenByDescending(g => g.Cards.ElementAt(4));
            winningOrder.AddRange(ordered);
        }

        winningOrder.Reverse();
        return winningOrder;
    }
}
