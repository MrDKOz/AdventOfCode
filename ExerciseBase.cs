namespace AdventOfCode;

public abstract class ExerciseBase
{
    protected IReadOnlyList<string> Input { get; private set; }

    protected ExerciseBase(int year, int day)
    {
        Input = PuzzleInput.Load(year, day);
    }

    [Test]
    public abstract void PartOne();

    [Test]
    public abstract void PartTwo();
}