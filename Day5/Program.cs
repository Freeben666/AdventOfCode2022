using System.Globalization;

string inputFilename = "C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day5\\input";

var input= File.OpenText(inputFilename);

Stack<char>[] stacks = new Stack<char>[9];

for (int i = 0; i < stacks.Length; i++)
{
    stacks[i] = new Stack<char>();
}

for (int j = 0; j < 8; j++)
{
    var line = input.ReadLine();

    for (int i = 0; i < stacks.Length; i++)
    {
        string crate = line.Substring((i*4) + 1, 1);
        if ( crate != " ")
        {
            stacks[i].Push(crate[0]);
        }
    }
}

var stacks2 = new Stack<char>[9];

for (int i = 0; i < stacks.Length; i++)
{
    var array = stacks[i].ToArray();

    stacks[i] = new Stack<char>(array);
    stacks2[i] = new Stack<char>(array);
}

input.ReadLine();
input.ReadLine();

while (!input.EndOfStream)
{
    var line = input.ReadLine();
    var data = line.Split();

    int count = Int32.Parse(data[1]);
    int from = Int32.Parse(data[3]);
    int to = Int32.Parse(data[5]);

    Stack<char> temp = new Stack<char>();

    for (int i = 0; i < count; i++)
    {
        // Part1
        stacks[to - 1].Push(stacks[from - 1].Pop());

        // Part2
        temp.Push(stacks2[from - 1].Pop());
    }

    for (int i = 0; i < count; i++)
    {
        // Part 2 Continued
        stacks2[to - 1].Push(temp.Pop());
    }
}

char[] output = new char[9];
char[] output2 = new char[9];

for (int i = 0; i<9; i++)
{
    output[i] = stacks[i].Pop();
    output2[i] = stacks2[i].Pop();
}

string outputString = new string(output);
string outputString2 = new string(output2);

Console.WriteLine("Résult Part 1 : " + outputString);
Console.WriteLine("Résult Part 2 : " + outputString2);

