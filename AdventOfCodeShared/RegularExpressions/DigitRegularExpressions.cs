using System;
using System.Text.RegularExpressions;

namespace AdventOfCodeShared.RegularExpressions
{
	public static partial class DigitRegularExpressions
	{
        [GeneratedRegex("\\d{1,2}")]
        public static partial Regex DayNumberRegex();

        [GeneratedRegex("\\d{4}")]
        public static partial Regex YearRegex();
    }
}

