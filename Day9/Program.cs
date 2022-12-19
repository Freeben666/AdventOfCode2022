int ropeLength = 10; // Length of the rope. 2 for PART 1, 10 for PART 2


List<Move> moves = new List<Move>(); // Create a list that will contains all the moves

// Read the input file and populate the list of moves
foreach (string line in File.ReadLines("input"))
{
    var data = line.Split(' ');

    Move m = new Move(); ;

    switch (data[0])
    {
        case "U":
            m.Direction = Direction.Up;
            break;
        case "D":
            m.Direction = Direction.Down;
            break;
        case "R":
            m.Direction = Direction.Right;
            break;
        case "L":
            m.Direction = Direction.Left;
            break;
    }

    m.Steps = Int32.Parse(data[1]);

    moves.Add(m);
}

// Calculate the size of the board
int x = 0, minX = 0, maxX = 0, y = 0, minY = 0, maxY = 0;

foreach (Move m in moves)
{
    switch (m.Direction)
    {
        case Direction.Up:
            y += m.Steps;
            if (y > maxY) maxY = y;
            break;
        case Direction.Down:
            y -= m.Steps;
            if (y < minY) minY = y;
            break;
        case Direction.Right:
            x += m.Steps;
            if (x > maxX) maxX = x;
            break;
        case Direction.Left:
            x -= m.Steps;
            if (x < minX) minX = x;
            break;
    }
}

int width = maxX - minX + 1;
int height = maxY - minY + 1;

Console.WriteLine("minX = {0}, maxX = {1} ==> Width = {2}", minX, maxX, width);
Console.WriteLine("minY = {0}, maxY = {1} ==> Height = {2}", minY, maxY, height);

Coordinate startPosition = new Coordinate(-minX, -minY);
Console.WriteLine("Start position : {0}", startPosition);


//Creating and initializing the playing board
bool[][] board = new bool[width][];

for (int i = 0; i < width; i++)
{
    board[i] = new bool[height];

    for (int j = 0; j < height; j++)
    {
        board[i][j] = false;
    }
}

// Create the rope. For part 2, the size is 10
Rope rope = new Rope(ropeLength, startPosition);

// Apply all the moves and update the board as we go
foreach (Move m in moves)
{
    for (int i = 0; i < m.Steps; i++)
    {
        rope.Move(m.Direction);

        if (rope.Tail != null)
            board[rope.Tail.X][rope.Tail.Y] = true;
    }
}

// Count all the positions where the tail went
int positions = 0;
for (int i = 0; i < width; i++)
{
    for (int j = 0; j < height; j++)
    {
        if (board[i][j]) positions++;
    }
}

Console.WriteLine("The tails occupied {0} different positions", positions);

internal enum Direction
{
    Left, Right, Up, Down
}

internal struct Move
{
    public Direction Direction { get; set; }
    public int Steps { get; set; }
}

internal struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate()
    {
        X = 0;
        Y = 0;
    }
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return "X=" + X + ", Y=" + Y;
    }
}

internal class Knot
{
    public int X { get; set; } // Horizontal coordinate 
    public int Y { get; set; } // Vertical coordinate

    public Knot()
    {
        X = 0;
        Y = 0;
    }
    public Knot(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Knot(Coordinate c) : this(c.X, c.Y) { }
    public Knot(Knot k) : this(k.X, k.Y) { }

    public void Move(Direction d)
    {
        switch (d)
        {
            case Direction.Left:
                X -= 1;
                break;
            case Direction.Right:
                X += 1;
                break;
            case Direction.Up:
                Y += 1;
                break;
            case Direction.Down:
                Y -= 1;
                break;
        }
    }
    public Coordinate Distance(Knot? k)
    {
        if (k == null) { return new Coordinate(); }
        else { return new Coordinate(this.X - k.X, this.Y - k.Y); }
    }
}

internal class Rope
{
    public LinkedList<Knot> Knots { get; }

    Knot? Head { get => Knots.First != null ? Knots.First.Value : null; }
    public Knot? Tail { get => Knots.Last != null ? Knots.Last.Value : null; }
    public int Length { get => Knots.Count; }

    public Rope(int length, Coordinate start)
    {
        Knots = new LinkedList<Knot>();
        for (int i = 0; i < length; i++)
        {
            Knot k = new Knot(start);
            Knots.AddLast(k);
        }
    }

    public Rope(int length) : this(length, new Coordinate(0, 0)) { }

    public void Move(Direction d)
    {
        LinkedListNode<Knot>? current = Knots.First; // Start at the head of the rope

        if (current == null) { return; } // If the rope is size 0, then return

        current.Value.Move(d); // Move the head in the specified direction

        current = current.Next; // Go to the next knot in the rope

        while (current != null)
        {
#pragma warning disable CS8602 // current.Previous can't be null here
            Coordinate distance = current.Value.Distance(current.Previous.Value); // Get distance to previous knot
#pragma warning restore CS8602 // current.Previous can't be null here

            if (Math.Abs(distance.X) > 1) // If current knot is way RIGHT or LEFT of previous knot
            {
                if (distance.X > 1) // Current knot is way right of previous knot
                {
                    current.Value.Move(Direction.Left); // We go LEFT to catch up
                }
                else
                {
                    current.Value.Move(Direction.Right); // We go RIGHT to catch up
                }

                // Handling diagonal cases
                if (distance.Y >= 1) // Current is ABOVE previous knot
                {
                    current.Value.Move(Direction.Down); // We go DOWN to catch up
                }
                else if (distance.Y <= -1) // Current is UNDER previous knot
                {
                    current.Value.Move(Direction.Up); // We go UP to catch up}
                }
            }
            else if (Math.Abs(distance.Y) > 1) // If current knot is way ABOVE or UNDER previous knot
            {
                if (distance.Y > 1) // Current knot is way ABOVE previous knot
                {
                    current.Value.Move(Direction.Down); // We go DOWN to catch up
                }
                else
                {
                    current.Value.Move(Direction.Up); // We go UP to catch up
                }

                // Handling diagonal cases
                if (distance.X >= 1) // Current is RIGHT of previous knot
                {
                    current.Value.Move(Direction.Left); // We go LEFT to catch up
                }
                else if (distance.X <= -1) // Current is LEFT previous knot
                {
                    current.Value.Move(Direction.Right); // We go RIGHT to catch up}
                }
            }
            else
            {
                break; // We did not move this knot, so the next ones won't have to move either
            }

            current = current.Next; // Go to the next knot
        }
    }
}