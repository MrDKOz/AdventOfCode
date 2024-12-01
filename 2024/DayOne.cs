namespace AdventOfCode._2024;

public class DayOne : ExerciseBase
{
    private readonly LocationLists _locationLists;
    public DayOne() : base(2024, 1)
    {
        _locationLists = new LocationLists(Input);
    }
    [Test]
    public override void PartOne()
    {
        Console.WriteLine($"Day One, Part One Answer: {_locationLists.Process()}");
    }

    [Test]
    public override void PartTwo()
    {
        throw new NotImplementedException();
    }

    private class LocationLists
    {
        private readonly List<int> _listOne = [];
        private readonly List<int> _listTwo = [];
        private int _totalDifference;

        public LocationLists(IReadOnlyList<string> rawList)
        {
            foreach (var line in rawList)
            {
                var temp = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                _listOne.Add(int.Parse(temp.First()));
                _listTwo.Add(int.Parse(temp.Last()));
            }
        }

        public int Process()
        {
            while (_listOne.Count != 0)
            {
                NextDifference();
            }

            return _totalDifference;
        }

        private void NextDifference()
        {
            var listOneLowest = _listOne.Min();
            var listTwoLowest = _listTwo.Min();

            _totalDifference += Math.Abs(listOneLowest - listTwoLowest);

            _listOne.Remove(listOneLowest);
            _listTwo.Remove(listTwoLowest);
        }
    }
}
