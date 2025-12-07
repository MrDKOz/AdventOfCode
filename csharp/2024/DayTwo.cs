namespace AdventOfCode._2024;

public class DayTwo : ExerciseBase
{
    private readonly Report _report;

    public DayTwo() : base(2024, 2)
    {
        _report = new Report(Input);
    }

    [Test, Description("Answer: 490")]
    public override void PartOne() => Console.WriteLine($"Day Two, Part One Answer: {_report.SafeEntries()}");

    [Test, Description("Answer: 536")]
    public override void PartTwo() => Console.WriteLine($"Day Two, Part Two Answer: {_report.SafeDampenedEntries()}");

    private class Report
    {
        private readonly List<ReportEntry> _entries;
        public int SafeEntries() => _entries.Count(entry => entry.IsSafe());
        public int SafeDampenedEntries() => _entries.Count(entry => entry.IsSafeWithDampening());

        public Report(IReadOnlyList<string> rawList) =>
            _entries = rawList.Select(entry => new ReportEntry(entry)).ToList();
    }

    private class ReportEntry(string entry)
    {
        private readonly List<int> _readings =
            entry.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        public bool IsSafe() => IsRecordSafe(_readings);

        private static bool IsRecordSafe(List<int> toTest)
        {
            var allAscending = toTest.SequenceEqual(toTest.OrderBy(x => x));
            var allDescending = toTest.SequenceEqual(toTest.OrderByDescending(x => x));
            var safeDifferences = true;

            for (var i = 0; i < toTest.Count - 1; i++)
            {
                var difference = Math.Abs(toTest[i] - toTest[i + 1]);

                if (difference is < 1 or > 3)
                {
                    safeDifferences = false;
                }
            }

            return (allAscending || allDescending) && safeDifferences;
        }

        public bool IsSafeWithDampening()
        {
            if (IsSafe()) return true;

            for (var i = 0; i < _readings.Count; i++)
            {
                var testRecord = new List<int>();
                testRecord.AddRange(_readings);
                testRecord.RemoveAt(i);

                if (IsRecordSafe(testRecord))
                    return true;
            }

            return false;
        }
    }
}