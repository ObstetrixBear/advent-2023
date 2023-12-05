using static System.Char;

namespace AdventOfCode
{
    internal class Program
    {
        private const string ResourcePath = "../../../../../Resources/";

        private static IEnumerable<string> FetchStringContents(string fileName) 
            => File.ReadAllLines(ResourcePath + fileName);

        private static readonly List<string> Numerics = new()
        {
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
        };

        /// <summary>
        /// Returns first and last digit, as if they were a single two-digit number.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static int MatchDigitSimple(string line)
        {
            var numbers = new string(line.Where(IsDigit).ToArray());
            return (((numbers?.First() ?? 0) - '0') * 10) + (numbers?.Last() ?? 0) - '0';
        }

        /// <summary>
        /// Single iteration of the search loop. Takes the span, looks for first
        /// instance of either a digit or the digit written as text.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="line"></param>
        /// <returns>First digit found</returns>
        private static int? MatchDigit(int pos, string line)
        {
            if (IsDigit(line[pos])) return line[pos] - '0';
            var tempSpan = line[pos..].AsSpan();

            if (tempSpan.Length <= 0)
            {
                return null;
            }

            var n = 0;
            foreach (var num in Numerics)
            {
                n++;
                if (tempSpan.StartsWith(num))
                {
                    return n;
                }
            }
            return null;
        }

        private static int CountFirstDigit(string line)
        {
            for (var i = 0; i < line.Length; ++i)
            {
                var potentialResult = MatchDigit(i, line);
                if (potentialResult.HasValue)
                {
                    return potentialResult.Value;
                };
            }
            return 0;
        }

        private static int CountLastDigit(string line)
        {
            for (var i = line.Length-1; i >= 0; --i)
            {
                var potentialResult = MatchDigit(i, line);
                if (potentialResult.HasValue)
                {
                    return potentialResult.Value;
                };
            }
            return 0;
        }

        static void Main(string[] args)
        {
            var data = FetchStringContents("input_1.txt");
            var sum = data.Sum(line => (CountFirstDigit(line) * 10) + CountLastDigit(line));

            Console.WriteLine(sum);
        }
    }
}