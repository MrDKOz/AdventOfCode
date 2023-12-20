using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DayNine
{
    private readonly Oasis _oasis = new(PuzzleInput.Load(2023, 9));

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Nine, Part One Answer: {_oasis.Task()}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Nine, Part Two Answer: {_oasis.Task(false)}");
    }

    private class Oasis
    {
        private readonly List<int[]> _readings;

        public Oasis(IReadOnlyCollection<string> input)
        {
            _readings = new List<int[]>(input.Count);

            foreach (var line in input)
            {
                _readings.Add(line.Split(' ').Select(int.Parse).ToArray());
            }
        }

        public long Task(bool partOne = true) =>
            _readings.Select(GenerateSequences)
                .Select(sequences => CalculateUnknownValue(sequences, partOne))
                .Sum();

        private static IReadOnlyList<int[]> GenerateSequences(IReadOnlyList<int> reading)
        {
            var sequences = new List<int[]> { reading.ToArray() };
            var firstSequence = true;
            do
            {
                sequences.Add(firstSequence
                    ? CalculateDifferences(reading)
                    : CalculateDifferences(sequences.Last()));

                firstSequence = false;
            } while (sequences.Last().Any(i => i != 0));

            return sequences;
        }

        private static long CalculateUnknownValue(IReadOnlyList<int[]> sequences, bool partOne = true)
        {
            long returnValue = 0;

            for (var i = sequences.Count - 1; i >= 0; i--)
            {
                returnValue = i == sequences.Count - 1
                    ? 0
                    : partOne
                        ? sequences[i].Last() + returnValue
                        : sequences[i].First() - returnValue;
            }

            return returnValue;
        }

        private static int[] CalculateDifferences(IReadOnlyList<int> input)
        {
            var returnValue = new int[input.Count - 1];

            for (var i = 0; i < input.Count - 1; i++)
            {
                returnValue[i] = input[i + 1] - input[i];
            }

            return returnValue;
        }
    }
}