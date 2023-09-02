using static System.AppContext;

namespace AdventOfCode.Helpers;

public static class PuzzleInput
{
    public static List<string> Load(int year, int day)
    {
        var path = Path.Combine(BaseDirectory, year.ToString(), "Files", $"day{day}.txt");
        return File.ReadAllLines(path).ToList();
    }
}