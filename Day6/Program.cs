string inputFilename = "C:\\Users\\BenoîtGuillemaud\\source\\repos\\AdventOfCode2022\\Day6\\input";

var input = File.OpenText(inputFilename);


Queue<char> stream = new Queue<char>();

int i;

for (i = 0; i < 13; i++)
{
    stream.Enqueue((char)input.Read());
}


while (!input.EndOfStream)
{
    stream.Enqueue((char)input.Read());
    i++;

    if(stream.Distinct().Count() == 14)
    {
        break;
    }

    stream.Dequeue();
}

input.Close();

Console.WriteLine("Résult Part 2 : {0}", i);