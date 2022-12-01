namespace AdventOfCode.Day1
{
    internal class ProgramDay1 : AdventOfCodeProgram
    {
        public ProgramDay1() : base(1)
        { }

        protected override string Run()
        {
            string[] lines = File.ReadAllLines(@"Day1\advent_of_code_1_input.txt");
            var caloriesPerElf = new List<int>();
            var temp = 0;
            foreach(string line in lines)
            {
                if (string.IsNullOrEmpty(line)) {
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
            return $"Most calories: {mostCalories}. The sum of the top 3 elfs is: {top3}";
        }
    }
}
