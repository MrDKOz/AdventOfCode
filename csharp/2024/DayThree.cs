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

    [Test, Description("Answer: 88802350")]
    public override void PartTwo()
    {
        var result = 0;

        var matches = GetMatches(_singleLine);

        foreach (Match match in matches)
        {
            var substring = _singleLine[..match.Index];

            var lastDoIndex = substring.LastIndexOf("do()", StringComparison.Ordinal);
            var lastDontIndex = substring.LastIndexOf("don't()", StringComparison.Ordinal);

            var dontAfterDo = lastDontIndex > lastDoIndex;

            if (lastDontIndex != -1 && dontAfterDo)
            {
                continue;
            }

            result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        Console.WriteLine($"Day Three, Part Two Answer: {result}");
    }

    private MatchCollection GetMatches(string input)
    {
        var regex = new Regex(@"mul\((\d+),(\d+)\)");

        return regex.Matches(input);
    }
}
