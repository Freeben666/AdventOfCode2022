string inputFilename = "C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day3\\input";
int output = 0;

Dictionary<char, int> priorities= new Dictionary<char, int>();

char start='a';
for(int i=0; i<26; i++)
{
    priorities.Add((Char)(Convert.ToUInt16(start) + i), i + 1);
}

start = 'A';
for (int i = 0; i < 26; i++)
{
    priorities.Add((Char)(Convert.ToUInt16(start) + i), i + 27);
}

foreach (string line in File.ReadLines(inputFilename))
{
    List<char> firstCompartment= new List<char>();
    List<char> secondCompartment = new List<char>();

    for (int i=0; i < (line.Length / 2); i++)
    {
        firstCompartment.Add(line[i]);
    }

    for (int i = (line.Length / 2); i < line.Length; i++)
    {
        secondCompartment.Add(line[i]);
    }

    List<char> firstDeduped = firstCompartment.Distinct().ToList();
    firstDeduped.Sort();

    List<char> secondDeduped= secondCompartment.Distinct().ToList();
    secondDeduped.Sort();

    foreach(char c in firstDeduped)
    {
        if (secondDeduped.Exists(x => x == c))
        {
            output += priorities[c];
        }
    }
}

Console.WriteLine("Résultat partie 1 : " + output);


var input = File.OpenText(inputFilename);
int output2 = 0;

while (!input.EndOfStream)
{
    List<char>[] groupRucksacks = new List<char>[3];

    for (int i = 0; i < 3; i++)
    {
        groupRucksacks[i] = new List<char>();
        foreach(char c in input.ReadLine())
        {
            groupRucksacks[i].Add(c);
        }
    }

    IEnumerable<char> commonItem = groupRucksacks[0].Intersect(groupRucksacks[1]).Intersect(groupRucksacks[2]);

    output2 += priorities[commonItem.First<char>()];
}

Console.WriteLine("Résultat partie 2 : " + output2);