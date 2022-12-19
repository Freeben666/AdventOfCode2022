string inputFilename = "C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day4\\input";

List<Assignment> assignments = new List<Assignment>();

int containedCount = 0;
int overlapCount = 0;

foreach(string line in File.ReadLines(inputFilename))
{
    string[] pair = line.Split(',');

    foreach (string s in pair)
    {
        var t = s.Split('-');
        var a = new Assignment();
        a.Start = Int32.Parse(t[0]);
        a.End = Int32.Parse(t[1]);
        assignments.Add(a);
    }
}

for (int i = 0; i < assignments.Count; i += 2)
{
    if (Contained(assignments[i], assignments[i + 1]))
    {
        containedCount++;
        overlapCount++;
        continue;
    }

    if (Overlap(assignments[i], assignments[i + 1]))
    {
        overlapCount++;
    }
}

Console.WriteLine("Résultat Part 1 : " + containedCount);
Console.WriteLine("Résultat Part 2 : " + overlapCount);

bool Contained(Assignment a, Assignment b)
{
    if ((a.Start >= b.Start) && (a.End <= b.End))
    {
        return true;
    }
    else if ((b.Start >= a.Start) && (b.End <= a.End))
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool Overlap(Assignment a, Assignment b)
{
    if((a.Start <= b.End) && (a.End >= b.Start))
    {
        return true;
    }
    else if ((b.Start <= a.End) && (b.End >= a.Start))
    {
        return true;
    }
    else
    {
        return false;
    }
}

struct Assignment
{
    public int Start;
    public int End;
};

