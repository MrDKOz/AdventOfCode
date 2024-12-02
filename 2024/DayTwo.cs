namespace AdventOfCode._2024;

public class DayTwo : ExerciseBase
{
    private readonly Report _report;

    public DayTwo() : base(2024, 2)
    {
        _report = new Report(Input);
    }

    [Test, Description("Answer: 490")]
    public override void PartOne()
    {
        Console.WriteLine($"Day Two, Part One Answer: {_report.SafeEntries()}");
    }

    [Test]
    public override void PartTwo()
    {
        throw new NotImplementedException();
    }

    private class Report
    {
        private readonly List<ReportEntry> _entries;

        public Report(IReadOnlyList<string> rawList)
        {
            _entries = rawList.Select(entry => new ReportEntry(entry)).ToList();
        }

        public int SafeEntries() => _entries.Count(entry => entry.IsSafe());
    }

    private class ReportEntry(string entry)
    {
        private readonly List<int> _readings =
            entry.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        public bool IsSafe()
        {
            var allAscending = AllAscending();
            var allDescending = AllDescending();
            var safeLevelDifferences = SafeLevelDifferences();

            // Console.WriteLine(string.Join(", ", _readings));
            // Console.WriteLine($"Ascending: {allAscending}, Descending: {allDescending}, Safe Differences: {safeLevelDifferences}");

            return (allAscending || allDescending) && safeLevelDifferences;
        }

        private bool AllAscending() => _readings.SequenceEqual(_readings.OrderBy(x => x));

        private bool AllDescending() => _readings.SequenceEqual(_readings.OrderByDescending(x => x));

        private bool SafeLevelDifferences()
        {
            for (var i = 0; i < _readings.Count - 1; i++)
            {
                var difference = Math.Abs(_readings[i] - _readings[i + 1]);

                if (difference is < 1 or > 3)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
