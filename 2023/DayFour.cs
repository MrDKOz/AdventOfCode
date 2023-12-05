using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DayFour
{
    private GameCards? _gameCards;
    
    [SetUp]
    public void Setup()
    {
        _gameCards = new GameCards(PuzzleInput.Load(2023, 4));
    }

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Two, Part One Answer: {_gameCards.Cards.Sum(c => c.Points())}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Two, Part Two Answer: ");
    }
}

public class GameCards
{
    public List<GameCard> Cards { get; set; } = new();

    public GameCards(List<string> input)
    {
        ProcessInput();

        return;

        void ProcessInput()
        {
            foreach (var line in input)
            {
                var cardSplit = line.Split(':');
                var cardId = Convert.ToInt32(cardSplit.First().Split(' ').Last());

                var numbers = cardSplit.Last().Split('|');
                var winningNumbers = ConvertStringToList(numbers.First());
                var ourNumbers = ConvertStringToList(numbers.Last());

                AddCard(cardId, winningNumbers, ourNumbers);
            }
        }

        List<int> ConvertStringToList(string stringToConvert)
        {
            var splitInput = stringToConvert.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return splitInput.Select(number => Convert.ToInt32(number)).ToList();
        }
    }

    private void AddCard(int id, List<int> winningNumbers, List<int> ourNumbers) =>
        Cards.Add(new GameCard
        {
            Id = id,
            OurNumbers = ourNumbers,
            WinningNumbers = winningNumbers
        });

    public class GameCard
    {
        public int Id { get; set; }
        public List<int> OurNumbers { get; set; } = new();
        public List<int> WinningNumbers { get; set; } = new();
        public List<int> MatchingNumbers() => WinningNumbers.Intersect(OurNumbers).ToList();
        public int MatchingNumberCount() => MatchingNumbers().Count;

        public int Points()
        {
            var points = MatchingNumberCount() > 0 ? 1 : 0;

            for (var i = 1; i < MatchingNumberCount(); i++)
            {
                points *= 2;
            }

            return points;
        }
    }
}
