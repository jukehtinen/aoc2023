using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var map = new HashSet<(int x, int y)>();
var current = (x: 0, y: 0);
map.Add(current);
foreach (var line in input)
{
    var match = Regex.Match(line, @"(\w+) (\w+) \(#(\w+)\)");
    var dir = match.Groups[1].Value;
    var length = int.Parse(match.Groups[2].Value);

    if (dir == "R")
    {
        for (var i = 0; i < length; i++)
        {
            current.x++;
            map.Add(current);
        }
    }
    else if (dir == "L")
    {
        for (var i = 0; i < length; i++)
        {
            current.x--;
            map.Add(current);
        }
    }
    else if (dir == "U")
    {
        for (var i = 0; i < length; i++)
        {
            current.y--;
            map.Add(current);
        }
    }
    else if (dir == "D")
    {
        for (var i = 0; i < length; i++)
        {
            current.y++;
            map.Add(current);
        }
    }
}

// Flood fill
var queue = new Queue<(int x, int y)>();
queue.Enqueue((1, 1));
var visited = new HashSet<(int x, int y)>();
while (queue.Count > 0)
{
    var (x, y) = queue.Dequeue();
    if (visited.Contains((x, y)))
    {
        continue;
    }
    if (map.Contains((x, y)))
    {
        continue;
    }

    visited.Add((x, y));
    queue.Enqueue((x + 1, y));
    queue.Enqueue((x - 1, y));
    queue.Enqueue((x, y + 1));
    queue.Enqueue((x, y - 1));
}

Console.WriteLine(visited.Count + map.Count);

var minX = map.Min(x => x.x);
var maxX = map.Max(x => x.x);
var minY = map.Min(x => x.y);
var maxY = map.Max(x => x.y);
for (var y = minY; y <= maxY; y++)
{
    for (var x = minX; x <= 40; x++)
    {
        if (x == 0 && y == 0)
        {
            Console.Write("O");
        }
        else if (map.Contains((x, y)))
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}
Console.WriteLine();