namespace AdventOfCode._2022;

/// <summary>
/// Puzzle link: https://adventofcode.com/2022/day/5
/// </summary>
public class DayFive : ExerciseBase
{
    private static IReadOnlyList<string> _puzzleInput = null!;
    private Warehouse? _warehouse;

    public DayFive() : base(2022, 5)
    {
        _puzzleInput = Input;
    }

    [Test]
    public override void PartOne()
    {
        _warehouse = new Warehouse(_puzzleInput);
        Console.WriteLine($"Answer: {_warehouse.GetTopCrateLabels()}");
    }

    [Test]
    public override void PartTwo()
    {
        _warehouse = new Warehouse(_puzzleInput, true);
        Console.WriteLine($"Answer: {_warehouse.GetTopCrateLabels()}");
    }

    private class Warehouse
    {
        private bool PartTwo { get; }
        private List<Stack<string>> Stacks { get; } = new();
        private List<Instruction> Instructions { get; } = new();

        public Warehouse(IReadOnlyList<string> input, bool partTwo = false)
        {
            PartTwo = partTwo;
            GetInitialState(input);
            ImportInstructions(input);
            ProcessInstructions();
        }

        private void GetInitialState(IReadOnlyList<string> input)
        {
            var bottomRowIndex = 0;
            GetStackCount();
            PopulateInitialStacks();

            return;

            void GetStackCount()
            {
                foreach (var line in input)
                {
                    bottomRowIndex++;
                    var tmpLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (tmpLine.Length < 1 || tmpLine[0] != "1") continue;

                    for (var i = 0; i < tmpLine.Length; i++)
                    {
                        Stacks.Add(new Stack<string>());
                    }

                    bottomRowIndex -= 2;
                    break;
                }
            }

            void PopulateInitialStacks()
            {
                for (var i = bottomRowIndex; i >= 0; i--)
                {
                    var line = input[i];

                    for (var s = 0; s < Stacks.Count; s++)
                    {
                        var crateLabel = line.Substring(1 + s * 4, 1);
                        if (string.IsNullOrWhiteSpace(crateLabel)) continue;

                        Stacks[s].Push(crateLabel);
                    }
                }
            }
        }

        private void ImportInstructions(IEnumerable<string> input)
        {
            foreach (var tempLine in from line in input
                     where line.StartsWith("move")
                     select line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            {
                Instructions.Add(new Instruction
                {
                    CrateCount = Convert.ToInt32(tempLine[1]),
                    SourceStack = Convert.ToInt32(tempLine[3]) - 1,
                    DestinationStack = Convert.ToInt32(tempLine[5]) - 1
                });
            }
        }

        private void ProcessInstructions()
        {
            foreach (var instruction in Instructions)
            {
                var sourceStack = Stacks[instruction.SourceStack];
                var destinationStack = Stacks[instruction.DestinationStack];

                if (PartTwo)
                {
                    var ordered = new Stack<string>();

                    for (var quantity = 0; quantity < instruction.CrateCount; quantity++)
                    {
                        ordered.Push(sourceStack.Pop());
                    }

                    while (ordered.Any())
                    {
                        destinationStack.Push(ordered.Pop());
                    }
                }
                else
                {
                    for (var i = 0; i < instruction.CrateCount; i++)
                    {
                        destinationStack.Push(sourceStack.Pop());
                    }
                }
            }
        }

        public string GetTopCrateLabels() => Stacks.Aggregate(string.Empty, (current, stack) => current + stack.Peek());
    }

    private class Instruction
    {
        public int SourceStack { get; init; }
        public int DestinationStack { get; init; }
        public int CrateCount { get; init; }
    }
}