using System.Runtime.CompilerServices;

string inputFilename = "C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day2\\input";

int totalScore = 0;

foreach(string line in File.ReadLines(inputFilename))
{
    string[] inputs = line.Split(" ");
    int roundScore = 0;

    switch (inputs[0])
    {
        case "A": //Rock
            switch(inputs[1])
            {
                case "X": // Need to lose : scissors
                    roundScore = 3; 
                    roundScore += 0;
                    break;
                case "Y": // Need to draw : Rock
                    roundScore = 1;
                    roundScore += 3;
                    break;
                case "Z": // Need to win : paper
                    roundScore = 2;
                    roundScore += 6;
                    break;
            }
            break;
        case "B": // Paper
            switch (inputs[1])
            {
                case "X": // Need to lose : rock
                    roundScore = 1;
                    roundScore += 0;
                    break;
                case "Y": // Need to draw : Paper
                    roundScore = 2;
                    roundScore += 3;
                    break;
                case "Z": // Need to win : Scissors
                    roundScore = 3;
                    roundScore += 6;
                    break;
            }
            break;
        case "C": // Scissors
            switch (inputs[1])
            {
                case "X": // Need to lose : Paper
                    roundScore = 2;
                    roundScore += 0;
                    break;
                case "Y": // Need to draw : Scissors
                    roundScore = 3;
                    roundScore += 3;
                    break;
                case "Z": // Need to win : Rock
                    roundScore = 1;
                    roundScore += 6;
                    break;
            }
            break;
    }
    totalScore+= roundScore;
}

Console.WriteLine("Résultat : " + totalScore);
