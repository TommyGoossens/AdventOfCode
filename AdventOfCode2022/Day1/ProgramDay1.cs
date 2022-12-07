using AdventOfCodeShared;

namespace AdventOfCode2022.Day1
{
    internal class ProgramDay1 : AdventOfCodeProgram
    {
        public ProgramDay1(string? text = null) : base(text)
        {
        }

        protected override string[] Run()
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
            var mostCalories = orderedElfList.First(); // Part1
            var top3 = orderedElfList.Sum(e => e); // Part 2
            return new string[] { $"Most calories: {mostCalories}.", $"The sum of the top 3 elfs is: {top3}" };
        }
    }
}
