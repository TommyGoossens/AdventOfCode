namespace AdventOfCode.Day1
{
    internal class ProgramDay1 : AdventOfCodeProgram
    {
        public ProgramDay1() : base(1)
        { }

        protected override string Run()
        {
            string[] lines = File.ReadAllLines(@"Day1\advent_of_code_1_input_large.txt");
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
            return $"Most {mostCalories} calories. The sum of the top 3 elfs is: {top3}";
        }

        // OLD
        //protected override string Run()
        //{
        //    string[] lines = File.ReadAllLines(@"Day1\advent_of_code_1_input.txt");
        //    int currentElf = 1;
        //    var caloriesPerElf = new Dictionary<int, List<int>>();
        //    foreach (string line in lines)
        //    {
        //        if (string.IsNullOrEmpty(line)) { currentElf++; continue; }

        //        caloriesPerElf.TryAdd(currentElf, new List<int>());
        //        caloriesPerElf[currentElf].Add(int.Parse(line));
        //    }
        //    var orderedElfList = caloriesPerElf.ToDictionary(d => d.Key, d => d.Value.Sum()).OrderByDescending(d => d.Value);
        //    var elfWithMostCalories = orderedElfList.First(); // Part1
        //    var top3 = orderedElfList.Take(3).Sum(e => e.Value); // Part 2
        //    return $"Elf nr {elfWithMostCalories.Key} with {elfWithMostCalories.Value} calories. The sum of the top 3 elfs is: {top3}";
        //}
    }
}
