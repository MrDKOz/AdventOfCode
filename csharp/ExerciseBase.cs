namespace AdventOfCode;

public abstract class ExerciseBase(int year, int day)
{
    protected IReadOnlyList<string> Input { get; private set; } = PuzzleInput.Load(year, day);

    [Test]
    public abstract void PartOne();

    [Test]
    public abstract void PartTwo();
}
