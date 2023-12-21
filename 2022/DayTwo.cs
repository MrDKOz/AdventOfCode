namespace AdventOfCode._2022;

/// <summary>
/// Puzzle link: https://adventofcode.com/2022/day/2
/// </summary>
public class DayTwo : ExerciseBase
{
    private readonly IEnumerable<string> _puzzleInput;
    private readonly ScoreLookup _scoreLookup = new();

    public DayTwo() : base(2022, 2)
    {
        _puzzleInput = Input;
    }

    [Test]
    public override void PartOne()
    {
        var totalScore = _puzzleInput.Sum(moveAndResponse => _scoreLookup.GetScore(moveAndResponse[0], moveAndResponse[2]));

        Console.WriteLine($"Answer: {totalScore}");
    }

    [Test]
    public override void PartTwo()
    {
        var totalScore = _puzzleInput.Sum(moveAndResponse => _scoreLookup.GetScore(moveAndResponse[0], moveAndResponse[2], true));

        Console.WriteLine($"Answer: {totalScore}");
    }

    private class ScoreLookup
    {
        private readonly List<Match> _originalRuleset = new()
        {
            new Match('A', 'X', 4),
            new Match('A', 'Y', 8),
            new Match('A', 'Z', 3),
            new Match('B', 'X', 1),
            new Match('B', 'Y', 5),
            new Match('B', 'Z', 9),
            new Match('C', 'X', 7),
            new Match('C', 'Y', 2),
            new Match('C', 'Z', 6)
        };
    
        private readonly List<Match> _revisedRuleset = new()
        {
            new Match('A', 'X', 3),
            new Match('A', 'Y', 4),
            new Match('A', 'Z', 8),
            new Match('B', 'X', 1),
            new Match('B', 'Y', 5),
            new Match('B', 'Z', 9),
            new Match('C', 'X', 2),
            new Match('C', 'Y', 6),
            new Match('C', 'Z', 7)
        };
    
        public int GetScore(char played, char response, bool useRevisedRuleset = false)
        {
            var ruleset = useRevisedRuleset ? _revisedRuleset : _originalRuleset;
            return ruleset.SingleOrDefault(m => m.Played == played && m.Response == response)!.Score;
        }
    }

    private class Match
    {
        internal readonly char Played;
        internal readonly char Response;
        internal readonly int Score;
    
        public Match(char played, char response, int score)
        {
            Played = played;
            Response = response;
            Score = score;
        }
    }
}