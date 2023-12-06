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
        Console.WriteLine($"Day Two, Part One Answer: {_gameCards?.Cards.Sum(c => c.Points())}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Two, Part Two Answer: {_gameCards?.TotalCards}");
    }
}

public class GameCards
{
    public List<GameCard> Cards { get; } = new();
    public int TotalCards => Cards.Sum(c => c.ProcessedCopies);

    public GameCards(List<string> input)
    {
        ProcessInput();
        ProcessPartTwoRules();

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

        void ProcessPartTwoRules()
        {
            foreach (var card in Cards)
            {
                while (card.ProcessedCopies < card.Copies)
                {
                    var matchingNumbers = card.MatchingNumberCount();
                    var cardIdToCopy = card.Id + 1;
                    for (var i = 0; i < matchingNumbers; i++)
                    {
                        var cardToCopy = Cards.Single(c => c.Id == cardIdToCopy);
                        cardToCopy.Copies++;
                        cardIdToCopy++;
                    }

                    card.ProcessedCopies++;
                }
            }
        }
    }

    private void AddCard(int id, List<int> winningNumbers, List<int> ourNumbers) =>
        Cards.Add(new GameCard
        {
            Id = id,
            OurNumbers = ourNumbers,
            WinningNumbers = winningNumbers,
            Copies = 1
        });

    public class GameCard
    {
        public int Id { get; set; }
        public List<int> OurNumbers { get; set; } = new();
        public List<int> WinningNumbers { get; set; } = new();
        private List<int> MatchingNumbers() => WinningNumbers.Intersect(OurNumbers).ToList();
        public int MatchingNumberCount() => MatchingNumbers().Count;
        public int Copies { get; set; }
        public int ProcessedCopies { get; set; }

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