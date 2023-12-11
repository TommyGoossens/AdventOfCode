using AdventOfCodeShared;

namespace AdventOfCode2022.Day1
{
    internal class ProgramDay1 : AdventOfCodeProgram<int>
    {
        public ProgramDay1(string? text = null) : base(text)
        {
        }

        public override int RunPartOne()
        {
            var orderedElfList = GetOrderedElfList();
            var mostCalories = orderedElfList.First(); // Part1
            return mostCalories;
        }

        public override int RunPartTwo()
        {
            var orderedElfList = GetOrderedElfList();
            var top3 = orderedElfList.Sum(e => e); // Part 2
            return top3;
        }

        private IEnumerable<int> GetOrderedElfList()
        {
            var caloriesPerElf = new List<int>();
            var temp = 0;
            foreach (string line in Lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    caloriesPerElf.Add(temp);
                    temp = 0;
                    continue;
                }
                temp += int.Parse(line);
            }

            var orderedElfList = caloriesPerElf.OrderByDescending(d => d).AsEnumerable();
            orderedElfList = orderedElfList.Take(3);
            return orderedElfList;
        }

        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay1(input).RunPartOne().Should().Be(expectedResult);
        }

        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay1(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
