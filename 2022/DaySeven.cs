namespace AdventOfCode._2022;

public class FileSystem
{
    private const bool DebugEnabled = false;
    private const long TotalSpace = 70000000;
    private const long RequiredSpace = 30000000;
    public long FreeSpace => TotalSpace - Root!.Size;
    public long NeedToFree => RequiredSpace - FreeSpace;
    public Directory? Root { get; } = new("/");
    private Directory? CurrentDirectory { get; set; }
    private List<string> TerminalOutput { get; }

    public FileSystem(List<string> terminalOutput)
    {
        TerminalOutput = terminalOutput;
        ProcessTerminalOutput();
    }

    private static void DebugOutput(string message)
    {
        if (DebugEnabled)
        {
            Console.WriteLine(message);
        }
    }

    public static IEnumerable<Directory> FetchAllDirectories(Directory focusDirectory)
    {
        var allDirectories = new List<Directory>();
        
        foreach (var subDirectory in focusDirectory.SubDirectories)
        {
            allDirectories.Add(subDirectory);
            allDirectories.AddRange(FetchAllDirectories(subDirectory));
        }

        return allDirectories;
    }

    private void ProcessTerminalOutput()
    {
        var currentLine = 0;

        foreach (var line in TerminalOutput)
        {
            currentLine++;

            if (!line.StartsWith("$")) continue;
            var tempLine = line.Split(" ");

            ExecuteCommand(tempLine);
        }

        return;

        void ExecuteCommand(IReadOnlyList<string> command)
        {
            switch (command[1])
            {
                case "cd":
                    ChangeDirectory(command[2]);
                    break;
                case "ls":
                    ListContents(currentLine);
                    break;
            }
        }
    }

    private void ChangeDirectory(string parameter)
    {
        switch (parameter)
        {
            case "/":
                CurrentDirectory = Root;
                DebugOutput("Directory changed to '/'");
                break;
            case "..":
                DebugOutput($"Directory changed from '{CurrentDirectory?.Name}' to '{CurrentDirectory?.Parent?.Name}'");
                CurrentDirectory = CurrentDirectory?.Parent!;
                break;
            default:
                var focusDirectory = CurrentDirectory?.SubDirectories.SingleOrDefault(sd => sd.Name == parameter);

                if (focusDirectory != null)
                {
                    CurrentDirectory = focusDirectory;
                    DebugOutput($"Directory changed to '{parameter}'");
                }
                else
                {
                    CurrentDirectory?.SubDirectories.Add(new Directory(parameter, CurrentDirectory));
                    DebugOutput($"Directory '{parameter}' created in '{CurrentDirectory?.Name}'");
                }
                break;
        }
    }
    
    private void ListContents(int currentLine)
    {
        for (var i = currentLine; i < TerminalOutput.Count; i++)
        {
            var line = TerminalOutput[i].Split(" ");

            if (line[0] == "$") break;

            if (line[0] == "dir")
            {
                CurrentDirectory?.SubDirectories.Add(new Directory(line[1], CurrentDirectory));
                DebugOutput($"Directory '{line[1]}' created in '{CurrentDirectory?.Name}'");
                continue;
            }

            if (!int.TryParse(line[0], out var size)) continue;
            var fileNameParts = line[1].Split(".");

            CurrentDirectory?.Files.Add(new File
            {
                Name = fileNameParts[0],
                Extension = fileNameParts.Length > 1 ? fileNameParts[1] : string.Empty,
                Size = size
            });
            DebugOutput($"File '{line[1]}' created in '{CurrentDirectory?.Name}'");
        }
    }
}

public class Directory
{
    public string Name { get; }
    public Directory? Parent { get; }
    public List<Directory> SubDirectories { get; }
    public List<File> Files { get; }
    public int Size => Files.Sum(f => (int)f.Size) + SubDirectories.Sum(sd => sd.Size);

    public Directory(string name, Directory? parent = null)
    {
        Name = name;
        Parent = parent;
        SubDirectories = new List<Directory>();
        Files = new List<File>();
    }
}

public class File
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public long Size { get; init; }
}

public class DaySeven
{
    private static readonly List<string> PuzzleInput = Helpers.PuzzleInput.Load(2022, 7);
    private readonly FileSystem _fileSystem = new(PuzzleInput);

    [Test]
    public void PartOne()
    {
        var subDirs = FileSystem.FetchAllDirectories(_fileSystem.Root!);
        var answer = subDirs.Where(d => d.Size <= 100000).Sum(d => d.Size);

        Console.WriteLine($"Answer: {answer}");
    }
    
    [Test]
    public void PartTwo()
    {
        var subDirs = FileSystem.FetchAllDirectories(_fileSystem.Root!);
        subDirs = subDirs.Where(sd => sd.Size > _fileSystem.NeedToFree).OrderBy(sd => sd.Size).ToList();

        Console.WriteLine($"Answer: {subDirs.First().Size}");
    }
}