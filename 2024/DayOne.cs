namespace AdventOfCode._2024;

public class DayOne : ExerciseBase
{
    private readonly LocationLists _locationLists;

    public DayOne() : base(2024, 1)
    {
        _locationLists = new LocationLists(Input);
    }

    [Test, Description("Answer: 1258579")]
    public override void PartOne() => Console.WriteLine($"Day One, Part One Answer: {_locationLists.StepOne()}");

    [Test, Description("Answer: 23981443")]
    public override void PartTwo() => Console.WriteLine($"Day One, Part Two Answer: {_locationLists.StepTwo()}");

    private class LocationLists
    {
        private readonly List<int> _listOne = [];
        private readonly List<int> _listTwo = [];

        public LocationLists(IReadOnlyList<string> rawList)
        {
            foreach (var line in rawList)
            {
                var temp = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                _listOne.Add(int.Parse(temp.First()));
                _listTwo.Add(int.Parse(temp.Last()));
            }
        }

        public int StepOne()
        {
            var totalDifference = 0;

            while (_listOne.Count != 0)
            {
                totalDifference += NextDifference();
            }

            return totalDifference;
        }

        private int NextDifference()
        {
            var listOneLowest = _listOne.Min();
            var listTwoLowest = _listTwo.Min();

            _listOne.Remove(listOneLowest);
            _listTwo.Remove(listTwoLowest);

            return Math.Abs(listOneLowest - listTwoLowest);
        }

        public int StepTwo() => _listOne.Sum(entry => entry * Occurrences(entry));

        private int Occurrences(int value) => _listTwo.Count(x => x == value);
    }
}
