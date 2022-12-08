using AdventOfCodeShared;
using System;
using System.IO;

namespace AdventOfCode2022.Day2
{

    internal enum RPSMove
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }

    internal enum StrategyMove
    {
        Lose = 0,
        Draw = 3,
        Win = 6,
    }

    internal static class Helper
    {
        internal static RPSMove ParseRpsMove(this string action)
        {
            return action switch
            {
                "A" or "X" => RPSMove.Rock,
                "B" or "Y" => RPSMove.Paper,
                "C" or "Z" => RPSMove.Scissors,
                _ => throw new NotSupportedException($"Action not supported: {action}")
            };
        }

        internal static StrategyMove ParseStrategyMove(this string action)
        {
            return action switch
            {
                "X" => StrategyMove.Lose,
                "Y" => StrategyMove.Draw,
                "Z" => StrategyMove.Win,
                _ => throw new NotSupportedException($"Action not supported: {action}")
            };
        }
    }

    internal class ProgramDay2 : AdventOfCodeProgram
    {
        public ProgramDay2(string? text = null) : base(text)
        {
        }

        protected override string RunPartOne()
        {
            var part1 = GetAnswerPart1();
            return $"Total score is: {part1}";
        }

        protected override string RunPartTwo()
        {
            var part2 = GetAnswerPart2();
            return $"Total score is: {part2}";
        }

        private int GetAnswerPart1()
        {
            var totalScore = 0;
            lines.AsParallel().ForAll(l =>
            {
                var opponent = l.Split(' ')[0].ParseRpsMove();
                var player = l.Split(' ')[1].ParseRpsMove();
                var roundScore = (int)opponent - (int)player;
                var result = roundScore switch
                {
                    1 or -2 => StrategyMove.Lose,
                    0 => StrategyMove.Draw,
                    _ => StrategyMove.Win
                };
                Interlocked.Add(ref totalScore, (int)player + (int)result);
            });
            return totalScore;
        }
        private int GetAnswerPart2()
        {
            var totalScore = 0;
            lines.AsParallel().ForAll(l =>
            {
                var opponent = l.Split(' ')[0].ParseRpsMove();
                var strategy = l.Split(' ')[1].ParseStrategyMove();
                var roundScore = strategy switch
                {
                    StrategyMove.Lose => Interlocked.Add(ref totalScore, GetLosingScore(opponent)),
                    StrategyMove.Draw => Interlocked.Add(ref totalScore, (int)opponent + 3),
                    StrategyMove.Win => Interlocked.Add(ref totalScore, GetWinningScore(opponent)),
                    _ => throw new NotImplementedException(),
                };
            });
            return totalScore;
        }

        private int GetWinningScore(RPSMove move)
        {
            var playerMoveScore = move switch
            {
                RPSMove.Rock => (int)RPSMove.Paper,
                RPSMove.Paper => (int)RPSMove.Scissors,
                RPSMove.Scissors => (int)RPSMove.Rock,
                _ => throw new NotImplementedException()
            };
            return playerMoveScore + 6;
        }

        private int GetLosingScore(RPSMove move)
        {
            var playerMoveScore = move switch
            {
                RPSMove.Rock => (int)RPSMove.Scissors,
                RPSMove.Paper => (int)RPSMove.Rock,
                RPSMove.Scissors => (int)RPSMove.Paper,
                _ => throw new NotImplementedException()
            };
            return playerMoveScore;
        }

        public override void RunTestsPartOne(string input, string expectedResult)
        {
            throw new NotImplementedException();
        }

        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            throw new NotImplementedException();
        }
    }
}
