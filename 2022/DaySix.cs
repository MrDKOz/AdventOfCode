namespace AdventOfCode._2022;

public class DaySix : ExerciseBase
{
    private readonly IReadOnlyList<string> _puzzleInput;

    public DaySix() : base(2022, 6)
    {
        _puzzleInput = Input;
    }
    
    [Test]
    public override void PartOne() => Console.WriteLine($"Answer: {GetAnswer(4)}");

    [Test]
    public override void PartTwo() => Console.WriteLine($"Answer: {GetAnswer(14)}");

    private int GetAnswer(int lastCount)
    {
        var lastRange = new List<char>();
        var answer = 0;

        for (var c = 0; c < _puzzleInput.First().Length; c++)
        {
            lastRange.Add(_puzzleInput.First()[c]);

            if (lastRange.Count > lastCount)
            {
                lastRange.RemoveAt(0);
            }

            if (lastRange.Count != lastCount || lastRange.Count != lastRange.Distinct().Count()) continue;

            answer = c + 1;
            break;
        }

        return answer;
    }
}