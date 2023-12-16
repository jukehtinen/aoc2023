var input = File.ReadAllLines("input.txt");

var map = new char[input.Length, input[0].Length];
for (var y = 0; y < input.Length; y++)
    for (var x = 0; x < input[y].Length; x++)
        map[x, y] = input[y][x];


// dir 0 = up, 1 = right, 2 = down, 3 = left

Console.WriteLine(Run(map, 0, 0, 1));

var largest = 0;
for (var y = 0; y < map.GetLength(1); y++)
{
    var result = Run(map, 0, y, 1);
    if (result > largest)
        largest = result;

    result = Run(map, input[0].Length - 1, y, 1);
    if (result > largest)
        largest = result;
}

for (var x = 0; x < map.GetLength(0); x++)
{
    var result = Run(map, x, 0, 2);
    if (result > largest)
        largest = result;

    result = Run(map, x, input.Length - 1, 2);
    if (result > largest)
        largest = result;
}

Console.WriteLine(largest);

static int Run(char[,] map, int initx, int inity, int initdir)
{
    var rays = new Stack<(int x, int y, int dir)>();
    var raysSeen = new HashSet<(int x, int y, int dir)>();
    var visited = new HashSet<(int x, int y)>();

    rays.Push((initx, inity, initdir));

    while (rays.Count > 0)
    {
        var (x, y, dir) = rays.Pop();
        if (raysSeen.Contains((x, y, dir)))
            continue;
        raysSeen.Add((x, y, dir));

        while (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1))
        {
            visited.Add((x, y));

            var c = map[x, y];
            if (c == '/')
            {
                dir = dir switch
                {
                    0 => 1,
                    1 => 0,
                    2 => 3,
                    3 => 2,
                    _ => throw new Exception("Invalid direction")
                };
            }
            else if (c == '\\')
            {
                dir = dir switch
                {
                    0 => 3,
                    1 => 2,
                    2 => 1,
                    3 => 0,
                    _ => throw new Exception("Invalid direction")
                };
            }
            else if (c == '-' && (dir == 0 || dir == 2))
            {
                if (!raysSeen.Contains((x, y, 1)))
                    rays.Push((x + 1, y, 1));
                if (!raysSeen.Contains((x, y, 3)))
                    rays.Push((x - 1, y, 3));
                break;
            }
            else if (c == '|' && (dir == 1 || dir == 3))
            {
                if (!raysSeen.Contains((x, y, 0)))
                    rays.Push((x, y - 1, 0));
                if (!raysSeen.Contains((x, y, 2)))
                    rays.Push((x, y + 1, 2));
                break;
            }

            (x, y) = dir switch
            {
                0 => (x, y - 1),
                1 => (x + 1, y),
                2 => (x, y + 1),
                3 => (x - 1, y),
                _ => throw new Exception("Invalid direction")
            };

            //PrintMap(map, visited);
        }
    }
    return visited.Count;
}

static void PrintMap(char[,] map, HashSet<(int x, int y)> visited)
{
    for (var y = 0; y < map.GetLength(1); y++)
    {
        for (var x = 0; x < map.GetLength(0); x++)
        {
            Console.Write(visited.Contains((x, y)) ? '#' : map[x, y]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}