namespace AdventOfCode._2023;

public class DayTwo
{
    private readonly Games _games = new(PuzzleInput.Load(2023, 2));

    [Test]
    public void PartOne()
    {
        var toCheck = new PossibilityToCheck(12, 13, 14);
        var possibleGames = _games.GetPossibleGames(toCheck);

        Console.WriteLine($"Day Two, Part One Answer: {possibleGames.Sum(g => g.Id)}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Two, Part Two Answer: {_games.AllGamesPower}");
    }

    private record PossibilityToCheck(int RedCount, int GreenCount, int BlueCount);

    private class Games
    {
        private List<Game> GamesList { get; }
        public int AllGamesPower => GamesList.Sum(g => g.GamePower);

        public Games(List<string> input)
        {
            GamesList = new List<Game>();

            ProcessInput(input);
        }

        private void ProcessInput(List<string> input)
        {
            foreach (var game in input)
            {
                var newGame = new Game
                {
                    Id = FetchGameId(game),
                    Handfuls = FetchHandfuls(game)
                };

                GamesList.Add(newGame);
            }

            return;

            // Fetch the ID of the game
            int FetchGameId(string game) =>
                Convert.ToInt32(game.Split(':').First().Replace("Game", string.Empty).Trim());

            List<Handful> FetchHandfuls(string game)
            {
                var result = new List<Handful>();
                var sets = game.Split(':').Last();
                var setsList = sets.Split(';').ToList();

                foreach (var set in setsList)
                {
                    var tmpHandful = new Handful();

                    foreach (var colorAndCount in set.Split(','))
                    {
                        var colorAndCountSplit = colorAndCount.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var color = colorAndCountSplit.Last();
                        var count = Convert.ToInt32(colorAndCountSplit.First());

                        switch (color)
                        {
                            case "red":
                                tmpHandful.RedCount += count;
                                break;
                            case "green":
                                tmpHandful.GreenCount += count;
                                break;
                            case "blue":
                                tmpHandful.BlueCount += count;
                                break;
                            default:
                                throw new Exception($"Unknown color '{color}'");
                        }
                    }

                    result.Add(tmpHandful);
                }

                return result;
            }
        }

        public IEnumerable<Game> GetPossibleGames(PossibilityToCheck possibilityToCheck)
        {
            var result = new List<Game>();

            foreach (var game in GamesList)
            {
                var gamePossible = true;

                foreach (var handful in game.Handfuls)
                {
                    if (handful.RedCount <= possibilityToCheck.RedCount &&
                        handful.GreenCount <= possibilityToCheck.GreenCount &&
                        handful.BlueCount <= possibilityToCheck.BlueCount) continue;

                    gamePossible = false;
                    break;
                }

                if (gamePossible)
                {
                    result.Add(game);
                }
            }

            return result;
        }
    }

    public class Game
    {
        public int Id { get; init; }
        public List<Handful> Handfuls { get; init; } = null!;
        private int HighestRedCount => Handfuls.Max(h => h.RedCount);
        private int HighestGreenCount => Handfuls.Max(h => h.GreenCount);
        private int HighestBlueCount => Handfuls.Max(h => h.BlueCount);
        public int GamePower => HighestRedCount * HighestGreenCount * HighestBlueCount;
    }

    public class Handful
    {
        public int RedCount { get; set; }
        public int BlueCount { get; set; }
        public int GreenCount { get; set; }
    }
}