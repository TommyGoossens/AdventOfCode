using AdventOfCodeShared;
using System.Collections.Generic;

namespace AdventOfCode2022.Day3
{
    internal class ProgramDay3 : AdventOfCodeProgram
    {
        private Dictionary<char, int> letterValues;

        public ProgramDay3() : base(3)
        {
            letterValues = new Dictionary<char, int>();
            var allLetters = Enumerable.Range('a', 26);
            for (int i = 0; i < allLetters.Count(); i++)
            {
                var letter = (char)allLetters.ElementAt(i);
                letterValues.Add(letter, i + 1);
                letterValues.Add(Char.ToUpper(letter), i + 27);
            }
        }

        protected override string[] Run()
        {
            var prioritySum = GetAnswerPart1();
            var badgesSum = GetAnswerPart2();
            return new string[] { $"The sum of priorities is: {prioritySum}", $"The sum of badges is: {badgesSum}" };
        }

        private int GetAnswerPart1()
        {
            var total = 0;
            lines.AsParallel().ForAll(l =>
            {
                var firstHalf = l.Substring(0, l.Length / 2);
                var secondHalf = l.Substring(l.Length / 2);
                var itemsInBoth = firstHalf.Intersect(secondHalf);
                Interlocked.Add(ref total, GetValueOfIntersectedChars(itemsInBoth));
            });
            return total;
        }

        private int GetAnswerPart2()
        {
            var total = 0;
            var i = 0;
            while ((i * 3) != lines.Length)
            {
                var batch = lines.Skip(i * 3).Take(3);
                var intersection = batch.Skip(1).Aggregate(new HashSet<char>(batch.First()),
                    (h, e) => { h.IntersectWith(e); return h; }).ToList();
                total += GetValueOfIntersectedChars(intersection);
                i++;
            }

            return total;
        }

        private int GetValueOfIntersectedChars(IEnumerable<char> chars) => chars.Select(c => letterValues[c]).Sum();
    }
}
