List<Int32> calories= new List<Int32>();
int v = 0;

foreach(string line in File.ReadLines("C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day1\\input"))
{
    if(line == "")
    {
        calories.Add(v);
        v = 0;
    }
    else
    {
        int t = 0;
        if (Int32.TryParse(line, out t))
        {
            v += t;
        }
    }
}

Console.WriteLine("Résultat !");
Console.WriteLine(calories.Max());

calories.Sort();
calories.Reverse();

int j = 0;

for(int i = 0; i<3; i++)
{
    j+= calories[i];
}

Console.WriteLine(j);