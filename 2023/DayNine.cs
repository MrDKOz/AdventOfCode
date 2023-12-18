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
        Console.WriteLine($"Day Eight, Part One Answer: ");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Eight, Part Two Answer: ");
    }

    private class Oasis
    {
        private readonly int[,] _readings;

        public Oasis(IReadOnlyList<string> input)
        {
            var digitCount = input[0].Split(' ').Length;
            _readings = new int[input.Count, digitCount];

            for (var i = 0; i < input.Count; i++)
            {
                for (var j = 0; j < digitCount; j++)
                {
                    _readings[i, j] = int.Parse(input[i].Split(' ')[j]);
                }
            }
            
            Console.WriteLine("hello");
        }
    }
}