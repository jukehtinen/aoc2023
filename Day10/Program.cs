var input = File.ReadAllLines("input.txt");
var width = input[0].Length;
var map = new char[width, input.Length];
int startX = 0;
int startY = 0;
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < width; x++)
    {
        map[x, y] = input[y][x];
        if (map[x, y] == 'S')
        {
            startX = x;
            startY = y;
        }
    }
}
map[startX, startY] = '|'; // hack, replace S with a pipe

var pipes = new Dictionary<char, Pipe>
{
    { '|', Pipe.North | Pipe.South },
    { '-', Pipe.East | Pipe.West },
    { 'L', Pipe.North | Pipe.East },
    { 'J', Pipe.North | Pipe.West },
    { '7', Pipe.South | Pipe.West },
    { 'F', Pipe.East | Pipe.South }
};

var visited = new HashSet<(int x, int y)>();
var steps = 0;
var currentX = startX;
var currentY = startY;
while (true)
{
    (currentX, currentY) = GetFirstConnected(currentX, currentY);
    visited.Add((currentX, currentY));
    steps++;

    if (currentX == startX && currentY == startY)
        break;
}

Console.WriteLine(steps / 2);

for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (!visited.Contains((x, y)))
            map[x, y] = '.';
    }
}

var inside = 0;
var currentlyInside = false;
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (map[x, y] != '.')
        {
            if (pipes[map[x, y]].HasFlag(Pipe.North))
                currentlyInside = !currentlyInside;
        }
        else
        {
            if (currentlyInside)
                inside++;
        }
    }
}
Console.WriteLine(inside);

(int x, int y) GetFirstConnected(int x, int y)
{
    if (IsOk(x, y, x, y - 1)) return (x, y - 1);
    else if (IsOk(x, y, x + 1, y)) return (x + 1, y);
    else if (IsOk(x, y, x, y + 1)) return (x, y + 1);
    else if (IsOk(x, y, x - 1, y)) return (x - 1, y);
    else return (-1, -1);
}

bool IsOk(int x, int y, int nx, int ny)
{
    if (visited.Contains((nx, ny)))
        return false;

    if (nx < 0 || nx >= width || ny < 0 || ny >= width)
        return false;

    if (map[nx, ny] == '.') return false;

    var currentPipe = pipes[map[x, y]];
    var newPipe = pipes[map[nx, ny]];

    if (x == nx && y < ny)
    {
        if (currentPipe.HasFlag(Pipe.South) && newPipe.HasFlag(Pipe.North))
            return true;
    }
    else if (x == nx && y > ny)
    {
        if (currentPipe.HasFlag(Pipe.North) && newPipe.HasFlag(Pipe.South))
            return true;
    }
    else if (x < nx && y == ny)
    {
        if (currentPipe.HasFlag(Pipe.East) && newPipe.HasFlag(Pipe.West))
            return true;
    }
    else if (x > nx && y == ny)
    {
        if (currentPipe.HasFlag(Pipe.West) && newPipe.HasFlag(Pipe.East))
            return true;
    }

    return false;
}

[Flags]
public enum Pipe
{
    North = 1, East = 2, South = 4, West = 8
}