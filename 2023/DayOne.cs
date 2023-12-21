using System.Text.RegularExpressions;

namespace AdventOfCode._2023;

public class DayOne : ExerciseBase
{
    private readonly CalibrationValues _calibrationValues;

    public DayOne() : base(2023, 1)
    {
        _calibrationValues = new(Input);
    }

    [Test]
    public override void PartOne()
    {
        _calibrationValues.Process();
        Console.WriteLine($"Day One, Part One Answer: {_calibrationValues.FirstAndLastDigits.Sum()}");
    }

    [Test]
    public override void PartTwo()
    {
        _calibrationValues.Process(true);
        Console.WriteLine($"Day One, Part Two Answer: {_calibrationValues.FirstAndLastDigits.Sum()}");
    }

    private class CalibrationValues
    {
        private readonly IReadOnlyList<string> _input;
        public readonly List<int> FirstAndLastDigits = new();

        public CalibrationValues(IReadOnlyList<string> input)
        {
            _input = input;
        }

        public void Process(bool partTwo = false)
        {
            if (partTwo)
            {
                ExtractFirstAndLastDigits();
            }
            else
            {
                ExtractFirstAndLastDigitsV2();
            }
        }

        private void ExtractFirstAndLastDigits()
        {
            foreach (var line in _input)
            {
                var firstDigit = FindFirstDigit(line);
                var lastDigit = FindLastDigit(line);

                FirstAndLastDigits.Add(Convert.ToInt32($"{firstDigit}{lastDigit}"));
            }
        }

        private void ExtractFirstAndLastDigitsV2()
        {
            var regex = new Regex("(?<=(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|(\\d{1}))");

            foreach (var line in _input)
            {
                var matches = regex.Matches(line);

                var a = GetValue(matches.First());
                var b = GetValue(matches.Last());

                FirstAndLastDigits.Add(int.Parse($"{a}{b}"));
            }
        }

        private int? GetValue(Match match)
        {
            var value = GetMatchedValue(match);

            if (StringToInt.ContainsKey(value))
            {
                StringToInt.TryGetValue(value, out var convertedValue);
                return convertedValue;
            }

            _ = int.TryParse(value, out var result);
            return result;
        }

        private string GetMatchedValue(Match match)
        {
            if (!string.IsNullOrEmpty(match.Value))
            {
                return match.Value;
            }

            var result = match.Groups.Values.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value));

            return result?.Value ?? string.Empty;
        }

        private static readonly Dictionary<string, int> StringToInt = new()
        {
            { "zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };


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
}