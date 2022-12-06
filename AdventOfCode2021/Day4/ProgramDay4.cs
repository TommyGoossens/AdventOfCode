using AdventOfCodeShared;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Day4
{
    public class Board
    {
        private List<Dictionary<int, bool>> tiles;
        private bool hasAlreadyWon;
        public Board(IEnumerable<string> lines)
        {
            tiles = new();
            hasAlreadyWon = false;
            foreach (var l in lines) tiles.Add(l.Split(' ').Where(c => !string.IsNullOrEmpty(c)).ToDictionary(int.Parse, c => false));
        }

        public bool CheckIfBingo(int number)
        {
            var matches = tiles.Select(r => r).Where(d => d.ContainsKey(number));
            foreach (var m in matches) m[number] = true;

            var hasWinningRow = tiles.Any(r => r.Values.All(c => c == true));
            var hasWinningCol = false;
            for (int i = 0; i < 5; i++)
            {
                if (tiles.Select(c => c.ElementAt(i)).All(d => d.Value == true)) hasWinningCol = true;
            }
            if (hasWinningRow || hasWinningCol) hasAlreadyWon = true;

            return hasAlreadyWon;
        }

        public bool HasAlreadyWon() => hasAlreadyWon;

        public int GetTotalOfUnmarkedTiles() => tiles.Sum(r => r.Where(d => d.Value == false).Select(d => d.Key).Sum());
    }
    public class ProgramDay4 : AdventOfCodeProgram
    {
        public ProgramDay4(string? text = null) : base(text)
        {
        }

        protected override string[] Run()
        {

            var bingoNumbers = lines.First().Split(',').Select(int.Parse);
            var boardRows = lines.Skip(1).Where(l => !string.IsNullOrEmpty(l));
            var parsedBoards = boardRows.Select((x, i) => new { Index = i, Value = x }).GroupBy(x => x.Index / 5).Select(x => new Board(x.Select(v => v.Value))).ToList();

            var (part1, part2) = PlayBingo(parsedBoards, bingoNumbers);
            return new string[] { $"Sum of unmarked values on winning board: {part1}", $"Sum of unmarked values on last board: {part2}" };
        }

        private (int part1, int part2) PlayBingo(IEnumerable<Board> boards, IEnumerable<int> bingoNumbers)
        {
            var winningResults = new List<int>();
            foreach (var nr in bingoNumbers)
            {
                foreach (var b in boards.Where(b => !b.HasAlreadyWon()))
                {
                    var hasBingo = b.CheckIfBingo(nr);
                    if (hasBingo) winningResults.Add(b.GetTotalOfUnmarkedTiles() * nr);
                }
            }

            return (winningResults.First(), winningResults.Last());
        }

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay4("7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\r\n\r\n22 13 17 11  0\r\n 8  2 23  4 24\r\n21  9 14 16  7\r\n 6 10  3 18  5\r\n 1 12 20 15 19\r\n\r\n 3 15  0  2 22\r\n 9 18 13 17  5\r\n19  8  7 25 23\r\n20 11 10 24  4\r\n14 21 16 12  6\r\n\r\n14 21 17 24  4\r\n10 16 15  9 19\r\n18  8 23 26 20\r\n22 11 13  6  5\r\n 2  0 12  3  7");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().EndWithEquivalentOf("4512");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay4("7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\r\n\r\n22 13 17 11  0\r\n 8  2 23  4 24\r\n21  9 14 16  7\r\n 6 10  3 18  5\r\n 1 12 20 15 19\r\n\r\n 3 15  0  2 22\r\n 9 18 13 17  5\r\n19  8  7 25 23\r\n20 11 10 24  4\r\n14 21 16 12  6\r\n\r\n14 21 17 24  4\r\n10 16 15  9 19\r\n18  8 23 26 20\r\n22 11 13  6  5\r\n 2  0 12  3  7");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().EndWithEquivalentOf("1924");
        }
    }
}
