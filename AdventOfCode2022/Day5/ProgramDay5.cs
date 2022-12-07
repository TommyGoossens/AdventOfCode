using AdventOfCode2022.Day6;
using AdventOfCodeShared;
using FluentAssertions;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode2022.Day5
{
    public class ProgramDay5 : AdventOfCodeProgram
    {
        private string[] actions;
        public ProgramDay5(string? text = null) : base(text)
        {
            actions = Array.Empty<string>();
        }

        protected override string[] Run()
        {
            var endResult1 = PerformMoveActions(GetCurrentStackAsDictionary(), actions, false);
            var endResult2 = PerformMoveActions(GetCurrentStackAsDictionary(), actions, true);
            var part1 = string.Join("", endResult1.Select(d => d.Value.FirstOrDefault().ToString()));
            var part2 = string.Join("", endResult2.Select(d => d.Value.FirstOrDefault().ToString()));
            return new string[] { $"Rearrangement done: {part1}", $"Rearramgement with CrateMover 9001: {part2}" };
        }

        private Dictionary<int, List<char>> PerformMoveActions(Dictionary<int, List<char>> currentStack, string[] actions, bool canPickupMultipleBoxes)
        {
            foreach (var (amount, source, dest) in actions.Select(ParseMoveStringAsActions))
            {
                var convertedAmount = Math.Min(amount, currentStack[source].Count());
                var itemsToMove = currentStack[source].Take(convertedAmount).ToList();

                if (!canPickupMultipleBoxes) itemsToMove.Reverse();
                currentStack[source].RemoveRange(0, convertedAmount);
                currentStack[dest].InsertRange(0, itemsToMove);

            }
            //PrintStack(currentStack);
            return currentStack;
        }

        private void PrintStack(Dictionary<int, List<char>> copy)
        {
            var temp = new Dictionary<int, List<char>>(copy.ToDictionary(c => c.Key, c => new List<char>(c.Value)));
            var hor = temp.Count();
            var vert = temp.Select(d => d.Value.Count()).Max();
            var result = new char[hor][];
            for (int h = 0; h < hor; h++)
            {
                result[h] = new char[vert];
                var col = temp[h + 1];
                col.Reverse();
                for (int v = 0; v < vert; v++)
                {
                    var val = col.ElementAtOrDefault(v);
                    result[h][vert - (v + 1)] = val;
                }
                //temp[h + 1].Reverse();
            }

            for (int v = 0; v < vert; v++)
            {
                for (int h = 0; h < hor; h++) Console.Write($"[{result[h][v]}] ");
                Console.WriteLine();
            }

            foreach (var p in temp) Console.Write($" {p.Key}  ");
            Console.WriteLine();
        }

        private Dictionary<int, List<char>> GetCurrentStackAsDictionary()
        {
            var cratesDict = new Dictionary<int, List<char>>();
            var index = lines.ToList().FindIndex(s => s.StartsWith(" 1"));

            var stackSection = lines.Take(index + 1);
            var rowWithNumbers = stackSection.Last().Split(" ").Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse);
            var stacks = stackSection.Take(stackSection.Count() - 1);

            foreach (var n in rowWithNumbers)
            {
                var temp = stacks.SelectMany(s => s.Skip((n - 1) * 4).Take(4).Where(c => c != '[' && c != ']' && c != ' '));
                cratesDict[n] = temp.ToList();
            }
            actions = lines.Skip(index + 2).ToArray();
            return cratesDict;
        }

        private (int amount, int source, int dest) ParseMoveStringAsActions(string moveString)
        {
            Regex re = new(@"\d+");
            var parsed = re.Matches(moveString).Select(m => int.Parse(m.Value));

            var amount = parsed.ElementAt(0);
            var source = parsed.ElementAt(1);
            var dest = parsed.ElementAt(2);
            return (amount, source, dest);
        }

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay5("    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().EndWithEquivalentOf("CMZ");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay5("    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().EndWithEquivalentOf("MCD");
        }
    }
}
