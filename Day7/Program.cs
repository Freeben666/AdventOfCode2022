using Day7;

string inputFilename = "C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day7\\input";

Dir root = new Dir("/");
Dir currentDir = root;

int totalDiskSpace = 70000000;
int neededSpace = 30000000;

foreach(string line in File.ReadLines(inputFilename))
{
    if (line.StartsWith("$"))
    {
        var command = line.Split(' ');

        switch (command[1])
        {
            case "cd":
                if (command[2] == "/")
                {
                    currentDir = root;
                }
                else if (command[2] == "..")
                {
                    currentDir = currentDir.Parent;
                }
                else if (currentDir.SubDirs.FindIndex(x => x.Name == command[2]) != -1)
                {
                    currentDir = currentDir.SubDirs[currentDir.SubDirs.FindIndex(x => x.Name == command[2])];
                }
                break;
            case "ls":
                break;
            default:
                break;
        }
    }
    else if (line.StartsWith("dir"))
    {
        var data = line.Split(" ");

        currentDir.SubDirs.Add(new Dir(data[1], currentDir));
    }
    else
    {
        var data = line.Split(" ");

        int size = Int32.Parse(data[0]);
        string name = data[1];

        currentDir.Files.Add(name, size);
    }
}

Console.WriteLine("Résult Part 1 : {0}", root.Part1());

Console.WriteLine("==== PART 2 ====");
Console.WriteLine("Size of / : {0}", root.Size());
int missingSpace = neededSpace - (totalDiskSpace - root.Size());
Console.WriteLine("Missing space : {0}", missingSpace);

List<int> sizes = new List<int>();
root.Part2(sizes);
sizes.Sort();
int solution = sizes.Find(x => x >= missingSpace);
Console.WriteLine("Solution : {0}", solution);