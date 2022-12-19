Queue<Instruction> program = new Queue<Instruction>();

foreach(var line in File.ReadLines("input"))
{
    var data = line.Split(' ');

    Instruction i = new Instruction();

    switch (data[0])
    {
        case "noop":
            i.Command = Command.noop;
            break;
        case "addx":
            i.Command = Command.addx;
            break;
    }

    if (i.Command == Command.addx)
    {
        i.Parameter = Int32.Parse(data[1]);
    }

    program.Enqueue(i);
}

int mainRegister = 1;
int addRegister = 0;
State currentState = State.Wait;

int cycles = 1;

int part1Total = 0;

while (true)
{
    if ((cycles - 20) % 40 == 0)
    {
        Console.WriteLine(" Cycle {0} ==> Register={1}", cycles, mainRegister);
        part1Total += mainRegister * cycles;
    }

    string str = cycles.ToString("0000") + "\t";
    switch (currentState)
    {
        case State.Wait:
            str += "Waiting\t";
            break;
        case State.Add:
            str += "Adding\t";
            break;
    }

    str += mainRegister.ToString("0000");

    Console.WriteLine(str);
    
    cycles++;
    switch(currentState)
    {
        case State.Wait:
            Instruction instruction;
            if (!program.TryDequeue(out instruction)) break;
            switch (instruction.Command)
            {
                case Command.addx:
                    currentState = State.Add;
                    addRegister = instruction.Parameter;
                    continue;
                case Command.noop:
                    currentState = State.Wait;
                    continue;
            }
            break;
        case State.Add:
            mainRegister += addRegister;
            currentState = State.Wait;
            continue;
    }

    break;
}

Console.WriteLine("Part 1 : {0}", part1Total);

struct Instruction
{
    public Command Command { get; set; }
    public int Parameter { get; set; }
}
enum Command
{
    noop, addx
}

enum State
{
    Wait, Add
}