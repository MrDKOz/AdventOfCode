namespace AdventOfCode._2024;

public class DayFive : ExerciseBase
{
    private InstructionBook _instructionBook;

    public DayFive() : base(2024, 5)
    {
        _instructionBook = new InstructionBook(Input);
    }

    [Test]
    public override void PartOne()
    {
        Console.WriteLine($"Day Four, Part One Answer: {_instructionBook.StepOne()}");
    }

    [Test]
    public override void PartTwo()
    {
        throw new NotImplementedException();
    }

    private class InstructionBook
    {
        private readonly PageOrderRules _pageOrderRules = new();
        private List<PageUpdates> _updates = [];

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

        public int StepOne()
        {
            var result = 0;

            foreach (var update in _updates)
            {
                var updated = false;
                var allRules = _pageOrderRules.FindRules(update._pagesToUpdate);

                for (var i = 0; i < update._pagesToUpdate.Count; i++)
                {
                    var value = update._pagesToUpdate[i];
                    var ruleExists = allRules.TryGetValue(value, out var relatedRule);

                    if (!ruleExists) continue;

                    foreach (var rule in relatedRule!)
                    {
                        if (update.IsAfter(value, rule))
                        {
                            update.MoveToBefore(value, rule);
                            updated = true;
                            i = 0;
                        }
                    }
                }

                if (updated) continue;

                var middlePage = update._pagesToUpdate[update._pagesToUpdate.Count / 2];
                result += middlePage;
            }

            return result;
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

            public int GetCount() => _pageOrderRules.Count;
        }

        private class PageUpdates
        {
            public List<int> _pagesToUpdate = [];

            public PageUpdates(string pagesToUpdate)
            {
                foreach (var pageNumber in pagesToUpdate.Split(','))
                {
                    _pagesToUpdate.Add(int.Parse(pageNumber));
                }
            }

            public bool IsBefore(int toCheck, int target) => IndexOf(toCheck) < IndexOf(target);

            public bool IsAfter(int toCheck, int target) => IndexOf(toCheck) > IndexOf(target);

            public void MoveToBefore(int toMove, int before)
            {
                var toMoveIndex = IndexOf(toMove);
                var beforeIndex = IndexOf(before);

                if (toMoveIndex == -1 || beforeIndex == -1)
                    throw new Exception("One or both of the values do not exist in the list.");

                _pagesToUpdate.RemoveAt(toMoveIndex);
                _pagesToUpdate.Insert(IndexOf(before), toMove);
            }

            public void MoveToAfter(int toMove, int after)
            {
                var toMoveIndex = IndexOf(toMove);
                var afterIndex = IndexOf(after);

                if (toMoveIndex == -1 || afterIndex == -1)
                    throw new Exception("One or both of the values do not exist in the list.");

                _pagesToUpdate.RemoveAt(toMoveIndex);

                if (afterIndex == _pagesToUpdate.Count)
                    _pagesToUpdate.Add(toMove);
                else
                    _pagesToUpdate.Insert(afterIndex + 1, toMove);
            }

            private int IndexOf(int pageNumber)
            {
                return _pagesToUpdate.IndexOf(pageNumber);
            }
        }
    }
}