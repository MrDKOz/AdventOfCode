using System.Collections;
using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DayNine
{
    private Oasis oasis = new(PuzzleInput.Load(2023, 9));
    
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Eight, Part One Answer: {oasis.PartOne()}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Eight, Part Two Answer: ");
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

        public int PartOne()
        {
            foreach (var reading in _readings)
            {
                var sequences = GenerateSequences(reading);
                var nextValue = CalculateNextValue(sequences);
            }

            return 0;
        }

        private static IReadOnlyList<int[]> GenerateSequences(IReadOnlyList<int> reading)
        {
            var sequences = new List<int[]>();
            var firstSequence = true;
            do
            {
                sequences.Add(firstSequence
                    ? CalculateDifferences(reading)
                    : CalculateDifferences(sequences.Last()));

                firstSequence = false;
            } while (sequences.Last().Sum() > 0);

            return sequences;
        }

        private static int CalculateNextValue(IReadOnlyList<int[]> sequences)
        {
            for (var i = sequences.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(sequences[i].Sum());
            }

            return 0;
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