namespace AdventOfCode._2023;

public class DaySix : ExerciseBase
{
    private Races? _races;

    public DaySix() : base(2023, 6)
    {
        _races = new(Input);
    }

    [SetUp]
    public void Setup()
    {
        _races = new Races(PuzzleInput.Load(2023, 6));
    }

    [Test]
    public override void PartOne()
    {
        Console.WriteLine($"Day Six, Part One Answer: {_races?.TotalWinCount}");
    }

    [Test]
    public override void PartTwo()
    {
        Console.WriteLine($"Day Six, Part Two Answer: {_races?.WinCountOfFinalRace}");
    }

    private class Races
    {
        private readonly List<Race> _raceList = new();

        public int TotalWinCount => _raceList
            .Select(r => r.WinningButtonHoldTimesCount)
            .Aggregate(1, (total, next) => total * next);

        public int WinCountOfFinalRace => _raceList.Last().WinningButtonHoldTimesCount;

        public Races(IReadOnlyCollection<string> input)
        {
            ProcessInput();
            return;

            void ProcessInput()
            {
                var times = input.First().Split(':').Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var distances = input.Last().Split(':').Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < times.Length; i++)
                {
                    AddRace(times[i], distances[i]);
                }
            }
        }

        private void AddRace(string time, string distance) =>
            _raceList.Add(new Race(float.Parse(time), float.Parse(distance)));

        private class Race
        {
            private readonly float _time;
            private readonly float _distance;
            private List<int> WinningButtonHeldTimes { get; } = new();
            public int WinningButtonHoldTimesCount => WinningButtonHeldTimes.Count;

            public Race(float time, float distance)
            {
                _time = time;
                _distance = distance;

                CalculatePossibleButtonHoldTimes();
            }

            private void CalculatePossibleButtonHoldTimes()
            {
                var buttonHoldTime = 0;

                for (var i = 0; i < _time; i++)
                {
                    var distance = buttonHoldTime * (_time - i);

                    if (distance > _distance)
                    {
                        WinningButtonHeldTimes.Add(buttonHoldTime);
                    }

                    buttonHoldTime++;
                }
            }
        }
    }
}