using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class DayThree : ExerciseBase
{
    private readonly string _singleLine;

    public DayThree() : base(2024, 3)
    {
        _singleLine = string.Join("", Input);
    }

    [Test, Description("Answer: 174336360")]
    public override void PartOne()
    {
        var result = 0;

        var matches = GetMatches(_singleLine);

        foreach (Match match in matches)
        {
            result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        Console.WriteLine($"Day Three, Part One Answer: {result}");
    }

    public override void PartTwo()
    {
    }

    private MatchCollection GetMatches(string input)
    {
        var regex = new Regex(@"mul\((\d+),(\d+)\)");

        return regex.Matches(input);
    }
}
