string inputFilename = "C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day8\\input";

int size = 99;

int[][] treeArray = new int[size][];

for(int i = 0; i < size; i++)
{
    treeArray[i] = new int[size];
}

int j = 0;
foreach(var line in File.ReadLines(inputFilename))
{
    for(int i=0; i<size; i++)
    {
        treeArray[i][j] = Int32.Parse(line.Substring(i,1));
    }
    j++;
}

int visibleCount = 0;
int topScenicScore = 0;

for (int x = 0; x < size; x++)
{
    for (int y = 0; y < size; y++)
    {
        if (Visible(treeArray, x, y))
        {
            visibleCount++;
        }

        int t = ScenicScore(treeArray, x, y);
        if (t > topScenicScore)
        {
            topScenicScore = t;
        }
    }
}

Console.WriteLine("Part 1 result : {0}", visibleCount);
Console.WriteLine("Part 2 result : {0}", topScenicScore);

bool Visible(int[][] forest, int x, int y)
{
    if ((x == 0) ||
        (x == forest.Length-1) ||
        (y == 0) ||
        (y == forest.Length-1))
    {
        return true;
    }

    bool visibleFromTop = true;
    bool visibleFromLeft = true;
    bool visibleFromRight = true;
    bool visibleFromBottom = true;

    // Vérification de la verticale haute
    for (int i = 0; i < y; i++)
    {
        if (forest[x][i] >= forest[x][y])
        {
            visibleFromTop = false;
            break;
        }
    }

    // Vérification de la verticale basse
    for (int i = y+1; i < forest.Length; i++)
    {
        if (forest[x][i] >= forest[x][y])
        {
            visibleFromBottom = false;
            break;
        }
    }

    // Vérification de l'horizontale gauche
    for (int i = 0; i < x; i++)
    {
        if (forest[i][y] >= forest[x][y])
        {
            visibleFromLeft = false;
            break;
        }
    }

    // Vérification de l'horizontale droite
    for (int i = x + 1; i < forest.Length; i++)
    {
        if (forest[i][y] >= forest[x][y])
        {
            visibleFromRight = false;
            break;
        }
    }

    return visibleFromTop || visibleFromLeft || visibleFromRight || visibleFromBottom;
}

int ScenicScore(int[][] forest, int x, int y)
{
    if ((x == 0) ||
        (x == forest.Length-1) ||
        (y == 0) ||
        (y == forest.Length-1))
    {
        return 0;
    }

    int top = 0, left = 0, right = 0, bottom = 0;

    int i;

    // Visibilité vers le haut
    for (i = 1; i <= y; i++)
    {
        if (forest[x][y - i] >= forest[x][y])
        {
            i++;
            break;
        }

    }
    top = i-1;

    // Visibilité vers le bas
    for (i = 1; i < forest.Length - y; i++)
    {
        if (forest[x][y + i] >= forest[x][y])
        {
            i++;
            break;
        }
    }
    bottom = i-1;

    // Visibilité vers la gauche
    for (i = 1; i <= x; i++)
    {
        if (forest[x - i][y] >= forest[x][y])
        {
            i++;
            break;
        }
    }
    left = i-1;

    // Visibilité vers la droite
    for (i = 1; i < forest.Length-x; i++)
    {
        if (forest[x + i][y] >= forest[x][y])
        {
            i++;
            break;
        }
    }
    right = i-1;

    return top * left * right * bottom;
}