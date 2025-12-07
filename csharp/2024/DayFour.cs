namespace AdventOfCode._2024;

public class DayFour() : ExerciseBase(2024, 4)
{
    private static (int row, int col)[] Directions =>
        [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 0), (0, 1), (1, -1), (1, 0), (1, 1)];

    [Test, Description("Answer: 2685")]
    public override void PartOne()
    {
        var rowCount = Input.Count;
        var columnCount = Input[0].Length;

        var result = 0;
        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                if (Input[i][j] != 'X') continue;

                result += Directions
                    .Count(direction => IsWordMatch(i, j, direction, rowCount, columnCount));
            }
        }

        Console.WriteLine($"Day Four, Part One Answer: {result}");
    }

    private bool IsWordMatch(int rowIdx, int colIdx, (int row, int col) direction, int rowCount, int columnCount)
    {
        const string targetWord = "XMAS";
        
        for (var k = 0; k < targetWord.Length; k++)
        {
            var newRow = rowIdx + k * direction.row;
            var newCol = colIdx + k * direction.col;

            if (!IsInGrid((newRow, newCol), rowCount, columnCount))
            {
                return false;
            }

            if (Input[newRow][newCol] != targetWord[k])
            {
                return false;
            }
        }

        return true;
    }

    [Test, Description("Answer: 2048")]
    public override void PartTwo()
    {
        var rowCount = Input.Count;
        var columnCount = Input[0].Length;

        var result = 0;
        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                if (Input[i][j] != 'A') continue;

                if (CheckDiagonals(i, j, rowCount, columnCount))
                {
                    result++;
                }
            }
        }

        Console.WriteLine($"Day Four, Part Two Answer: {result}");
    }

    private bool CheckDiagonals(int row, int col, int rowCount, int columnCount)
    {
        char[] requiredChars = ['M', 'S'];
        var leftDiagonalChars = new List<char>();
        var rightDiagonalChars = new List<char>();

        // Left Diagonal
        int[] leftRows = [row - 1, row + 1];
        int[] leftCols = [col - 1, col + 1];

        foreach (var (r, c) in new[] { (leftRows[0], leftCols[0]), (leftRows[1], leftCols[1]) })
        {
            if (!IsInGrid((r, c), rowCount, columnCount)) return false;
            leftDiagonalChars.Add(Input[r][c]);
        }

        // Right Diagonal
        int[] rightRows = [row - 1, row + 1];
        int[] rightCols = [col + 1, col - 1];

        foreach (var (r, c) in new[] { (rightRows[0], rightCols[0]), (rightRows[1], rightCols[1]) })
        {
            if (!IsInGrid((r, c), rowCount, columnCount)) return false;
            rightDiagonalChars.Add(Input[r][c]);
        }

        // Check if both diagonals contain 'M' and 'S'
        return requiredChars.All(c => leftDiagonalChars.Contains(c)) && requiredChars.All(c => rightDiagonalChars.Contains(c));
    }

    private static bool IsInGrid((int row, int col) coord, int rowLen, int colLen) => 
        0 <= coord.row
        && coord.row < rowLen
        && 0 <= coord.col
        && coord.col < colLen;
}