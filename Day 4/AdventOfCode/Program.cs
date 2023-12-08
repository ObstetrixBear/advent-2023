using static System.Char;

namespace AdventOfCode
{
    internal class Program
    {
        private const string ResourcePath = "../../../../../Resources/";

        private static IEnumerable<string> FetchStringContents(string fileName) 
            => File.ReadAllLines(ResourcePath + fileName);


        static void Main(string[] args)
        {
            Console.WriteLine("Hworld");
        }
    }
}