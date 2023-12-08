// --- Day 3: Gear Ratios ---
// You and the Elf eventually reach a gondola lift station; he says the
// gondola lift will take you up to the water source, but this is as
// far as he can bring you. You go inside.
// 
// It doesn't take long to find the gondolas, but there seems to be a
// problem: they're not moving.
// 
// "Aaah!"
// 
// You turn around to see a slightly-greasy Elf with a wrench and a look
// of surprise. "Sorry, I wasn't expecting anyone! The gondola lift isn't
// working right now; it'll still be a while before I can fix it." You
// offer to help.
// 
// The engineer explains that an engine part seems to be missing from the
// engine, but nobody can figure out which one. If you can add up all
// the part numbers in the engine schematic, it should be easy to work
// out which part is missing.
// 
// The engine schematic (your puzzle input) consists of a visual
// representation of the engine. There are lots of numbers and symbols
// you don't really understand, but apparently any number adjacent to a
// symbol, even diagonally, is a "part number" and should be included in
// your sum. (Periods (.) do not count as a symbol.)
// 
// Here is an example engine schematic:
// 
// 467..114..
// ...*......
// ..35..633.
// ......#...
// 617*......
// .....+.58.
// ..592.....
// ......755.
// ...$.*....
// .664.598..
//
// In this schematic, two numbers are not part numbers because they are
// not adjacent to a symbol: 114 (top right) and 58 (middle right).
// Every other number is adjacent to a symbol and so is a part number;
// their sum is 4361.
// 
// Of course, the actual engine schematic is much larger. What is the
// sum of all of the part numbers in the engine schematic?
// 
// To begin, get your puzzle input.

namespace AdventOfCode
{

    internal class Program
    {
        public const string Input = """
        467..114..
        ...*......
        ..35..633.
        ......#...
        617*......
        .....+.58.
        ..592.....
        ......755.
        ...$.*....
        .664.598..
        """;

        private static List<string> ChopUp(string s) => s.Trim().Split('\n', 
            StringSplitOptions.TrimEntries).ToList();

        private static IEnumerable<string> FetchStringContents(string fileName) 
            => File.ReadAllLines(fileName).Select(l => l.Trim()).ToArray();

        /// <summary>
        /// Get gear symbols as GridPoints.
        /// </summary>
        /// <param name="map"></param>
        /// <returns>Points occupied by an asterisk.</returns>
        private static IEnumerable<GridPoint> FetchGearSymbols(IReadOnlyList<string> map)
        {
            for (var y = 0; y < map.Count; y++)
            {
                for (var x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == '*')
                    {
                        yield return new GridPoint(x, y);
                    }
                }
            }
        }

        // Get all numbers surrounding a given asterisk.
        private static IEnumerable<Number> GetSurroundingNumbers(GridPoint position, List<string> map)
        {
            var offsets = new List<GridPoint>()
            {
                new(-1, -1),
                new(0, -1),
                new(1, -1),
                new(-1, 0),
                new(1, 0),
                new(-1, 1),
                new(0, 1),
                new(1, 1)
            };

            foreach (var offset in offsets)
            {
                var x = offset.Column + position.Column;
                var y = offset.Row + position.Row;
                if (char.IsNumber(map[y][x]))
                {
                    yield return new Number(new(x, y), map);
                }
            }
        }

        /// <summary>
        /// A gear is defined as an asterisk surrounded by 2 numbers. If one is found,
        /// we pass it back.
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="map"></param>
        /// <returns>What really should be an object that speaks for itself.
        /// But hey, this is just for fun.</returns>
        private static long GetGearNumber(GridPoint? candidate, List<string> map)
        {
            var adjacent = GetSurroundingNumbers(candidate, map).Distinct().ToList();
            if (adjacent.Count != 2) return 0;
            Console.WriteLine($"Found a gear pair: {adjacent[0].Get} and " +
                              $"{adjacent[1].Get}, yielding {adjacent[0].Get * adjacent[1].Get}!");
            return adjacent[0].Get * adjacent[1].Get;

        }

        static void Main(string[] args)
        {
            var map = FetchStringContents("input_3.txt").ToList();
            var symbols = FetchGearSymbols(map);

            // It's succinct enough, right?
            Console.WriteLine($"Total sum is therefore {symbols.Sum(candidate => GetGearNumber(candidate, map))}.");
        }
    }
}