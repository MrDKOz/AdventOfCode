using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DayOne
{
    private CalibrationValues? _calibrationValues;

    [SetUp]
    public void Setup()
    {
        _calibrationValues = new CalibrationValues(PuzzleInput.Load(2023, 1));
    }
    
    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day One, Part One Answer: {_calibrationValues!.ProcessedValues.Sum()}");
    }

    [Test]
    public void PartTwo()
    {
        
    }
}

internal class CalibrationValues
{
    private readonly List<string> _rawInput;
    public readonly List<int> ProcessedValues = new();

    public CalibrationValues(List<string> rawInput)
    {
        _rawInput = rawInput;

        ExtractFirstAndLastDigits();
    }

    private void ExtractFirstAndLastDigits()
    {
        
        foreach (var line in _rawInput)
        {
            var firstDigit = FindFirstDigit(line);
            var lastDigit = FindLastDigit(line);
            
            ProcessedValues.Add(Convert.ToInt32($"{firstDigit}{lastDigit}"));
        }
    }

    private static string FindFirstDigit(string input)
    {
        foreach (var character in input.Where(char.IsDigit))
        {
            return character.ToString();
        }

        throw new Exception("No digit found.");
    }

    private static string FindLastDigit(string input) => FindFirstDigit(new string(input.Reverse().ToArray()));
}