using AdventOfCodeShared;

namespace AdventOfCode2022.Day1
{
    internal class ProgramDay1 : AdventOfCodeProgram
    {
        public ProgramDay1(string? text = null) : base(text)
        {
        }

        protected override string RunPartOne()
        {
            var orderedElfList = GetOrderedElfList();
            var mostCalories = orderedElfList.First(); // Part1
            return $"Most calories: {mostCalories}.";
        }
        
        protected override string RunPartTwo()
        {
            var orderedElfList = GetOrderedElfList();
            var top3 = orderedElfList.Sum(e => e); // Part 2
            return $"The sum of the top 3 elfs is: {top3}";
        }

        private IEnumerable<int> GetOrderedElfList()
        {
            var caloriesPerElf = new List<int>();
            var temp = 0;
            foreach (string line in lines)
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
