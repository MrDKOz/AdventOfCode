namespace AdventOfCode._2024;

public class DayFive : ExerciseBase
{
    private readonly (int right, int wrong) _answers;

    public DayFive() : base(2024, 5)
    {
        var instructionBook = new InstructionBook(Input);
        _answers = instructionBook.Process();
    }

    [Test, Description("Answer: 5948")]
    public override void PartOne() => Console.WriteLine($"Day Four, Part One Answer: {_answers.right}");

    [Test, Description("Answer: 3062")]
    public override void PartTwo() => Console.WriteLine($"Day Four, Part Two Answer: {_answers.wrong}");

    private class InstructionBook
    {
        private readonly PageOrderRules _pageOrderRules = new();
        private readonly List<PageUpdates> _updates = [];

        public InstructionBook(IReadOnlyList<string> input)
        {
            ParseInput(input);
        }

        private void ParseInput(IReadOnlyList<string> input)
        {
            foreach (var line in input)
            {
                if (line.Contains('|'))
                {
                    _pageOrderRules.AddRule(line);
                }
                else if (line.Contains(','))
                {
                    _updates.Add(new PageUpdates(line));
                }
            }
        }

        public (int right, int wrong) Process()
        {
            var right = 0;
            var wrong = 0;

            foreach (var update in _updates)
            {
                var updated = false;
                var allRules = _pageOrderRules.FindRules(update.GetPages());

                for (var i = 0; i < update.GetCount(); i++)
                {
                    var value = update.GetValueAtIndex(i);
                    var ruleExists = allRules.TryGetValue(value, out var relatedRule);

                    if (!ruleExists) continue;

                    foreach (var rule in relatedRule!.Where(rule => update.IsAfter(value, rule)))
                    {
                        update.MoveToBefore(value, rule);
                        updated = true;
                        i = 0;
                    }
                }

                var middlePage = update.GetMiddleValue();
                
                if (updated)
                {
                    wrong += middlePage;
                }
                else
                {
                    right += middlePage;
                }
            }

            return (right, wrong);
        }

        private class PageOrderRules
        {
            private readonly Dictionary<int, List<int>> _pageOrderRules = new();

            public Dictionary<int, List<int>> FindRules(List<int> pages)
            {
                var applicableRules = new Dictionary<int, List<int>>();

                foreach (var page in pages)
                {
                    var hasRules = _pageOrderRules.TryGetValue(page, out var rules);

                    if (!hasRules) continue;

                    foreach (var rule in rules!.Where(pages.Contains))
                    {
                        AddValue(applicableRules, page, rule);
                    }
                }

                return applicableRules;
            }

            public void AddRule(string rule)
            {
                var split = rule.Split('|');

                AddValue(_pageOrderRules, int.Parse(split[0]), int.Parse(split[1]));
            }

            private static void AddValue<TKey, TValue>(Dictionary<TKey, List<TValue>> dict, TKey key, TValue value)
                where TKey : notnull
            {
                if (!dict.TryGetValue(key, out var values))
                {
                    values = [];
                    dict[key] = values;
                }

                values.Add(value);
            }
        }

        private class PageUpdates
        {
            private readonly List<int> PagesToUpdate = [];

            public PageUpdates(string pagesToUpdate)
            {
                foreach (var pageNumber in pagesToUpdate.Split(','))
                {
                    PagesToUpdate.Add(int.Parse(pageNumber));
                }
            }

            public List<int> GetPages() => PagesToUpdate;
            
            public int GetValueAtIndex(int index) => PagesToUpdate[index];
            
            public int GetMiddleValue() => GetValueAtIndex(GetCount() / 2);
            
            public int GetCount() => PagesToUpdate.Count;

            public bool IsBefore(int toCheck, int target) => IndexOf(toCheck) < IndexOf(target);

            public bool IsAfter(int toCheck, int target) => IndexOf(toCheck) > IndexOf(target);

            public void MoveToBefore(int toMove, int before)
            {
                var toMoveIndex = IndexOf(toMove);
                var beforeIndex = IndexOf(before);

                if (toMoveIndex == -1 || beforeIndex == -1)
                    throw new Exception("One or both of the values do not exist in the list.");

                PagesToUpdate.RemoveAt(toMoveIndex);
                PagesToUpdate.Insert(IndexOf(before), toMove);
            }

            public void MoveToAfter(int toMove, int after)
            {
                var toMoveIndex = IndexOf(toMove);
                var afterIndex = IndexOf(after);

                if (toMoveIndex == -1 || afterIndex == -1)
                    throw new Exception("One or both of the values do not exist in the list.");

                PagesToUpdate.RemoveAt(toMoveIndex);

                if (afterIndex == PagesToUpdate.Count)
                    PagesToUpdate.Add(toMove);
                else
                    PagesToUpdate.Insert(afterIndex + 1, toMove);
            }

            private int IndexOf(int pageNumber)
            {
                return PagesToUpdate.IndexOf(pageNumber);
            }
        }
    }
}